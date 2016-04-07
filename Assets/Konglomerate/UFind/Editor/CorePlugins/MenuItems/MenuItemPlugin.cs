using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEditor;

namespace UFind
{
	public class MenuItemPlugin : UFPlugin
	{
		#region implemented abstract members of UFPlugin
		protected override IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			var list = new List<IFinderResult>();

			var types = Assembly.GetCallingAssembly().GetTypes();
			foreach (var type in types)
			{
				foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
				{
					var item = Attribute.GetCustomAttribute(method, typeof(MenuItem)) as MenuItem;
					if (item != null)
					{
						if (item.menuItem.Contains("Window/UFind"))
						{
							continue;
						}

						if (context.GetIsMatchForTerm(item.menuItem))
						{
							list.Add(new MenuItemResult(item, method));
						}
					}
				}
			}

			return list;
		}

		public override string Name { get { return "Menu Items"; } }
		#endregion
	}
}