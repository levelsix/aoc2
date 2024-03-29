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
	
	/// <summary>
	/// Stops the animation when leaving the state
	/// </summary>
	public override void OnExitState()
	{
		_user.model.SetAnimation(_ability.animation, false);
		
		_complete = false;
		
		base.OnExitState();
	}
	
	/// <summary>
	/// Initilzing this instance.
	/// Starts the animation and orients the user appropriately
	/// </summary>
	public override void Init ()
	{
		_user.currentLogicState = _ability.name;
		
		if (_user.model.usesTriggers)
		{
			_user.model.SetAnimation(_ability.animation, true);
			_user.model.SetAnimSpeed((float)(_user.GetStat(AOC2Values.UnitStat.ATTACK_SPEED))/100f);
			canBeInterrupt = false;
		}
		
		Vector3 dir = (_user.targetPos.position - _user.aPos.position).normalized;
		
		if (dir != Vector3.zero)
		{
			_user.trans.forward = dir;
		}
		
		base.Init();
	}
	
	/// <summary>
	/// Uses the ability when the proper frame is triggered
	/// </summary>
	public override void OnAbilityUseFrame ()
	{
		_ability.Use(_user, _user.aPos.position, _user.targetPos.position, true);
		base.OnAbilityUseFrame();
	}
	
	/// <summary>
	/// Completes the logic
	/// </summary>
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
					yield return new WaitForSeconds(_ability.castTime * 
						AOC2Math.AttackSpeedMod(_user.GetStat(AOC2Values.UnitStat.ATTACK_SPEED)));
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
