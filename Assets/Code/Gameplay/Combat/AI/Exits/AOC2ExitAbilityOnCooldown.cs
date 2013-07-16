using UnityEngine;
using System.Collections;

public class AOC2ExitAbilityOnCooldown : AOC2ExitLogicState {
	
	/// <summary>
	/// The attack to check the cooldown for
	/// </summary>
	private readonly AOC2Ability _ability;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitAbilityOnCooldown"/> class.
	/// </summary>
	/// <param name='ability'>
	/// Attack reference to check
	/// </param>
	public AOC2ExitAbilityOnCooldown(AOC2Ability ability, AOC2LogicState state)
		: base(state)
	{
		_ability = ability;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		return _ability.onCool;
	}
	
}
