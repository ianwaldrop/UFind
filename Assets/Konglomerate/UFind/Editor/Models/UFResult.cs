using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace UFind
{
	public abstract class UFResult : IFinderResult
	{
		#region IFinderResult implementation
//		public virtual IDetailView DetailView { get { return null; } }

		public abstract GUIContent Description { get; }

		public abstract string Title { get; }

		public int Score
		{
			get { return UFModel.Context.GetMatchScoreForResult(this); }
		}

		public virtual GUIContent Content
		{
			get
			{
				var content = EditorGUIUtility.ObjectContent(UnityObject, UnityObject.GetType());
				content.text = Title;
				return content;
			}
		}
		#endregion

		#region Actions
		public abstract void Execute(IFinderContext context);
		#endregion

		#region Protected
		protected virtual Object UnityObject { get; set; }

		protected virtual bool CanOpenAsset { get; set; }

		protected static bool ModifierCommand
		{
			get
			{
				var current = Event.current;
				return current.command || current.control;
			}
		}

		protected bool ModifierShift
		{
			get { return Event.current.shift; }
		}
		
		protected bool ModifierAlt
		{
			get { return Event.current.alt; }
		}

		protected bool ModifierOpen
		{
			get { return !ModifierShift && ModifierCommand && CanOpenAsset; }
		}

		protected static GUIContent GetContentForScriptEditor()
		{
			var editor = InternalEditorUtility.GetExternalScriptEditor().Equals("internal")
				? "MonoDevelop"
				: "external";
			return new GUIContent(string.Format("Open in {0}", editor));
		}

		protected static GUIContent GetContentForAdditiveSelection(string assetName)
		{
			var text = string.Format("Add {0} to selection", assetName);
			return new GUIContent(text);
		}

		protected static GUIContent GetHierarchyPath(GameObject gameObject)
		{
			return GetHierarchyPath(gameObject.transform);
		}

		protected static GUIContent GetHierarchyPath(Transform transform)
		{
			return new GUIContent(transform.GetPath(true));
		}

		protected static string GetRelativeAssetPath(string path)
		{
			return path.Remove(0, 6);
		}

		protected void SelectUnityObject(bool additive = false)
		{
			if (additive)
			{
				var length = Selection.objects.Length;
				var selection = new Object[length + 1];
				System.Array.Copy(Selection.objects, selection, length);
				selection[length] = UnityObject;
				Selection.objects = selection;
			}
			else
			{
				Selection.activeObject = UnityObject;
			}
		}
		#endregion
	}
}
