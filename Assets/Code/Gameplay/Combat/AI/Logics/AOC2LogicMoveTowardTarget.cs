using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Logic state for moving towards a target
/// </summary>
public class AOC2LogicMoveTowardTarget : AOC2LogicState {
	
	/// <summary>
	/// Constant Minimum Move Distance.
	/// If the distance between this and a target squared is
	/// less than this, the logic will be complete. 
	/// </summary>
	private const float MIN_MOVE_DIST_SQR = .005f;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicMoveTowardTarget"/> class.
	/// </summary>
	/// <param name='thisUnit'>
	/// This unit.
	/// </param>
	public AOC2LogicMoveTowardTarget(AOC2Unit thisUnit)
		: base(thisUnit)
	{
	}
	
	public override void OnExitState()
	{
		_user.model.SetAnimationFlag(AOC2Values.Animations.Anim.WALK, false);
	}

	/// <summary>
	/// Moves this unit towards its target.
	/// </summary>
	public override IEnumerator Logic ()
	{
		_user.model.SetAnimationFlag(AOC2Values.Animations.Anim.WALK, true);
		while (true)
		{
			_user.Move(_user.targetPos.position - _user.aPos.position);
			_complete = AOC2Math.GroundDistanceSqr(_user.aPos.position, _user.targetPos.position) < MIN_MOVE_DIST_SQR;
			yield return null;
		}
	}
}
