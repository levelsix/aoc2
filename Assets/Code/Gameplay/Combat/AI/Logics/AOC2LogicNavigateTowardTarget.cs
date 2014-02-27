using UnityEngine;
using System.Collections;

///<summary>
///@author Rob Giusti
///Logic to use Unity's NavMesh navigation to seek out a target
///PRECONDITION: _unit.nav != null
///</summary>
public class AOC2LogicNavigateTowardTarget : AOC2LogicState {
	
	/// <summary>
	/// Constant for the distance between unit and target where the
	/// unit is considered at close enough to the target to be at the target
	/// </summary>
	const float STOP_DIST = .1f;
	
	/// <summary>
	/// Gets a value indicating whether this <see cref="AOC2LogicNavigateTowardTarget"/> is complete.
	/// </summary>
	/// <value>
	/// <c>true</c> if the user is within STOP_DIST from the target position; otherwise, <c>false</c>.
	/// </value>
	override public bool Complete{
		get
		{
			return AOC2Math.GroundDistanceSqr(_user.aPos.position, _user.targetPos.position) < STOP_DIST;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AOC@LogicNavigateTowardTarget"/> class
	/// </summary>
	/// <param name='unit'>
	/// This unit.
	/// </param>
	public AOC2LogicNavigateTowardTarget(AOC2Unit unit)
		: base(unit)
	{
		
	}

	/// <summary>
	/// Sets up the path to the target position along the NavMesh
	/// </summay>
	public override void Init()
	{
		_user.currentLogicState = "Navigate";
		//_unit.nav.SetDestination(_unit.targetPos.position);
		
		_user.nav.speed = _user.GetStat(AOC2Values.UnitStat.MOVE_SPEED);
		
		_user.nav.Resume();
		_user.model.SetAnimation(AOC2Values.Animations.Anim.WALK, true);
	}
	
	/// <summary>
	/// Raises the exit state event.
	/// Stops the walk animation.
	/// </summary>
	public override void OnExitState()
	{
		_user.model.SetAnimation(AOC2Values.Animations.Anim.WALK, false);
		_user.nav.Stop();
	}

	/// <summary>
	/// Moves this unit along its path.
	/// Completes when the status of the  
	/// </summary>
	public override IEnumerator Logic()
	{
		while (true)
		{
			_user.nav.SetDestination(_user.targetPos.position);
			yield return null;
		}
	}

}
