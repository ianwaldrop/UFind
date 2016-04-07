namespace UFind
{
	public interface IGenerateResultCollection
	{
		IResultCollection Results { get; }

		void GenerateResults(IFinderContext context);
	}
}