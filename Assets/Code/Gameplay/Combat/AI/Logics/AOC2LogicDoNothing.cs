using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// The logic state for a unit doing nothing.
/// Can be modified with a longer timer to put less
/// computational strain on the AI system.
/// The DoNothing state is useful in creating player logics
/// that put the player in a state where the unit is waiting
/// for the next player input, so this state is used as an
/// end state for most logics.
/// </summary>
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
