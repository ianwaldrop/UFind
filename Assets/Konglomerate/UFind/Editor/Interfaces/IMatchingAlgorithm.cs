using System.Collections.Generic;

namespace UFind
{
    public interface IMatchingAlgorithm
    {
        int GetMatchScoreForTerm(UFQuery query, IScoreCache scoreCache, string term);
    }
}
