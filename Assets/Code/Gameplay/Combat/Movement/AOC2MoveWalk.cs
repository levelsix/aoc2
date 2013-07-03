using UnityEngine;
using System.Collections;

public class AOC2MoveWalk : AOC2MoveAbility {

	public override void Move(AOC2Unit _unit, AOC2Position _target, float speed)
	{
		_unit.aPos.position += (_target.position - _unit.aPos.position).normalized * speed * Time.deltaTime;
	}
}
