namespace UFind
{
	public interface IQueryScores
	{
		int GetScoreForTerm(string term);

		void SetScoreForTerm(string term, int value);

		bool HasScoreForTerm(string term);
	}
}
