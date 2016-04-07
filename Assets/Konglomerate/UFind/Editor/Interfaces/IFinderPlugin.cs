namespace UFind
{
	public interface IFinderPlugin : IGenerateResultCollection, IRanked
	{
		string Name { get; }
	}
}
