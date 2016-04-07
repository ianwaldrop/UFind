using UnityEngine;

namespace UFind
{
	public interface IFinderResult : IRanked
	{
//		IDetailView DetailView { get; }
		GUIContent Description { get; }
		GUIContent Content { get; }
		string Title { get; }

		void Execute(IFinderContext context);
	}
}
