using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Exit condition for this unit's target being within range
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
			return Test(_thisUnit.targetPos.position);
		}
		return false;
	}
	
	public bool Test(Vector3 pos)
	{
		return AOC2Math.GroundDistanceSqr(_thisUnit.aPos.position, pos) < _rangeSqr;
	}
	
}
