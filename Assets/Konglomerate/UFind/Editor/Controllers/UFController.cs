using UnityEditor;
using UnityEngine;

namespace UFind
{
	public static class UFController
	{
		#region Actions
		[MenuItem("Window/UFind %.")]
		public static void ShowWindow()
		{
			UFModel.UpdateResults();
			UFModel.Window = ScriptableObject.CreateInstance<UFWindow>();
			UFModel.Window.Show();
		}

		public static void CloseWindow()
		{
			if (UFModel.Window != null)
			{
				UFModel.Window.Close();
			}
		}

		public static void UpdateQuery(string query)
		{
			UFModel.Context.Query = query;
			UFModel.UpdateResults();
		}

		public static void ProcessEvent(Event current)
		{

			// this section was for selecting a result by number; let's number them
			// before we worry about how to select them
//			if (char.IsNumber(current.character))
//			{
//				var index = int.Parse(current.character.ToString()) - 1;
//				Debug.Log(string.Format("char {0} => index {1}", current.character, index));
//				UFModel.Results.SelectAtIndex(index);
//				current.Use();
//				return;
//			}

			switch (current.keyCode)
			{
				case KeyCode.Escape:
					UFController.CloseWindow();
					current.Use();
					break;

				case KeyCode.UpArrow:
					UFModel.SelectPreviousResult();
					current.Use(); 
					break;

				case KeyCode.DownArrow:
					UFModel.SelectNextResult();
					current.Use();
					break;

				case KeyCode.KeypadEnter:
				case KeyCode.Return:
					UFModel.Context.CurrentResult.Execute(UFModel.Context);
					CloseWindow();
					current.Use();
					break;
			}
		}
		#endregion
	}
}
