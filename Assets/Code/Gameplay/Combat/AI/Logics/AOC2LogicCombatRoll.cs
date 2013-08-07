using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Moves a unit in a single direction a set distance,
/// then takes a pause 
/// </summary>
public class AOC2LogicCombatRoll : AOC2LogicState {
	
	float _distance;
	
	float _waitAfter;
	
	float _speed;
	
	Vector3 _dir;
	
	Vector3 _target;
	
	const float MIN_DIST = .1f;
	
	NavMeshHit dummy = new NavMeshHit();
	
	public AOC2LogicCombatRoll(AOC2Unit unit, float range, float pauseAfter, float speed)
		:base(unit)
	{
		_distance = range;
		_waitAfter = pauseAfter;
		_speed = speed;
	}
	
	public override void Init ()
	{
		base.Init ();
		
		_user.currentLogicState = "Roll";
		
		_dir = (_user.targetPos.position - _user.aPos.position).normalized;
		
		//Set targetposition so that we don't keep moving after the roll, but we're really just going to
		//use the local _target so that the roll target cannot be modified externally
		NavMesh.SamplePosition(_user.aPos.position + _dir * _distance, out dummy, 10f, -1);
		_user.targetPos.position = _target = dummy.position;
		
		_user.model.SetAnimationFlag(AOC2Values.Animations.Anim.WALK, true);
	}
	
	public override void OnExitState ()
	{
		_user.model.SetAnimationFlag(AOC2Values.Animations.Anim.WALK, true);
		base.OnExitState ();
	}
	
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
