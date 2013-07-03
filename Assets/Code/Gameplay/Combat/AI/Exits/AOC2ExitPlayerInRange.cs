using UnityEngine;
using System.Collections;

public class AOC2ExitPlayerInRange : AOC2ExitLogicState {

	/// <summary>
	/// The unit running this logic.
	/// Set at constructor.
	/// </summary>
	private readonly AOC2Unit _thisUnit;
	
	/// <summary>
	/// The range to check if a target is within.
	/// Set at constructor.
	/// </summary>
	private readonly float _range;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitTargetInRange"/> class.
	/// </summary>
	/// <param name='state'>
	/// State to change to when exit conditions are met
	/// </param>
	/// <param name='unit'>
	/// Unit conducting this logic
	/// </param>
	/// <param name='range'>
	/// Range to target within
	/// </param>
	public AOC2ExitPlayerInRange(AOC2LogicState state, AOC2Unit unit, float range) 
		: base(state)
	{
		_thisUnit = unit;
		_range = range;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		AOC2Unit targ = AOC2ManagerReferences.combatManager.GetClosestPlayerInRange(_thisUnit, _range);
		if (targ != null)
		{
			_thisUnit.targetPos = targ.aPos;
			return true;
		}
		return false;
	}
	
}