using UnityEngine;
using System.Collections;

public class AOC2LogicChargeAtTarget : AOC2LogicState {
	
	private AOC2Unit _unit;
	
	public AOC2LogicChargeAtTarget(AOC2Unit thisUnit)
		: base()
	{
		_unit = thisUnit;
	}
	
	protected override IEnumerator Logic ()
	{
		while (true)
		{
			_unit.Move(_unit.targetPos.position - _unit.aPos.position);
			yield return null;
		}
	}
}
