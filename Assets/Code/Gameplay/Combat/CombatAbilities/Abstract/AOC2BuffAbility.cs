using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Base structure for an ability that buffs a character's
/// stats.
/// </summary>
public class AOC2BuffAbility : AOC2Ability {

	float _duration;
	
	float _scale;
	
	AOC2Values.UnitStat _stat;
	
	public AOC2BuffAbility(string name, float duration, float scale, AOC2Values.UnitStat stat, float cast, float cool,
		int mana, AOC2Values.Abilities.TargetType target)
		: base(name, cast, cool, mana, target)
	{
		_duration = duration;
		_scale = scale;
		_stat = stat;
	}
	
	public override bool Use (AOC2Unit user, Vector3 origin, Vector3 target)
	{
		if (!_onCool){
			AOC2ManagerReferences.combatManager.RunBuff(user, this);
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
		float amount = target.stats[(int)_stat] * _scale;
		target.stats[(int)_stat] += (int)amount;
		Debug.Log("Buff: " + target.stats[(int)_stat]);
		yield return new WaitForSeconds(_duration);
		target.stats[(int)_stat] -= (int)amount;
	}
}
