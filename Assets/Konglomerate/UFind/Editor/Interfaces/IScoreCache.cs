using System.Collections.Generic;

namespace UFind
{
	public interface IScoreCache
    {
        void CacheQueryData(UFQuery queryToCache);

        IQueryScores GetScoresForQuery(UFQuery query);
    }

    public class SimpleScoreCache : IScoreCache
    {
		public IQueryScores GetScoresForQuery(UFQuery query)
		{
            if (cachedScores.ContainsKey(query.Value))
            {
                return cachedScores[query.Value];
            }
            return null;
		}
        
        public void CacheQueryData(UFQuery queryToCache)
        {
            var value = queryToCache.Value;
            if (!cachedScores.ContainsKey(value))
            {
                cachedScores.Add(value, new QueryScores());
            }
        }

        readonly Dictionary<string, QueryScores> cachedScores = new Dictionary<string, QueryScores>();
    }
}
