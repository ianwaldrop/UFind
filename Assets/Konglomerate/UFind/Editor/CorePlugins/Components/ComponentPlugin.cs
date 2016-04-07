using UFind;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace UFind
{
	public class ComponentPlugin : UFPlugin
	{
		public override string Name { get { return "Components"; } }

		protected override IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			return Object.FindObjectsOfType<Component>()
				.Where(c => !ignoredTypes.Contains(c.GetType())
				       && context.GetIsMatchForTerm(ObjectNames.GetInspectorTitle(c)))
				.OrderBy(c => c.transform.GetSiblingIndex())
				.Select<Component, IFinderResult>(c => new ComponentResult(c));
		}

		protected override IEnumerable<IFinderResult> GetCommandResults(IFinderContext context)
		{
			return null;
		}

		static readonly System.Type[] ignoredTypes = {
			typeof(Transform),
			typeof(AudioListener),
			typeof(FlareLayer),
			typeof(GUILayer)
		};
	}
}
