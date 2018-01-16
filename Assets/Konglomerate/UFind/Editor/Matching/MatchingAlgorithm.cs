#define USE_CACHE

namespace UFind
{
	public abstract class MatchingAlgorithm : IMatchingAlgorithm
    {
        public virtual int GetMatchScoreForTerm(UFQuery query, IScoreCache scoreCache, string term)
        {
			var currentScores = scoreCache.GetScoresForQuery(query);
			#if USE_CACHE
            if (currentScores.HasScoreForTerm(term))
            {
                return currentScores.GetScoreForTerm(term);
            }
			#endif

			var score = GenerateMatchScore(query.Value, term);
            currentScores.SetScoreForTerm(term, score);
            return score;
        }

        protected abstract int GenerateMatchScore(string a, string b);
    }
}
