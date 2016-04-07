using System.Linq;
using UnityEngine;
using System.ComponentModel;
using System.Collections.Generic;

namespace UFind
{
	public static class Extensions
	{
		public static string GetPath(this Transform transform, bool sansSelf = false)
		{
			if (transform.parent == null)
			{
				return sansSelf ? null : transform.name;
			}
			if (sansSelf)
			{
				return transform.parent.GetPath();
			}
			return transform.parent.GetPath() + "/" + transform.name;
		}

		public static void To<T>(this object obj, System.Action<T> callback)
		{
			To<T>(obj, callback, null);
		}

		public static void To<T>(this object obj, System.Action<T> callback, System.Action failed)
		{
			if (callback == null)
			{
				throw new System.ArgumentNullException ("callback", "A callback receving the casting result must be supplied.");
			}

			try
			{
				var t = (T)obj;
				if (t.Equals(default(T)))
				{
					if (failed != null)
						failed();
				}
				else if (callback != null)
				{
					callback(t);
				}
			}
			catch
			{
				if (failed != null)
					failed();
			}
		}

		public static int Wrap(this int i, int min, int max)
		{
			return i < min ? max : i > max ? min : i;
		}

		// http://wiki.unity3d.com/index.php?title=EnumExtensions
		public static bool TryParse<T>(this System.Enum theEnum, string valueToParse, out T returnValue)
		{
			returnValue = default(T);
			if (System.Enum.IsDefined(typeof(T), valueToParse))
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
				returnValue = (T)converter.ConvertFromString(valueToParse);
				return true;
			}
			return false;
		}

		// http://stackoverflow.com/questions/4108828/generic-extension-method-to-see-if-an-enum-contains-a-flag
		public static bool HasFlag(this System.Enum variable, System.Enum value)
		{
			if (variable == null)
			{
				// don't throw an argument null exception; a null value just isn't present.
				return false;
			}

			if (value == null)
			{
				throw new System.ArgumentNullException ("value");
			}

			var variableType = variable.GetType();
			if (!System.Enum.IsDefined(variableType, value))
			{
				var message = string.Format("Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.",
				                        value.GetType(), variableType);
				throw new System.ArgumentException (message, "value");
			}

			ulong num = System.Convert.ToUInt64(value);
			return ((System.Convert.ToUInt64(variable) & num) == num);
		}

		// http://damieng.com/blog/2008/04/10/using-linq-to-foreach-over-an-enum-in-c
		public static IEnumerable<T> GetEnumerable<T>(this T e)
		{
			return System.Enum.GetValues(typeof(T)).Cast<T>();
		}

		public static bool CaseInsensitiveContains(this string s, string test)
		{
			return s.IndexOf(test, System.StringComparison.OrdinalIgnoreCase) >= 0;
		}
	}
}
