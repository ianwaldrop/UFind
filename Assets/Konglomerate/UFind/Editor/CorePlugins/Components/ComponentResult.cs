using UFind;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class ComponentResult : UFResult
{
	public readonly Component component;

	public ComponentResult(Component component)
	{
		this.component = component;

		var mb = component as MonoBehaviour;
		CanOpenAsset = !(mb == null || MonoScript.FromMonoBehaviour(mb) == null);
		UnityObject = component;
	}

	#region implemented abstract members of UFResult
	public override void Execute(IFinderContext context)
	{
		if (ModifierOpen)
		{
			var path = AssetDatabase.GetAssetPath(component);
			InternalEditorUtility.OpenFileAtLineExternal(path, 0);
		}
		else
		{
			SelectUnityObject(ModifierShift);
		}
	}

	public override string Title { get { return ObjectNames.GetInspectorTitle(component); } }

	public override GUIContent Description
	{
		get
		{
			if (ModifierOpen)
			{
				return GetContentForScriptEditor();
			}
			if (ModifierShift)
			{
				return GetContentForAdditiveSelection(Title);
			}
			return new GUIContent(component.gameObject.name);
		}
	}
	#endregion
}
