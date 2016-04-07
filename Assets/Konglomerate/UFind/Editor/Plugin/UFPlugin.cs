using UFind;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UFind
{
	public abstract class UFPlugin : IFinderPlugin
	{
		public int Score
		{
			get { return results.Sum(r => r.Score); }
		}

		#region IFinderPlugin implementation
		public abstract string Name { get; }
		#endregion

		#region IGenerateResultCollection implementation
		public ReadOnlyCollection<IFinderResult> Results { get { return results.AsReadOnly(); } }

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
		protected virtual IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			return null;
		}

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
