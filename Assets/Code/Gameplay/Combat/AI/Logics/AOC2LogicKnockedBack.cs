using UnityEngine;
using System.Collections;

public class AOC2LogicKnockedBack : AOC2LogicState {
	
	float _force;
	
	Vector3 _dir;
	
	const float MIN_KNOCKBACK = 2f;
	
	/// <summary>
	/// Constant knockback coefficient decay per second.
	/// </summary>
	const float KNOCKBACK_DECAY_PER_SEC = .8f;
	
	public AOC2LogicKnockedBack(AOC2Unit unit, float force, Vector3 dir) : base(unit)
	{
		_force = force / _user.mass;
		_dir = dir;
	}
	
	public override void Init ()
	{
		_user.currentLogicState = "KnockedBack";
		base.Init ();
	}
	
	public override IEnumerator Logic ()
	{
		while(_force > MIN_KNOCKBACK)
		{
			//Debug.Log("Knock back force: " + _force);
			_user.Move(_dir, _force, false);
			
			_force -= _force * KNOCKBACK_DECAY_PER_SEC * Time.deltaTime;
			
			yield return null;
		}
		_complete = true;
	}
}
