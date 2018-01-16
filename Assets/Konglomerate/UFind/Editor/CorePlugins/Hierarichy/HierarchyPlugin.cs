using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace UFind
{
	public class HierarchyPlugin : UFPlugin
	{
		public override string Name { get { return "Game Objects"; } }

		protected override IEnumerable<UFResult> GetObjectResults(IFinderContext context)
		{
			return Object.FindObjectsOfType<Transform>()
				         .Where(t => context.GetIsMatchForTerm(t.name))
				         .OrderBy(t => t.GetSiblingIndex())
				         .Select(t => new HierarchyResult(t.gameObject) as UFResult);
		}
	}
}
