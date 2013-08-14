using UnityEngine;
using System.Collections;

[System.Serializable]
public class AOC2Ability {
	
	public string name;
	
	public int manaCost;
	
	public AOC2Values.Abilities.TargetType targetType;
	
	public AOC2Values.Animations.Anim animation;
	
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
	
	public AOC2Ability(string abName, float cast, float cool, int mana, AOC2Values.Abilities.TargetType target,
		AOC2Values.Animations.Anim anim = AOC2Values.Animations.Anim.ATTACK)
	{
		name = abName;
		castTime = cast;
		coolDown = cool;
		manaCost = mana;
		targetType = target;
		animation = anim;
	}
 
    public AOC2Ability(AOC2Ability other)
    {
        name = other.name;
        castTime = other.castTime;
        coolDown = other.coolDown;
        manaCost = other.manaCost;
        targetType = other.targetType;
		animation = other.animation;
    }
    
	public IEnumerator Cool (AOC2Unit user)
	{
		_onCool = true;
		if (AOC2EventManager.UI.OnAbilityStartCool != null)
		{
			AOC2EventManager.UI.OnAbilityStartCool(this, coolDown);
		}
		yield return new WaitForSeconds(coolDown);
		_onCool = false;
	}

	public virtual bool Use(AOC2Unit user, Vector3 origin, Vector3 target, bool ignoreCooldown = false)
	{
		if (ignoreCooldown && user.UseMana(manaCost))
		{
			return true;
		}
		if (!_onCool && user.UseMana(manaCost))
		{
			AOC2ManagerReferences.combatManager.CoolAbility(this, user);
			return true;
		}
		return false;
	}
}
