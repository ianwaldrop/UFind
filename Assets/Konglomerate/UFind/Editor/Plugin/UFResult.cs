using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace UFind
{
	public abstract class UFResult : IRanked
	{
		#region IFinderResult implementation
		/// <summary>
		/// Executes this result.
		/// </summary>
		public abstract void Execute(IFinderContext context);

//		public virtual IDetailView DetailView { get { return null; } }

		/// <summary>
		/// Gets the title which appears in the result list view.
		/// </summary>
		public abstract string Title { get; }

		/// <summary>
		/// Gets the description text which appears in the result list view.
		/// </summary>
		/// <value>The description.</value>
		public abstract GUIContent Description { get; }

		/// <summary>
		/// Gets the match score for this result.
		/// </summary>
		public int Score
		{
			get { return UFModel.Context.GetMatchScoreForResult(this); }
		}

		/// <summary>
		/// Gets the GUIContent displayed in the result list view.
		/// </summary>
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

		#region Protected
		/// <summary>
		/// Gets or sets the unity object.
		/// You should set this value if your plugin deals with Unity Object Types.
		/// </summary>
		protected virtual Object UnityObject { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this result can be opened.
		/// Affects the result of the <see cref="UFind.UFResult.ModifierOpen"/> property.
		/// </summary>
		protected virtual bool CanOpenAsset { get; set; }

		/// <summary>
		/// Gets a value indicating whether the command/control key is being held down.
		/// </summary>
		protected static bool ModifierCommand
		{
			get
			{
				var current = Event.current;
				return current.command || current.control;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the shift key is being held down.
		/// </summary>
		protected bool ModifierShift
		{
			get { return Event.current.shift; }
		}

		/// <summary>
		/// Gets a value indicating whether the alt key is being held down.
		/// </summary>
		protected bool ModifierAlt
		{
			get { return Event.current.alt; }
		}

		/// <summary>
		/// Gets a value indicating whether the target of this result should be opened.
		/// </summary>
		protected bool ModifierOpen
		{
			get { return !ModifierShift && ModifierCommand && CanOpenAsset; }
		}

		/// <summary>
		/// Gets the GUIContent for opening the target of this result in the defined script editor.
		/// </summary>
		/// <returns>The content for script editor.</returns>
		protected static GUIContent GetContentForScriptEditor()
		{
			var editor = InternalEditorUtility.GetExternalScriptEditor().Equals("internal")
				? "MonoDevelop"
				: "external";
			return new GUIContent(string.Format("Open in {0}", editor));
		}

		/// <summary>
		/// Gets the GUIContent for additively adding the target object to the active selection.
		/// </summary>
		/// <returns>The content for additive selection.</returns>
		protected static GUIContent GetContentForAdditiveSelection(string assetName)
		{
			var text = string.Format("Add {0} to selection", assetName);
			return new GUIContent(text);
		}

		/// <summary>
		/// Gets the exclusive hierarchy path for this GameObject.
		/// </summary>
		/// <returns>The hierarchy path.</returns>
		protected static GUIContent GetHierarchyPath(GameObject gameObject)
		{
			return GetHierarchyPath(gameObject.transform);
		}

		/// <summary>
		/// Gets the exclusive hierarchy path for this Transform.
		/// </summary>
		/// <returns>The hierarchy path.</returns>
		protected static GUIContent GetHierarchyPath(Transform transform)
		{
			return new GUIContent(transform.GetPath(true));
		}

		/// <summary>
		/// Gets the relative asset path.
		/// </summary>
		/// <returns>The relative asset path.</returns>
		protected static string GetRelativeAssetPath(string path)
		{
			return path.Remove(0, 6);
		}

		/// <summary>
		/// Selects the <see cref="UFind.UFResult.UnityObject"/>.
		/// </summary>
		/// <param name="additive">If set to <c>true</c> additively adds the object to the selection.</param>
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
