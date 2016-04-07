using NUnit.Framework;

namespace UFind
{
	public class IntExtensionTests
	{
		const int LOWER_BOUND = 0;
		const int UPPER_BOUND = 10;

		[Test(Description = "Numbers smaller than the range should be wrapped to the upper bound")]
		public void WrapSmallNumber_IsEqualToUpperBound()
		{
			var testNumber = -10;
			var result = testNumber.Wrap(LOWER_BOUND, UPPER_BOUND);
			Assert.AreEqual(result, UPPER_BOUND);
		}

		[Test(Description = "Numbers larger than the range should be wrapped to the lower bound")]
		public void WrapLargeNumber_IsEqualToLowerBound()
		{
			var testNumber = 20;
			var result = testNumber.Wrap(LOWER_BOUND, UPPER_BOUND);
			Assert.AreEqual(result, LOWER_BOUND);
		}

		[Test(Description = "Numbers which fall within the lower and upper bounds should remain unchanged")]
		public void ContainedNumber_IsEqualToStartingValue()
		{
			const int INITIAL_VALUE = 5;
			var testNumber = INITIAL_VALUE;
			var result = testNumber.Wrap(LOWER_BOUND, UPPER_BOUND);
			Assert.AreEqual(result, INITIAL_VALUE);
		}
	}
}
