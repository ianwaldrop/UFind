using UFind;
using UnityEngine;
using NUnit.Framework;

public class ComponentPluginTest
{
	[SetUp]
	public void Setup()
	{
		var camera = Object.FindObjectOfType<Camera>();
		if (camera == null)
		{
			var go = new GameObject("Main Camera");
			go.AddComponent<Camera>();
		}

		plugin = new ComponentPlugin();
		context = new UFContext();
	}

	[TearDown]
	public void Teardown()
	{
		context.Query = string.Empty;
	}

	UFContext context;

	[Test]
	public void LowerCase_IsNotNull_AndGreaterThanZero()
	{
		context.Query = "cam";
		plugin.GenerateResults(context);
		Assert.IsNotNull(plugin.Results);
		Assert.Greater(plugin.Results.Count, 0);
	}

	[Test]
	public void TestUpper_IsNotNull_AndGreaterThanZero()
	{
		context.Query = "Cam";
		plugin.GenerateResults(context);
		Assert.IsNotNull(plugin.Results);
		Assert.Greater(plugin.Results.Count, 0);
	}

	[Test]
	public void NoInput_IsNotNull()
	{
		context.Query = string.Empty;
		plugin.GenerateResults(context);
		Assert.IsNotNull(plugin.Results);
	}

	[Test]
	public void SlashCommand_IsNullAndZeroResults()
	{
		context.Query = "/";
		plugin.GenerateResults(context);
		Assert.IsNotNull(plugin.Results);
		Assert.AreEqual(plugin.Results.Count, 0);
	}

	ComponentPlugin plugin;
}
