using UnityEngine;
using UnityEditor;
using UFind;

namespace UFind
{
	public class ComponentDetailView : UFDetailView
	{
		public override void Draw(IFinderContext context, IFinderResult result)
		{
			result.To<ComponentResult>(componentResult =>
			{
				Editor.CreateCachedEditor(componentResult.component, null, ref editor);

				using (new GUILayout.VerticalScope())
				{
					editor.OnInspectorGUI();
				}
			});
		}

		Editor editor;
	}
}
