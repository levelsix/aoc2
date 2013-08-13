using UnityEngine;
using System.Collections;

[System.Serializable]
public class AOC2AttackAbility : AOC2Ability {

	protected AOC2Attack _attack;
	
	public override float range {
		get {
			return _attack.range + base.range;
		}
	}
	
	public AOC2AttackAbility(AOC2Attack attack, string abName, float cast, 
		float cool, int mana, AOC2Values.Abilities.TargetType target,
		AOC2Values.Animations.Anim anim = AOC2Values.Animations.Anim.ATTACK)
		: base(abName, cast, cool, mana, target, anim)
	{
		_attack = attack;
	}
    
    public AOC2AttackAbility(AOC2AttackAbility ability)
        :base(ability)
    {
        _attack = ability._attack;
    }
	
	/// <summary>
	/// Use the attack ability. Returns true if the ability was used, false if the ability
	/// is still on cooldown.
	/// </summary>
	/// <param name='user'>
	/// The unit using the ability
	/// </param>
	/// <param name='origin'>
	/// The origin position of the ability
	/// </param>
	/// <param name='target'>
	/// The target of the ability
	/// </param>
	/// <param name='ignoreCooldown'>
	/// Whether cooldown should be ignored. Used if animations are being used for timing instead
	/// of coroutines.
	/// </param>
	public override bool Use (AOC2Unit user, Vector3 origin, Vector3 target, bool ignoreCooldown = false)
	{
		//Debug.Log("Using attack ability " + name);
		if (ignoreCooldown)
		{
			_attack.Use(user, origin, target);
			return true;
		}
		if (!_onCool)
		{
			_attack.Use(user, origin, target);
			AOC2ManagerReferences.combatManager.CoolAbility(this, user);
			return true;
		}
		return false;
	}
}
