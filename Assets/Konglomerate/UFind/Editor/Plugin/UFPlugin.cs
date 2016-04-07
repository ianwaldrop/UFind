using UFind;
using System.Collections.Generic;

namespace UFind
{
	public abstract class UFPlugin : IGenerateResultCollection, IFinderPlugin
	{
		public int Score
		{
			get { return results.Sum(r => r.Score); }
		}

		#region IFinderPlugin implementation
		public abstract string Name { get; }
		#endregion

		#region IGenerateResultCollection implementation
		public IResultCollection Results { get { return results; } }

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
					Results.AddRange(generatedResults);
				}
			}
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
		readonly IResultCollection results = new UFResultCollection();
		#endregion
	}
}
