using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System.Linq;
using UnityEngine;
using System;

namespace UFind
{
	public class MenuItemPlugin : UFPlugin
	{
		public MenuItemPlugin()
		{
			Func<MethodInfo, MenuItem> getMenuItem = m => m.GetCustomAttributes(typeof(MenuItem), true).First(a => a is MenuItem) as MenuItem;

			items = UFUtilities.GetAllTypes()
							   .SelectMany(t => t.GetMethods())
							   .Where(m => Attribute.IsDefined(m, typeof(MenuItem)))
							   .Select(m => new MenuItemResult(getMenuItem(m), m))
							   .ToList();
		}

		public override string Name
		{
			get
			{
				return "Menu Items";
			}
		}

		protected override IEnumerable<UFResult> GetCommandResults(IFinderContext context)
		{
			return items
				.Where(i => i.IsValid)
				.Where(i => context.GetIsMatchForTerm(i.Title))
				.Cast<UFResult>();
		}

		readonly List<MenuItemResult> items;
	}

	public class MenuItemResult : UFResult
	{
		public MenuItemResult(MenuItem menuItem, MethodInfo methodInfo)
		{
			this.methodInfo = methodInfo;
			this.menuItem = menuItem;
			CanOpenAsset = false;
		}

		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		public override GUIContent Content
		{
			get
			{
				return new GUIContent(Title);
			}
		}

		public override GUIContent Description
		{
			get
			{
				return new GUIContent(menuItem.menuItem);
			}
		}

		public override string Title
		{
			get
			{
				return menuItem.menuItem.Split('/').Last();
			}
		}

		public override void Execute(IFinderContext context)
		{
			methodInfo.Invoke(null, null);
		}

		MethodInfo methodInfo;
		MenuItem menuItem;
	}
}
