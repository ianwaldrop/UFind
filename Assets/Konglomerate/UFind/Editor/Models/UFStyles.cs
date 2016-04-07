using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UFind
{
	static class UFStyles
	{
		#region Constructors
		static UFStyles()
		{
			textures = Resources.FindObjectsOfTypeAll<Texture2D>();

			const string PATH_SKIN_FORMAT = "Konglomerate/UFind/skin-{0}-theme.guiskin";
			var skinPath = string.Format(PATH_SKIN_FORMAT, EditorGUIUtility.isProSkin ? "dark" : "light");
			Skin = EditorGUIUtility.Load(skinPath) as GUISkin;

			Skin.button.focused.background = BuiltInTexture("Pre button");
			SearchIcon.normal.background = BuiltInTexture("Search Icon");
			Header.normal.background = BuiltInTexture("TE Toolbar");
		}
		#endregion

		#region Properties
		internal static GUISkin Skin { get; private set; }

		internal static GUIStyle SearchField
		{
			get { return Skin.GetStyle("search-field"); }
		}

		internal static GUIStyle Header
		{
			get { return Skin.GetStyle("header"); }
		}

		internal static GUIStyle Title
		{
			get { return Skin.GetStyle("result-title"); }
		}

		internal static GUIStyle Description
		{
			get { return Skin.GetStyle("result-description"); }
		}

		internal static GUIStyle SearchIcon
		{
			get { return Skin.GetStyle("search-icon"); }
		}

		internal static GUIStyle Icons
		{
			get { return Skin.GetStyle("icons"); }
		}

		internal static GUIStyle Result
		{
			get { return Skin.GetStyle("result"); }
		}

		internal static GUIStyle Help
		{
			get { return Skin.GetStyle("help-text"); }
		}

		// due to some issue with unity we have to load _ALL_ of the textures first and query for them by name...
		// so globally accessable helper because helpful - Ian
		internal static Texture2D BuiltInTexture(string name)
		{
			return textures.FirstOrDefault(t => t.name.Equals(name));
		}
		#endregion

		#region Private
		static readonly Texture2D[] textures;
		#endregion
	}
}