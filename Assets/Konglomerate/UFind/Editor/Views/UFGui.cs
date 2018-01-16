using UnityEngine;
using UnityEditor;

namespace UFind
{
	static class UFGui
	{
		internal static void SearchBar()
		{
			const string QUERY_FIELD_NAME = "query";

			using (new GUILayout.HorizontalScope(UFStyles.SearchField))
			{
				GUILayout.Box(string.Empty, UFStyles.SearchIcon);

				GUI.SetNextControlName(QUERY_FIELD_NAME);
				GUI.FocusControl(QUERY_FIELD_NAME);

				EditorGUI.BeginChangeCheck();
				var query = EditorGUILayout.TextField(UFModel.Context.Query.Value, UFStyles.SearchField);

				//var query = EditorGUILayout.TextField(UFModel.Context.Query.Value, UFStyles.SearchField);
				//UFModel.QueryEditor = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl) as TextEditor;
				//if (UFModel.CursorIndex >= 0)
				//{
				//	UFModel.QueryEditor.cursorIndex = UFModel.CursorIndex;
				//	UFModel.QueryEditor.selectIndex = UFModel.CursorIndex;
				//	UFModel.CursorIndex = -1;
				//}

				if (EditorGUI.EndChangeCheck())
				{
					UFController.UpdateQuery(query);
				}

				if (UFModel.Context.SelectedResult != null)
				{
					GUILayout.Box(UFModel.Context.SelectedResult.Content.image, UFStyles.Icons);
				}
			}
		}

		internal static void ResultList(float width)
		{
			using (var scope = new GUILayout.ScrollViewScope(UFModel.ResultListViewScrollPosition, GUILayout.Width(width)))
			{
				foreach (var plugin in UFModel.Plugins)
				{
					if (plugin.Results.Count > 0)
					{
						GUILayout.Label(plugin.Name.ToUpper(), UFStyles.Header);
						foreach (var result in plugin.Results)
						{
							ResultItem(result);
						}
					}
				}
				UFModel.ResultListViewScrollPosition = scope.scrollPosition;
			}
		}

		internal static void ResultItem(UFResult result)
		{
			using (new GUILayout.HorizontalScope())
			{
				if (UFModel.Context.SelectedResult == result)
				{
					GUI.SetNextControlName("focused");
					GUI.FocusControl("focused");
				}

				if (GUILayout.Button(string.Empty, GUILayout.ExpandWidth(true)))
				{
					UFModel.SelectResult(result);
				}

				var buttonRect = GUILayoutUtility.GetLastRect();
				var content = result.Content;

				var imageRect = new Rect(buttonRect)
				{
					size = Vector2.one * buttonRect.height
				};
				imageRect.width += 20;

				var titleRect = new Rect(buttonRect)
				{
					xMin = imageRect.xMax,
					yMin = buttonRect.yMin,
					width = buttonRect.width - imageRect.width,
					height = buttonRect.height
				};

				var descriptionContent = result.Description;
				if (!(descriptionContent == null || string.IsNullOrEmpty(descriptionContent.text)))
				{
					titleRect.height = buttonRect.height * 0.6f;

					var descriptionRect = new Rect(titleRect);
					descriptionRect.yMin = titleRect.yMax;
					descriptionRect.yMax = buttonRect.yMax;
					GUI.Label(descriptionRect, result.Description, UFStyles.Description);
				}

				GUI.Box(imageRect, content.image, GUI.skin.button);
				GUI.Label(titleRect, content.text, UFStyles.Title);
			}
		}
	}
}
