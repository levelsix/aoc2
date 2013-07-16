using UnityEngine;
using System.Collections;

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
	
	protected override IEnumerator Logic ()
	{
		while (true)
		{
			yield return new WaitForSeconds(delayBef);
			_unit.aPos.position = _unit.targetPos.position;
			yield return new WaitForSeconds(delayAft);
			complete = true;
			yield return null;
		}
	}
}
