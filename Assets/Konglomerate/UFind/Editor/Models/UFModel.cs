using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UFind
{
	static class UFModel
	{
		#region Constructors
		static UFModel()
		{
			Context = new UFContext();
			Plugins = ActivatePlugins();
		}
		#endregion

		#region Properties
		internal static LinkedList<UFPlugin> Plugins { get; private set; }

		internal static Vector2 ResultListViewScrollPosition { get; set; }

		internal static UFContext Context { get; private set; }
		
		internal static int ResultCount { get; private set; }

		internal static UFWindow Window { get; set; }
		#endregion

		#region Actions
		internal static void SelectResult(IFinderResult result)
		{
			Context.CurrentResult = result;
		}

		internal static void SelectPreviousResult()
		{
			var result = Context.CurrentResult;
			if (result != null)
			{
				var plugin = GetPluginForResult(result);
				var index = plugin.Results.IndexOf(result);
				if (index > 0)
				{
					SelectResult(plugin.Results[index - 1]);
				}
				else
				{
					UFPlugin previous = GetPreviousPlugin(plugin);
					if (previous != null)
					{
						SelectResult(previous.Results[previous.Results.Count - 1]);
					}
				}
			}
		}

		internal static void SelectNextResult()
		{
			var result = Context.CurrentResult;
			if (result != null)
			{
				var plugin = GetPluginForResult(result);
				var index = plugin.Results.IndexOf(result);
				if (index < plugin.Results.Count - 1)
				{
					SelectResult(plugin.Results[index + 1]);
				}
				else
				{
					UFPlugin next = GetNextPlugin(plugin);
					if (next != null)
					{
						SelectResult(next.Results[0]);
					}
				}
			}
		}

		static UFPlugin GetNextPlugin(UFPlugin origin)
		{
			var next = Plugins.Find(origin).Next;
			if (next != null)
			{
				var value = next.Value;
				return value.Results.Count == 0
					? GetNextPlugin(value)
					: value;
			}
			return null;
		}

		static UFPlugin GetPreviousPlugin(UFPlugin origin)
		{
			var previous = Plugins.Find(origin).Previous;
			if (previous != null)
			{
				var value = previous.Value;
				return value.Results.Count == 0
					? GetPreviousPlugin(value)
					: value;
			}
			return null;
		}

		internal static void UpdateResults()
		{
			ResultCount = 0;
			foreach (var plugin in Plugins)
			{
				plugin.GenerateResults(Context);
				ResultCount += plugin.Results.Count;
			}

			var sortedPlugins = Plugins.OrderByDescending(p => p.Score);
			Plugins = new LinkedList<UFPlugin>(sortedPlugins);

			var selectedPlugin = Plugins.FirstOrDefault(p => p.Results.Count > 0);
			Context.CurrentResult = selectedPlugin != null 
				? selectedPlugin.Results.FirstOrDefault()
				: null;
		}
		#endregion

		#region Private
		static UFPlugin GetPluginForResult(IFinderResult result)
		{
			foreach (var plugin in Plugins)
			{
				if (plugin.Results.Contains(result))
					return plugin;
			}
			return null;
		}

		static LinkedList<UFPlugin> ActivatePlugins()
		{
			var list = new LinkedList<UFPlugin>();
			foreach (var type in FindPlugins())
			{
				var plugin = (UFPlugin)Activator.CreateInstance(type);
				list.AddLast(plugin);
			}
			return list;
		}

		static IEnumerable<Type> FindPlugins()
		{
			var assembly = Assembly.GetExecutingAssembly();
			return assembly.GetTypes().ToList().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(UFPlugin)));
		}
		#endregion
	}
}
