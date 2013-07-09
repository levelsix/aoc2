using UnityEngine;
using System.Collections;

/// <summary>
/// Exit state that tests true when a particular attack is on cooldown
/// </summary>
public class AOC2ExitAttackOffCooldown : AOC2ExitLogicState {
	
	/// <summary>
	/// The attack to check the cooldown for
	/// </summary>
	private readonly AOC2Attack _attack;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitAttackOffCooldown"/> class.
	/// </summary>
	/// <param name='attack'>
	/// Attack reference to check
	/// </param>
	public AOC2ExitAttackOffCooldown(AOC2Attack attack, AOC2LogicState state)
		: base(state)
	{
		_attack = attack;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		return !_attack.onCool;
	}
}
