using UnityEngine;
using System.Collections;

public abstract class AOC2MoveAbility : MonoBehaviour {

	abstract public void Move(AOC2Unit _unit, AOC2Position _target, float speed);
}
