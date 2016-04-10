using UFind;
using UnityEditor;

namespace UFind
{
	public interface IFinderContext
	{
		/// <summary>
		/// Gets the user's currently selected result.
		/// </summary>
		/// <value>The user's currently selected result.</value>
		UFResult SelectedResult { get; }

		/// <summary>
		/// Gets a value indicating whether the current query is a slash command.
		/// </summary>
		/// <value><c>true</c> if this current query is slash command; otherwise, <c>false</c>.</value>
		bool IsSlashCommand { get; }

		/// <summary>
		/// Gets the current query.
		/// </summary>
		/// <value>The current query.</value>
		UFQuery Query { get; }

		/// <summary>
		/// Gets a value indicating whether the given term is a match for the current query.
		/// </summary>
		/// <returns><c>true</c>, if the given term is a match for the current query, <c>false</c> otherwise.</returns>
		/// <param name="term">Term.</param>
		bool GetIsMatchForTerm(string term);
	}

	public struct UFQuery
	{
		const string EPKEY_UFIND_QUERY = "ufind-query";

		/// <summary>
		/// The actual quey value as input by the user.
		/// </summary>
		public string Value { get { return EditorPrefs.GetString(EPKEY_UFIND_QUERY, string.Empty); } }

		/// <summary>
		/// Gets the query transformed to lower case.
		/// </summary>
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
