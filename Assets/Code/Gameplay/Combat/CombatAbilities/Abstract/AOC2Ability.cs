using UnityEngine;
using System.Collections;

[System.Serializable]
public class AOC2Ability {
	
	public string name;
	
	public int manaCost;
	
	public AOC2Values.Abilities.TargetType targetType;
	
	/// <summary>
	/// The cast time.
	/// Time between player using and attack happening.
	/// </summary>
	public float castTime = 0f;
	
	/// <summary>
	/// The cool down.
	/// The time after this attack before it can be
	/// used again.
	/// </summary>.
	public float coolDown = 1f;

	/// <summary>
	/// The on cooldown flag
	/// </summary>
	protected bool _onCool = false;

	/// <summary>
	/// Gets a value indicating whether this <see cref="AOC2Attack"/> is on cooldown.
	/// </summary>
	/// <value>
	/// <c>true</c> if on cooldown; otherwise, <c>false</c>.
	/// </value>
	public bool onCool{
		get
		{
			return _onCool;
		}
	}
	
	public virtual float range{
		get
		{
			return .5f;
		}
	}
	
	public AOC2Ability(string abName, float cast, float cool, int mana, AOC2Values.Abilities.TargetType target)
	{
		name = abName;
		castTime = cast;
		coolDown = cool;
		manaCost = mana;
		targetType = target;
	}

	public IEnumerator Cool ()
	{
		_onCool = true;
		yield return new WaitForSeconds(coolDown);
		_onCool = false;
	}

	public virtual bool Use(AOC2Unit user, Vector3 origin, Vector3 target)
	{
		if (!_onCool)
		{
			AOC2ManagerReferences.combatManager.CoolAbility(this);
			return true;
		}
		return false;
	}
}
