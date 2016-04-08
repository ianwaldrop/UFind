using UnityEngine;
using UnityEditor;

namespace UFind
{
	public class HierarchyResult : UFResult
	{
		public readonly GameObject gameObject;

		public HierarchyResult(GameObject gameObject)
		{
			this.gameObject = gameObject;
			UnityObject = gameObject;
		}

		#region implemented abstract members of UFResult
		public override void Execute(IFinderContext context)
		{
			if (ModifierAlt)
			{
				gameObject.SetActive(!gameObject.activeSelf);
			}
			else
			{
				SelectUnityObject(ModifierShift);
			}
		}

		public override string Title
		{
			get { return ObjectNames.NicifyVariableName(gameObject.name); }
		}

		public override GUIContent Description
		{
			get
			{
				if (ModifierAlt)
				{
					return new GUIContent("Toggle Game Object active state");
				}
				return ModifierShift
					? GetContentForAdditiveSelection(Title)
					: GetHierarchyPath(gameObject);
			}
		}
		#endregion
	}
}
