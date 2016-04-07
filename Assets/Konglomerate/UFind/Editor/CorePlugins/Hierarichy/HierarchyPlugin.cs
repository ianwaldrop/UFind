using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace UFind
{
	public class HierarchyPlugin : UFPlugin
	{
		public override string Name { get { return "Game Objects"; } }

		protected override IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			return Object.FindObjectsOfType<Transform>()
				.Where(t => context.GetIsMatchForTerm(t.name))
				.OrderBy(t => t.GetSiblingIndex())
				.Select<Transform, IFinderResult>(t => new HierarchyResult(t.gameObject));
		}
	}
}
