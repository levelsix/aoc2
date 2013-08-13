using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Logic for using a character ability. Waits for cast time before
/// activating the ability.
/// </summary>
public class AOC2LogicUseAbility : AOC2LogicState {
	
	/// <summary>
	/// The ability.
	/// </summary>
	private AOC2Ability _ability;
	
	AOC2LogicState moveLogic;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicUseAbility"/> class.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	/// <param name='abil'>
	/// Ability.
	/// </param>
	/// <param name='enemy'>
	/// Enemy flag.
	/// </param>
	public AOC2LogicUseAbility(AOC2Unit unit, AOC2Ability abil)
		: base(unit)
	{
		_ability = abil;
	}

	public override void OnExitState()
	{
		_user.model.SetAnimationFlag(_ability.animation, false);
		
		base.OnExitState();
	}
	
	public override void Init ()
	{
		_user.currentLogicState = "Use Ability";
		
		if (_user.model.usesTriggers)
		{
			_user.model.SetAnimationFlag(_ability.animation, true);
			_user.model.SetAnimSpeed(1f + _user.stats.attackSpeed/100f);
			canBeInterrupt = false;
		}
		
		Vector3 dir = (_user.targetPos.position - _user.aPos.position).normalized;
		
		if (dir != Vector3.zero)
		{
			_user.trans.forward = dir;
		}
		
		base.Init();
	}
	
	public override void OnAbilityUseFrame ()
	{
		_ability.Use(_user, _user.aPos.position, _user.targetPos.position);
		base.OnAbilityUseFrame();
	}
	
	public override void OnAnimationEnd ()
	{
		_complete = true;
		canBeInterrupt = true;
		base.OnAnimationEnd();
	}
	
	/// <summary>
	/// Logic this instance.
	/// Uses the given ability, including waiting for cast time.
	/// </summary>
	public override IEnumerator Logic ()
	{	
		if (!_user.model.usesTriggers)
		{
			while(true)
			{
	            while(_ability.onCool)
	            {
	                yield return null;  
	            }
	            
				//Wait for the cast time if there is one
				if (_ability.castTime > 0)
				{
					yield return new WaitForSeconds(_ability.castTime * AOC2Math.AttackSpeedMod(_user.stats.attackSpeed));
				}
				
				while(!_ability.Use(_user, _user.aPos.position, _user.targetPos.position))
				{
					yield return null;
				}
				
				_complete = true;
			}
		}
	}
	
}
