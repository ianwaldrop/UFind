using UnityEngine;
using System.Collections.Generic;

namespace UFind
{
	public interface IFinderModel
	{
		IFinderContext Context { get; set; }

		IFinderResult Selected { get; }
		
		IResultCollection Results { get; }

		ICollection<IFinderPlugin> Plugins { get; }

		Vector2 ResultListViewScrollPosition { get; set; }
	}
}

