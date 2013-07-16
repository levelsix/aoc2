using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Exit state that tests true when a particular attack is on cooldown
/// </summary>
public class AOC2ExitAbilityOffCooldown : AOC2ExitLogicState {
	
	/// <summary>
	/// The attack to check the cooldown for
	/// </summary>
	private readonly AOC2Ability _ability;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitAbilityOffCooldown"/> class.
	/// </summary>
	/// <param name='abil'>
	/// Attack reference to check
	/// </param>
	public AOC2ExitAbilityOffCooldown(AOC2Ability abil, AOC2LogicState state)
		: base(state)
	{
		_ability = abil;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		return !_ability.onCool;
	}
}
