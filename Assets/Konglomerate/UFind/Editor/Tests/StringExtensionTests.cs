using NUnit.Framework;
using System;

namespace UFind
{
	public class StringExtensionTests
	{
		const string CONTROL = "The quick brown fox jumped over the lazy dog";

		[Test]
		public void LowerCaseInsensitiveMatch_IsTrue()
		{
			var result = CONTROL.CaseInsensitiveContains("the");
			Assert.IsTrue(result);
		}

		[Test]
		public void UpperCaseInsensitiveContains_IsTrue()
		{
			var result = CONTROL.CaseInsensitiveContains("THE");
			Assert.IsTrue(result);
		}

		[Test(Description = "Compares the control string to a string which is not contained to assure there are no false positives")]
		public void LowerCaseInsensitiveContains_IsFalse()
		{
			var result = CONTROL.CaseInsensitiveContains("you shall not pass");
			Assert.IsFalse(result);
		}

		[Test(Description = "Compares the control string to a string which is not contained to assure there are no false positives")]
		public void UpperCaseInsensitiveContains_IsFalse()
		{
			var result = CONTROL.CaseInsensitiveContains("YOU SHALL NOT PASS");
			Assert.IsFalse(result);
		}
	}
}
