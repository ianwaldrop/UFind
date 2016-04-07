/*
 * Copyright © 2014 Krakhaus
 *
 * Styles
 */

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UFind
{
	public static class UFStyles
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
		public static GUISkin Skin { get; private set; }

		public static GUIStyle SearchField
		{
			get { return Skin.GetStyle("search-field"); }
		}

		public static GUIStyle Header
		{
			get { return Skin.GetStyle("header"); }
		}

		public static GUIStyle Title
		{
			get { return Skin.GetStyle("result-title"); }
		}

		public static GUIStyle Description
		{
			get { return Skin.GetStyle("result-description"); }
		}

		public static GUIStyle SearchIcon
		{
			get { return Skin.GetStyle("search-icon"); }
		}

		public static GUIStyle Icons
		{
			get { return Skin.GetStyle("icons"); }
		}

		public static GUIStyle Result
		{
			get { return Skin.GetStyle("result"); }
		}

		public static GUIStyle Help
		{
			get { return Skin.GetStyle("help-text"); }
		}

		// due to some issue with unity we have to load _ALL_ of the textures first and query for them by name...
		// so globally accessable helper because helpful
		public static Texture2D BuiltInTexture(string name)
		{
			return textures.FirstOrDefault(t => t.name.Equals(name));
		}
		#endregion

		#region Private
		static readonly Texture2D[] textures;
		#endregion
	}
}