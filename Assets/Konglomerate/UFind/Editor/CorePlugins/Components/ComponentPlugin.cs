using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UFind
{
	public class ComponentPlugin : UFPlugin
	{
		#region IFinderPlugin implementation
		protected override IEnumerable<UFResult> GetObjectResults(IFinderContext context)
		{
			return Object.FindObjectsOfType<Component>()
				         .Where(c => !ignoredTypes.Contains(c.GetType())
				                && context.GetIsMatchForTerm(ObjectNames.GetInspectorTitle(c)))
				         .Select<Component, UFResult>(c => new ComponentResult(c));
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
