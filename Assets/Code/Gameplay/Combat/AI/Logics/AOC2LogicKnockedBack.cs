using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Logic for a unit getting knocked back. Decays the force over time
/// </summary>
public class AOC2LogicKnockedBack : AOC2LogicState {
	
	/// <summary>
	/// The force
	/// </summary>
	float _force;
	
	/// <summary>
	/// The direction of the knockback
	/// </summary>
	Vector3 _dir;
	
	/// <summary>
	/// Constant for the minimum amount of knockback 
	/// </summary>
	const float MIN_KNOCKBACK = 2f;
	
	/// <summary>
	/// Constant knockback coefficient decay per second.
	/// </summary>
	const float KNOCKBACK_DECAY_PER_SEC = .8f;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicKnockedBack"/> class.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	/// <param name='force'>
	/// Force.
	/// </param>
	/// <param name='dir'>
	/// Direction.
	/// </param>
	public AOC2LogicKnockedBack(AOC2Unit unit, float force, Vector3 dir) : base(unit)
	{
		_force = force / _user.mass;
		_dir = dir;
	}
	
	/// <summary>
	/// Initialize this instance. Sets the debug string.
	/// </summary>
	public override void Init ()
	{
		_user.currentLogicState = "KnockedBack";
		base.Init ();
	}
	
	/// <summary>
	/// Moves the user in the knockback direction
	/// </summary>
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
