using UFind;
using UnityEngine;
using UnityEditor.AnimatedValues;
using UnityEditor;

namespace UFind
{
	class UFWindow : EditorWindow
	{
		const float SEARCH_BAR_HEIGHT = 46;
		const float MAX_WINDOW_HEIGHT = 512;
		const float WINDOW_WIDTH = 600;

		#region Unity
		void OnFocus()
		{
			position = WindowPosition;
		}

		void OnLostFocus()
		{
			UFController.CloseWindow();
		}

		void OnGUI()
		{
			UFController.ProcessEvent(Event.current);

			GUI.skin = UFStyles.Skin;
			UFGui.SearchBar();
			if (UFModel.ResultCount > 0)
			{
				UFGui.ResultList(WINDOW_WIDTH);
//				using (var h = new GUILayout.HorizontalScope())
//				{
//					UFGui.ResultList(WINDOW_WIDTH * 0.4f);
//					if (UFModel.Context.CurrentResult != null)
//					{
//						UFGui.DetailView(UFModel.Context.CurrentResult, WINDOW_WIDTH * 0.6f);
//					}
//				}
			}
			GUI.skin = null;

			position = WindowPosition;
			Repaint();

		}
		#endregion

		#region Private
		readonly AnimFloat windowHeight = new AnimFloat(SEARCH_BAR_HEIGHT) { speed = 10 };

		Rect WindowPosition
		{
			get
			{
				windowHeight.target = UFModel.ResultCount > 0 ? MAX_WINDOW_HEIGHT : SEARCH_BAR_HEIGHT;

				var rect = new Rect(position);
				rect.center = new Vector2(Screen.currentResolution.width, 0) * 0.5f;
				rect.yMin = Screen.currentResolution.height * 0.3f;
				rect.height = windowHeight.value;
				rect.width = WINDOW_WIDTH;
				return rect;
			}
		}
		#endregion
	}
}
