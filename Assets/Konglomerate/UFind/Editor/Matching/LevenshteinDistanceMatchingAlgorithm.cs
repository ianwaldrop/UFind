using UnityEngine;

namespace UFind
{
	public class LevenshteinDistanceMatchingAlgorithm : MatchingAlgorithm
	{
		protected override int GenerateMatchScore(string a, string b)
		{
			return DamerauLevensteinMetricImplementation(a, b);
		}

		int BasicImplementation(string a, string b)
		{
			if (string.IsNullOrEmpty(a))
			{
				if (!string.IsNullOrEmpty(b))
				{
					return b.Length;
				}
				return 0;
			}

			if (string.IsNullOrEmpty(b))
			{
				if (!string.IsNullOrEmpty(a))
				{
					return a.Length;
				}
				return 0;
			}

			int cost, min1, min2, min3;
			int[,] d = new int[a.Length + 1, b.Length + 1];

			for (int i = 0; i <= d.GetUpperBound(0); i += 1)
			{
				d[i, 0] = i;
			}

			for (int i = 0; i <= d.GetUpperBound(1); i += 1)
			{
				d[0, i] = i;
			}

			for (int i = 1; i <= d.GetUpperBound(0); i += 1)
			{
				for (int j = 1; j <= d.GetUpperBound(1); j += 1)
				{
					cost = System.Convert.ToInt32(!(a[i - 1] == b[j - 1]));

					min1 = d[i - 1, j] + 1;
					min2 = d[i, j - 1] + 1;
					min3 = d[i - 1, j - 1] + cost;
					d[i, j] = Mathf.Min(Mathf.Min(min1, min2), min3);
				}
			}

			return d[d.GetUpperBound(0), d.GetUpperBound(1)];
		}

		int ReducedMemoryImplementation(string a, string b)
		{
			if (string.IsNullOrEmpty(a))
			{
				if (string.IsNullOrEmpty(b)) return 0;
				return b.Length;
			}
			if (string.IsNullOrEmpty(b)) return a.Length;

			if (a.Length > b.Length)
			{
				var temp = b;
				b = a;
				a = temp;
			}

			var m = b.Length;
			var n = a.Length;
			var distance = new int[2, m + 1];
			// Initialize the distance 'matrix'
			for (var j = 1; j <= m; j++) distance[0, j] = j;

			var currentRow = 0;
			for (var i = 1; i <= n; ++i)
			{
				currentRow = i & 1;
				distance[currentRow, 0] = i;
				var previousRow = currentRow ^ 1;
				for (var j = 1; j <= m; j++)
				{
					var cost = (b[j - 1] == a[i - 1] ? 0 : 1);
					distance[currentRow, j] = Mathf.Min(Mathf.Min(
								distance[previousRow, j] + 1,
								distance[currentRow, j - 1] + 1),
								distance[previousRow, j - 1] + cost);
				}
			}
			return distance[currentRow, m];
		}

		int DamerauLevensteinMetricImplementation(string a, string b)
		{
			const int MAX_LENGTH = 32;
			var metric = new DamerauLevensteinMetric(MAX_LENGTH);
			return metric.GetDistance(a, b, MAX_LENGTH);
		}
	}

	public class DamerauLevensteinMetric
	{
		private const int DEFAULT_LENGTH = 255;
		private int[] _currentRow;
		private int[] _previousRow;
		private int[] _transpositionRow;

		public DamerauLevensteinMetric()
			: this(DEFAULT_LENGTH)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maxLength"></param>
		public DamerauLevensteinMetric(int maxLength)
		{
			_currentRow = new int[maxLength + 1];
			_previousRow = new int[maxLength + 1];
			_transpositionRow = new int[maxLength + 1];
		}

		/// <summary>
		/// Damerau-Levenshtein distance is computed in asymptotic time O((max + 1) * min(first.length(), second.length()))
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public int GetDistance(string first, string second, int max)
		{
			int firstLength = first.Length;
			int secondLength = second.Length;

			if (firstLength == 0)
				return secondLength;

			if (secondLength == 0) return firstLength;

			if (firstLength > secondLength)
			{
				string tmp = first;
				first = second;
				second = tmp;
				firstLength = secondLength;
				secondLength = second.Length;
			}

			if (max < 0) max = secondLength;
			if (secondLength - firstLength > max) return max + 1;

			if (firstLength > _currentRow.Length)
			{
				_currentRow = new int[firstLength + 1];
				_previousRow = new int[firstLength + 1];
				_transpositionRow = new int[firstLength + 1];
			}

			for (int i = 0; i <= firstLength; i++)
				_previousRow[i] = i;

			char lastSecondCh = (char)0;
			for (int i = 1; i <= secondLength; i++)
			{
				char secondCh = second[i - 1];
				_currentRow[0] = i;

				// Compute only diagonal stripe of width 2 * (max + 1)
				int from = Mathf.Max(i - max - 1, 1);
				int to = Mathf.Min(i + max + 1, firstLength);

				char lastFirstCh = (char)0;
				for (int j = from; j <= to; j++)
				{
					char firstCh = first[j - 1];

					// Compute minimal cost of state change to current state from previous states of deletion, insertion and swapping 
					int cost = firstCh == secondCh ? 0 : 1;
					int value = Mathf.Min(Mathf.Min(_currentRow[j - 1] + 1, _previousRow[j] + 1), _previousRow[j - 1] + cost);

					// If there was transposition, take in account its cost 
					if (firstCh == lastSecondCh && secondCh == lastFirstCh)
						value = Mathf.Min(value, _transpositionRow[j - 2] + cost);

					_currentRow[j] = value;
					lastFirstCh = firstCh;
				}
				lastSecondCh = secondCh;

				int[] tempRow = _transpositionRow;
				_transpositionRow = _previousRow;
				_previousRow = _currentRow;
				_currentRow = tempRow;
			}

			return _previousRow[firstLength];
		}
	}
}
