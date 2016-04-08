using UnityEngine;

namespace UFind
{
	public interface IFinderResult : IRanked
	{
		void Execute(IFinderContext context);

//		IDetailView DetailView { get; }
		GUIContent Description { get; }
		GUIContent Content { get; }
		string Title { get; }
	}
}
