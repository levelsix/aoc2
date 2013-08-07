using UnityEngine;
using System.Collections;

public class AOC2LogicBullRush : AOC2LogicCombatRoll {
	
	AOC2Attack _attack;
	
	public AOC2LogicBullRush(AOC2Unit unit, AOC2Attack attack, float range, float waitAfter, float speed)
		: base(unit, range, waitAfter, speed)
	{
		_attack = attack;
	}
	
	public override IEnumerator Logic ()
	{
		_attack.Use(_user, _user.aPos.position, _user.targetPos.position);
		
		yield return base.Logic ();
	}
}
