using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Base structure for an ability that buffs a character's
/// stats.
/// </summary>
public class AOC2BuffAbility : AOC2Ability {

	float _duration;
	
	float _amount;
    
    bool _flat;
	
	AOC2Values.UnitStat _stat;
	
	public AOC2BuffAbility(string name, float duration, float amount, AOC2Values.UnitStat stat, bool flat, float cast, float cool,
		int mana, AOC2Values.Abilities.TargetType target, AOC2Values.Animations.Anim anim = AOC2Values.Animations.Anim.ATTACK)
		: base(name, cast, cool, mana, target, anim)
	{
		_duration = duration;
		_amount = amount;
		_stat = stat;
        _flat = flat;
	}
    
    public AOC2BuffAbility(AOC2BuffAbility ability)
        :base(ability)
    {
        _duration = ability._duration;
        _amount = ability._amount;
        _stat = ability._stat;
        _flat = ability._flat;
    }
	
	public override bool Use (AOC2Unit user, Vector3 origin, Vector3 target, bool ignoreCooldown = false)
	{
		if (!_onCool || ignoreCooldown)
		{
			AOC2ManagerReferences.combatManager.RunBuff(user, this);
			if (!_onCool)
			{
				AOC2ManagerReferences.combatManager.CoolAbility(this, user);
			}
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// Coroutine that buffs the stat, then after the duration
	/// removes the buff
	/// </summary>
	public IEnumerator Buff(AOC2Unit target)
	{
		float amount;
        if (_flat)
        {
            amount = _amount;
        }
        else
        {
            amount = target.stats[(int)_stat] * _amount;
        }
		target.stats[(int)_stat] += (int)amount;;
		Debug.Log("Buff: " + target.stats[(int)_stat]);
		yield return new WaitForSeconds(_duration);
		target.stats[(int)_stat] -= (int)amount;;
	}
}
