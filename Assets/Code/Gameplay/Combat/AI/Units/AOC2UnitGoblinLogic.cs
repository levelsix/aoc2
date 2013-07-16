using UnityEngine;
using System.Collections;

/// <summary>
/// Simple enemy that chases enemies if they're within a certain range
/// </summary>
public class AOC2UnitGoblinLogic : AOC2UnitLogic {
	
	/// <summary>
	/// The range that this Goblin will chase enemies within
	/// </summary>
	public float range = 10;
	
	/// <summary>
	/// The base attack for this enemy
	/// </summary>
	public AOC2Ability baseAttack;
	
	/// <summary>
	/// Pointer to this unit
	/// </summary>
	private AOC2Unit _unit;
	
	/// <summary>
	/// Awake this instance.
	/// Get component pointer
	/// </summary>
	void Awake()
	{
		_unit = GetComponent<AOC2Unit>();
	}
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start ()
	{
		baseAttack = AOC2AbilityLists.Enemy.baseAttackAbility;
		
		AOC2LogicState doNothing = new AOC2LogicDoNothing();
		AOC2LogicState chase = new AOC2LogicMoveTowardTarget(_unit);
		AOC2LogicState attack = new AOC2LogicUseAbility(_unit, baseAttack, true);
		
		doNothing.AddExit(new AOC2ExitPlayerInRange(chase, _unit, range));
		
		chase.AddExit(new AOC2ExitTargetOutsideRange(doNothing, _unit, range));
		chase.AddExit(new AOC2ExitTargetInRange(attack, _unit, baseAttack.range));
		
		attack.AddExit(new AOC2ExitTargetOutsideRange(chase, _unit, baseAttack.range));
		
		_baseState = doNothing;
	}
	
}
