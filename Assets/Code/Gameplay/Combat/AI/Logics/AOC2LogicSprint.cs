using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Similiar to the MoveTowardTarget logic, however calls the unit's
/// Sprint() function in order to move faster.
/// </summary>
public class AOC2LogicSprint : AOC2LogicState {
	
	/// <summary>
	/// Constant Minimum Move Distance.
	/// If the distance between this and a target squared is
	/// less than this, the logic will be complete. 
	/// </summary>
	private const float MIN_MOVE_DIST_SQR = .5f;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicMoveTowardTarget"/> class.
	/// </summary>
	/// <param name='thisUnit'>
	/// This unit.
	/// </param>
	public AOC2LogicSprint(AOC2Unit thisUnit)
		: base(thisUnit)
	{
	}
	
	/// <summary>
	/// Moves this unit towards its target.
	/// </summary>
	public override IEnumerator Logic ()
	{
		while (true)
		{
			_user.Sprint(_user.targetPos.position - _user.aPos.position);
			_complete = AOC2Math.GroundDistanceSqr(_user.aPos.position, _user.targetPos.position) < MIN_MOVE_DIST_SQR;
			yield return null;
		}
	}
}
