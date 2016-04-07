using System.Collections.ObjectModel;

namespace UFind
{
	public interface IFinderPlugin : IRanked
	{
		ReadOnlyCollection<IFinderResult> Results { get; }

		string Name { get; }

		void GenerateResults(IFinderContext context);
	}
}
