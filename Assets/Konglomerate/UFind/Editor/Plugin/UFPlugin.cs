using UFind;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UFind
{
	public abstract class UFPlugin : IFinderPlugin
	{
		#region IFinderPlugin implementation
		/// <summary>
		/// Gets the plugin name for display in the result list view.
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// Gets the aggregate score of the plugin's current results.
		/// </summary>
		public int Score
		{
			get { return results.Sum(r => r.Score); }
		}
		#endregion

		#region IGenerateResultCollection implementation
		/// <summary>
		/// Defines the label text for the header in the result list view.
		/// </summary>
		public ReadOnlyCollection<IFinderResult> Results { get { return results.AsReadOnly(); } }

		/// <summary>
		/// Generates results for the plugin.
		/// Called internally by UFind; you should never need to call this yourself.
		/// </summary>
		public void GenerateResults(IFinderContext context)
		{
			results.Clear();
			if (context.Query.Value.Length > 0)
			{
				var generatedResults = context.IsSlashCommand
					? GetCommandResults(context)
					: GetObjectResults(context);

				if (generatedResults != null)
				{
					results.AddRange(generatedResults);
				}
			}

			results = results.OrderByDescending(r => r.Score).ToList();
		}
		#endregion

		#region Protected
		/// <summary>
		/// Used to generate a list of object results (things).
		/// </summary>
		protected virtual IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			return null;
		}

		/// <summary>
		/// Used to generate a list of command results (actions).
		/// </summary>
		protected virtual IEnumerable<IFinderResult> GetCommandResults(IFinderContext context)
		{
			return null;
		}
		#endregion

		#region Private
		List<IFinderResult> results = new List<IFinderResult>();
		#endregion
	}
}
