using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// The logic state for a unit doing nothing.
/// Can be modified with a longer timer to put less
/// computational strain on the AI system.
/// The DoNothing state is useful in creating player logics
/// that put the player in a state where the unit is waiting
/// for the next player input, so this state is used as an
/// end state for most logics.
/// </summary>
public class AOC2LogicDoNothing : AOC2LogicState {
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicDoNothing"/> class.
	/// </summary>
	/// <param name='user'>
	/// User.
	/// </param>
	public AOC2LogicDoNothing(AOC2Unit user) : base(user)
	{
	}
	
	/// <summary>
	/// Initilize this instance by setting animation.
	/// </summary>
	public override void Init ()
	{
		_user.currentLogicState = "Idle";
		_user.model.SetAnimation(AOC2Values.Animations.Anim.IDLE, true);
		base.Init ();
	}
	
	/// <summary>
	/// Logic this instance.
	/// Does nothing
	/// </summary>
	public override IEnumerator Logic ()
	{
		while (true)
		{
			_complete = true;
			yield return null;
		}
	}
}
