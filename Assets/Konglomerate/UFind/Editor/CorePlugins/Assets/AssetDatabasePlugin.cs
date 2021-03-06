﻿using System.Linq;
using System.Collections.Generic;
using UnityEditor;

namespace UFind
{
	public class AssetDatabasePlugin : UFPlugin
	{
		#region IFinderPlugin implementation
		protected override IEnumerable<IFinderResult> GetObjectResults(IFinderContext context)
		{
			return AssetDatabase.FindAssets(context.Query.Value)
				.Select<string, IFinderResult>(g => new AssetDatabaseResult(g));
		}

		public override string Name
		{
			get { return "Assets"; }
		}
		#endregion
	}
}
