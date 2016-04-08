using System.Collections.ObjectModel;

namespace UFind
{
	interface IFinderPlugin : IRanked
	{
		void GenerateResults(IFinderContext context);

		ReadOnlyCollection<IFinderResult> Results { get; }

		string Name { get; }
	}
}
