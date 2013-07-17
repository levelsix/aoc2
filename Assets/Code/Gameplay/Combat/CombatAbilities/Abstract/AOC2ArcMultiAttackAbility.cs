using UnityEngine;
using System.Collections;

public class AOC2ArcMultiAttackAbility : AOC2AttackAbility {

	int _shots;
	
	float _arc;
	
	public AOC2ArcMultiAttackAbility(AOC2Attack attack, string abName, float cast, 
		float cool, int mana, AOC2Values.Abilities.TargetType target, int shots, float arc)
		: base(attack, abName, cast, cool, mana, target)
	{
		_shots = shots;
		_arc = arc * Mathf.Deg2Rad;
	}
	
	public override bool Use (AOC2Unit user, Vector3 origin, Vector3 target)
	{
		if (!_onCool)
		{
			float angle = Mathf.Atan2(target.z - origin.z, target.x - origin.x);
			float minAngle = angle - _arc/2;
			float maxAngle = angle + _arc/2;
			for (int i = 0; i < _shots; i++) 
			{
				angle = minAngle + i * _arc / _shots;
				Vector3 dir = new Vector3(Mathf.Cos (angle), 0, Mathf.Sin(angle));
				_attack.Use(user, origin, origin + dir); 
			}
			AOC2ManagerReferences.combatManager.CoolAbility(this);
			return true;
		}
		return false;
	}
}
