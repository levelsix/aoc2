using UnityEngine;
using System.Collections;

public class AOC2ExitAttackOnCooldown : AOC2ExitLogicState {
	
	/// <summary>
	/// The attack to check the cooldown for
	/// </summary>
	private readonly AOC2Attack _attack;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitAttackOnCooldown"/> class.
	/// </summary>
	/// <param name='attack'>
	/// Attack reference to check
	/// </param>
	public AOC2ExitAttackOnCooldown(AOC2Attack attack, AOC2LogicState state)
		: base(state)
	{
		_attack = attack;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		return _attack.onCool;
	}
	
}
