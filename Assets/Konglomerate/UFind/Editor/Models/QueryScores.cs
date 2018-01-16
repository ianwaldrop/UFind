using System.Collections.Generic;

namespace UFind
{
	public class QueryScores : Dictionary<string, int>, IQueryScores
	{
		public int GetScoreForTerm(string term)
		{
			int result = 0;
			TryGetValue(term, out result);
			return result;
		}

		public void SetScoreForTerm(string term, int value)
		{
			this[term] = value;
		}

		public bool HasScoreForTerm(string term)
		{
			return ContainsKey(term);
		}
	}
}
