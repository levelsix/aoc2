using UnityEngine;
using System.Collections;

public class AOC2LogicDoNothing : AOC2LogicState {

	protected override IEnumerator Logic ()
	{
		while (true)
		{
			complete = true;
			yield return null;
		}
	}
}
