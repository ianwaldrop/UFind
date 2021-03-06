using UFind;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

using Object = UnityEngine.Object;

namespace UFind
{
	public class ComponentPlugin : UFPlugin
	{
		#region IFinderPlugin implementation
		protected override IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			return Object.FindObjectsOfType<Component>()
				.Where(c => !ignoredTypes.Contains(c.GetType())
				       && context.GetIsMatchForTerm(ObjectNames.GetInspectorTitle(c)))
				.Select<Component, IFinderResult>(c => new ComponentResult(c));
		}

		public override string Name { get { return "Components"; } }
		#endregion

		#region Private
		static readonly List<Type> ignoredTypes = new List<Type>
		{
			typeof(Transform),
			typeof(AudioListener),
			typeof(FlareLayer),
			typeof(GUILayer)
		};
		#endregion
	}
}
