using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Exit condition for there being a valid player target within range
/// </summary>
public class AOC2ExitTargetInRange : AOC2ExitLogicState {
	
	/// <summary>
	/// The unit running this logic.
	/// Set at constructor.
	/// </summary>
	private readonly AOC2Unit _thisUnit;
	
	/// <summary>
	/// The squared range to check if a target is within.
	/// Set at constructor.
	/// </summary>
	private readonly float _rangeSqr;
	
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
	public AOC2ExitTargetInRange(AOC2LogicState state, AOC2Unit unit, float range) 
		: base(state)
	{
		_thisUnit = unit;
		_rangeSqr = range * range;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		if (_thisUnit.targetPos != null)
		{
			return (AOC2Math.GroundDistanceSqr(_thisUnit.aPos.position, _thisUnit.targetPos.position) < _rangeSqr);
		}
		return false;
	}
	
}
