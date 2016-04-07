using UFind;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

public interface IGenerateResultCollection
{
	IResultCollection Results { get; }

	void GenerateResults(IFinderContext context);
}
