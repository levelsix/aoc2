using UnityEngine;
using System.Collections;

/// <summary>
/// Simple enemy that chases enemies if they're within a certain range
/// </summary>
public class AOC2UnitLogicHuntTargets : AOC2UnitLogic {
	
	/// <summary>
	/// The range that this Goblin will chase enemies within
	/// </summary>
	public float range = 10;
	
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
	public override void Init ()
	{	
		AOC2LogicState doNothing = new AOC2LogicDoNothing();
		AOC2LogicState chase = new AOC2LogicMoveTowardTarget(_unit);
		AOC2LogicState basicAttack = new AOC2LogicUseAbility(_unit, _unit.basicAttackAbility);
		
		doNothing.AddExit(new AOC2ExitPlayerInRange(chase, _unit, range));
		
		chase.AddExit(new AOC2ExitNoPlayerWithinRange(doNothing, _unit, range));
		chase.AddExit(new AOC2ExitTargetInRange(basicAttack, _unit, _unit.basicAttackAbility.range));
		
		basicAttack.AddExit(new AOC2ExitNoPlayerWithinRange(chase, _unit, _unit.basicAttackAbility.range));
		
		logic = new AOC2HFSMLogic(doNothing);
		
		base.Init();
	}
	
}
