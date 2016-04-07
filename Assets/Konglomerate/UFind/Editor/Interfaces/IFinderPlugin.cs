namespace UFind
{
	public interface IFinderPlugin : IGenerateResultCollection
	{
		string Name { get; }
	}
}
