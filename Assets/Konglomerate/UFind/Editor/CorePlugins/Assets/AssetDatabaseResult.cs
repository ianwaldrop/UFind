using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

namespace UFind
{
	public class AssetDatabaseResult : UFResult
	{
		public readonly string path;

		public AssetDatabaseResult(string guid)
		{
			path = AssetDatabase.GUIDToAssetPath(guid);
			UnityObject = AssetDatabase.LoadAssetAtPath<Object>(path);
			CanOpenAsset = UnityObject is MonoScript || UnityObject is SceneAsset;
		}

		#region implemented abstract members of UFResult
		public override void Execute(IFinderContext context)
		{
			if (ModifierOpen)
			{
				if (UnityObject is MonoScript)
				{
					InternalEditorUtility.OpenFileAtLineExternal(path, 0);
				}

				if (UnityObject is SceneAsset)
				{
					EditorSceneManager.OpenScene(path, ModifierShift ? OpenSceneMode.Additive : OpenSceneMode.Single);
				}
			}
			else
			{
				SelectUnityObject(ModifierShift);
			}
		}

		public override GUIContent Description
		{
			get
			{
				if (ModifierOpen && UnityObject is MonoScript)
				{
					return GetContentForScriptEditor();
				}
				if (ModifierCommand)
				{
					if (UnityObject is SceneAsset)
					{
						return new GUIContent(ModifierShift ? "Open Additive" : "Open");
					}
				}

				return !IsFolder ? new GUIContent(GetRelativeAssetPath(path)) : null;
			}
		}

		public override string Title
		{
			get { return IsFolder ? GetRelativeAssetPath(path) : UnityObject.name; }
		}
		#endregion

		#region Private
		bool IsFolder { get { return File.GetAttributes(path).HasFlag(FileAttributes.Directory); } }
		#endregion
	}
}
