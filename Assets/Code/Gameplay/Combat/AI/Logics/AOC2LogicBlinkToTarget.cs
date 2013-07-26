using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Logic state where the unit waits for a cast time, then immediately
/// moves to its target, followed by a delay before the next check
/// for exit states.
/// </summary>
public class AOC2LogicBlinkToTarget : AOC2LogicState {

	private AOC2Unit _unit;
	
	private float delayBef;
	
	private float delayAft;
	
	public AOC2LogicBlinkToTarget(AOC2Unit thisUnit, float delayBefore, float delayAfter)
		: base()
	{
		_unit = thisUnit;
		delayAft = delayAfter;
		delayBef = delayBefore;
	}
	
	public override IEnumerator Logic ()
	{
		while (true)
		{
			yield return new WaitForSeconds(delayBef);
			_unit.aPos.position = _unit.targetPos.position;
			yield return new WaitForSeconds(delayAft);
			_complete = true;
			yield return null;
		}
	}
}
