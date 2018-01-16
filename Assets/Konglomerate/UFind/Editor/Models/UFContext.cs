using UFind;
using UnityEngine;
using NUnit.Framework;

namespace UFind
{
    class UFContext : IFinderContext
    {
        internal UFContext()
        {
            scoreCache.CacheQueryData(Query);
        }

        #region Properties
        public bool IsSlashCommand { get { return Query.Value.StartsWith("/", System.StringComparison.Ordinal); } }

        public Event CurrentEvent { get { return Event.current; } }

        public UFResult SelectedResult { get; internal set; }

        public UFQuery Query
        {
            get { return query; }
            set { scoreCache.CacheQueryData(query = value); }
        }
        #endregion

        #region Actions
        public bool GetIsMatchForTerm(string term)
        {
            return GetMatchScoreForTerm(term) > 0;
        }
        #endregion

        #region Internal
        internal int GetMatchScoreForResult(UFResult result)
        {
            return GetMatchScoreForTerm(result.Title);
        }

        internal int GetMatchScoreForTerm(string term)
        {
            return matchingService.GetMatchScoreForTerm(Query, scoreCache, term);
        }
        #endregion

        #region Private
        readonly IMatchingAlgorithm matchingService = new LevenshteinDistanceMatchingAlgorithm();
        readonly IScoreCache scoreCache = new SimpleScoreCache();
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
