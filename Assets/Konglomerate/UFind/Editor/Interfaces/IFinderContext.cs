using UFind;
using UnityEditor;

namespace UFind
{
	public interface IFinderContext
	{
		IFinderResult CurrentResult { get; }

		bool IsSlashCommand { get; }

		UFQuery Query { get; }

		int GetMatchScoreForTerm(string term);

		bool GetIsMatchForTerm(string term);
	}

	public struct UFQuery
	{
		const string EPKEY_UFIND_QUERY = "ufind-query";

		public string Value { get { return EditorPrefs.GetString(EPKEY_UFIND_QUERY, string.Empty); } }

		public string Lower { get { return Value.ToLower(); } }

		internal UFQuery(string query)
		{
			EditorPrefs.SetString(EPKEY_UFIND_QUERY, query);
		}

		public static implicit operator UFQuery(string query)
		{
			return new UFQuery(query);
		}
	}
}
