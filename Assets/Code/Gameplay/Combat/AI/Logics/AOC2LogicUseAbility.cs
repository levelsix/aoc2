using UnityEngine;
using System.Collections;

public class AOC2LogicUseAbility : AOC2LogicState {
	
	private AOC2Unit _unit;
	
	private AOC2Ability _ability;
	
	private bool _isEnemy;
	
	public AOC2LogicUseAbility(AOC2Unit unit, AOC2Ability abil, bool enemy)
		: base()
	{
		_ability = abil;
		_unit = unit;
	}
	
	/// <summary>
	/// Logic this instance.
	/// Uses the given ability, including waiting for cast time.
	/// </summary>
	protected override IEnumerator Logic ()
	{
		while(true)
		{
			//Wait for the cast time if there is one
			if (_ability.castTime > 0)
			{
				yield return new WaitForSeconds(_ability.castTime);
			}
			
			while(!_ability.Use(_unit, _unit.aPos.position, _unit.targetPos.position))
			{
				yield return null;
			}
			
			complete = true;
			yield return null;
		}
	}
	
}
