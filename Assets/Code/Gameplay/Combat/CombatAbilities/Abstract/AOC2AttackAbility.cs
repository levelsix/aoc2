using UnityEngine;
using System.Collections;

[System.Serializable]
public class AOC2AttackAbility : AOC2Ability {

	private AOC2Attack _attack;
	
	public override float range {
		get {
			return _attack.range;
		}
	}
	
	public AOC2AttackAbility(AOC2Attack attack, string abName, float cast, float cool)
		: base(abName, cast, cool)
	{
		_attack = attack;
	}
	
	public override bool Use (AOC2Unit user, Vector3 origin, Vector3 target)
	{
		if (!_onCool)
		{
			_attack.Use(user, origin, target);
			AOC2ManagerReferences.combatManager.CoolAbility(this);
			return true;
		}
		return false;
	}
}
