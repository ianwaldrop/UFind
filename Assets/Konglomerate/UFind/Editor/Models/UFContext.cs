using UFind;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

namespace UFind
{
	public class UFContext : IFinderContext
	{
		public UFContext()
		{
			CacheQueryData(Query);
		}

		#region Properties
		public UFQuery Query
		{
			get { return query; }
			set { CacheQueryData(query = value); }
		}

		void CacheQueryData(UFQuery query)
		{
			currentQueryValue = query.Value;
			currentQueryLowerChars.Clear();
			currentQueryLowerChars.AddRange(Query.Lower.ToCharArray());

			if (!cachedScores.ContainsKey(currentQueryValue))
			{
				cachedScores.Add(currentQueryValue, new Dictionary<string, int>());
			}
		}

		public bool IsSlashCommand { get { return Query.Value.StartsWith("/"); } }

		public Event CurrentEvent { get { return Event.current; } }

		public IFinderResult CurrentResult { get; internal set; }
		#endregion

		#region Actions
		public bool GetIsMatchForTerm(string term)
		{
			return GetMatchScoreForTerm(term) > 0;
		}

		public int GetMatchScoreForResult(IFinderResult result)
		{
			return GetMatchScoreForTerm(result.Title);
		}

		public int GetMatchScoreForTerm(string term)
		{
			var currentScores = cachedScores[currentQueryValue];
			if (currentScores.ContainsKey(term))
			{
				return currentScores[term];
			}
			else
			{
				var qchars = new List<char>(currentQueryLowerChars);
				var score = 0;

				foreach (var tchars in term.ToLower())
				{
					if (qchars.Contains(tchars))
					{
						qchars.Remove(tchars);
						score++;
					}
				}

				currentScores[term] = score;

				return score;
			}
		}
		#endregion

		#region Private
		readonly Dictionary<string, Dictionary<string, int>> cachedScores = new Dictionary<string, Dictionary<string, int>>();
		readonly List<char> currentQueryLowerChars = new List<char>();
		string currentQueryValue;
		UFQuery query;
		#endregion
	}
}

public class UFContextTests
{
	[SetUp]
	public void Setup()
	{
		context = new UFContext();
	}

	[TearDown]
	public void TearDown()
	{
		context.Query = string.Empty;
	}

	UFContext context;

	[Test]
	public void SlashCommand_IsTrue()
	{
		context.Query = "/test";
		Assert.IsTrue(context.IsSlashCommand);
	}

	[Test]
	public void SlashCommand_IsFalse()
	{
		context.Query = "test";
		Assert.IsFalse(context.IsSlashCommand);
	}
}
