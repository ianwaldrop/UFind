using System.Linq;

namespace UFind
{
    public class SimpleMatchingService : MatchingAlgorithm
    {
        protected override int GenerateMatchScore(string a, string b)
        {
			b = b.ToLower();
			return a.ToLower()
				    .ToCharArray()
				    .Count(b.Contains);
        }
    }
}
