using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Moves a unit in a single direction a set distance,
/// then takes a pause 
/// </summary>
public class AOC2LogicCombatRoll : AOC2LogicState {
	
	/// <summary>
	/// The distance that the character rolls
	/// </summary>
	float _distance;
	
	/// <summary>
	/// The wait after the roll before another action can be taken
	/// </summary>
	float _waitAfter;
	
	/// <summary>
	/// The speed that the user moves during the roll
	/// </summary>
	float _speed;
	
	/// <summary>
	/// The direction that the user is moving
	/// </summary>
	Vector3 _dir;
	
	/// <summary>
	/// The position that the user will be at at the end of the roll
	/// </summary>
	Vector3 _target;
	
	/// <summary>
	/// The minimum distance from the target from which the user will be considered
	/// to be at the target position
	/// </summary>
	const float MIN_DIST = .1f;
	
	/// <summary>
	/// The point on the nav mesh which is closest to where the user should be.
	/// </summary>
	NavMeshHit point = new NavMeshHit();
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicCombatRoll"/> class.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	/// <param name='range'>
	/// Range.
	/// </param>
	/// <param name='pauseAfter'>
	/// Pause after.
	/// </param>
	/// <param name='speed'>
	/// Speed.
	/// </param>
	public AOC2LogicCombatRoll(AOC2Unit unit, float range, float pauseAfter, float speed)
		:base(unit)
	{
		_distance = range;
		_waitAfter = pauseAfter;
		_speed = speed;
	}
	
	/// <summary>
	/// Initialize this instance; determines target position, direction, and animation
	/// </summary>
	public override void Init ()
	{
		base.Init ();
		
		_user.currentLogicState = "Roll";
		
		_dir = (_user.targetPos.position - _user.aPos.position).normalized;
		
		//Set targetposition so that we don't keep moving after the roll, but we're really just going to
		//use the local _target so that the roll target cannot be modified externally
		NavMesh.SamplePosition(_user.aPos.position + _dir * _distance, out point, 10f, -1);
		_user.targetPos.position = _target = point.position;
		
		_user.model.SetAnimation(AOC2Values.Animations.Anim.WALK, true);
	}
	
	/// <summary>
	/// Stops the animation after the dash is over
	/// </summary>
	public override void OnExitState ()
	{
		_user.model.SetAnimation(AOC2Values.Animations.Anim.WALK, false);
		base.OnExitState ();
	}
	
	/// <summary>
	/// Logic this instance.
	/// Moves the user towards the target at speed until they are there, then stalls them
	/// for the waitAfter time
	/// </summary>
	public override IEnumerator Logic ()
	{
		canBeInterrupt = false;
		
		while (AOC2Math.GroundDistanceSqr(_user.aPos.position, _target) > MIN_DIST)
		{
			_user.Move(_target - _user.aPos.position, _speed);
			yield return null;
		}
	
		yield return new WaitForSeconds(_waitAfter);
		canBeInterrupt = true;		
		_complete = true;
		yield return null;
	}
	
}
