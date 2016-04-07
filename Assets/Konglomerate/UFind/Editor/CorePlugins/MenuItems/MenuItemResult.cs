using UnityEngine;
using System.Reflection;
using UnityEditor;

namespace UFind
{
	public class MenuItemResult : UFResult
	{
		public readonly MethodInfo method;
		public readonly MenuItem item;

		public MenuItemResult(MenuItem item, MethodInfo method)
		{
			this.method = method;
			this.item = item;
		}

		#region implemented abstract members of UFResult
		public override GUIContent Description { get { return null; } }

		public override string Title { get { return item.menuItem; } }

		public override void Execute(IFinderContext context)
		{
			method.Invoke(null, null);
		}

		protected override Object UnityObject
		{
			get { return new TextAsset(); }
		}
		#endregion
	}
}