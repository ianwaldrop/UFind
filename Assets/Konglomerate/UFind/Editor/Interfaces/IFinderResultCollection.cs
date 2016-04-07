using System.Collections.Generic;

namespace UFind
{
	public interface IResultCollection : IList<IFinderResult>
	{
		void AddRange(IEnumerable<IFinderResult> collection);
	}
}
