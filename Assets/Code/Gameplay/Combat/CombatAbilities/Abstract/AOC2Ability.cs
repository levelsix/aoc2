using UnityEngine;
using System.Collections;
using proto;

[System.Serializable]
public class AOC2Ability {
	
	public SpellProto spellProto;
	
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
		
	public float range;
	
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
	
	public AOC2Ability(SpellProto proto)
	{
		spellProto = proto;
		
		name = proto.name;
		castTime = proto.castTime;
		coolDown = proto.coolDown;
		manaCost = proto.manaCost;
		animation = AOC2Values.Animations.Anim.ATTACK;
		
		range = CalculateRange(proto);
	}
	
	public float CalculateRange(SpellProto proto)
	{
		switch (proto.targetType) {
		case SpellProto.SpellTargetType.TARGETTED:
			return proto.range;
		case SpellProto.SpellTargetType.SELF:
			return 0.5f; //Just to account for small error in floating point math
		case SpellProto.SpellTargetType.PERSONAL:
			float delivDist = spellProto.size/2 + spellProto.deliverySpeed * spellProto.deliveryDuration;
			if (proto.directionType != SpellProto.SpellDirectionType.SCATTERED)
			{
				delivDist += spellProto.particleDuration * spellProto.particleSpeed;
			}
			return delivDist;
		default:
		break;
		}
		return 0;
	}
    
	public IEnumerator Cool (AOC2Unit user)
	{
		_onCool = true;
		
		float coolTime = coolDown * AOC2Math.AttackSpeedMod(user.GetStat(AOC2Values.UnitStat.ATTACK_SPEED));
		
		if (AOC2EventManager.UI.OnAbilityStartCool != null)
		{
			AOC2EventManager.UI.OnAbilityStartCool(this, coolTime);
		}
		yield return new WaitForSeconds(coolTime);
		_onCool = false;
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
	public virtual bool Use (AOC2Unit user, Vector3 origin, Vector3 target, bool ignoreCooldown = false)
	{
		if ((ignoreCooldown || !_onCool) && user.UseMana(manaCost))
		{
			
			//Future code, for deriving attack data from SpellProto
			if (spellProto != null)
			{
				Vector3 spellOrigin;
				Vector3 dir;
				
				switch (spellProto.targetType) {
					case SpellProto.SpellTargetType.SELF:
						spellOrigin = origin;
						dir = Vector3.zero;
						break;
					case SpellProto.SpellTargetType.TARGETTED:
						spellOrigin = target;
						dir = Vector3.zero;
						break;
					default:
						spellOrigin = origin;
						dir = (target - origin).normalized;
						break;
				}
				
				AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get (AOC2ManagerReferences.deliveryList.baseDelivery,
					spellOrigin + dir * user.trans.localScale.x / 2) as AOC2Delivery;
				
				InitDelivery(deliv, user, dir);
			}
			AOC2ManagerReferences.combatManager.CoolAbility(this, user);
			return true;
		}
		return false;
	}
	
	protected void InitDelivery(AOC2Delivery deliv, AOC2Unit user, Vector3 dir)
	{
		bool oppositeLayer = true;
		if ((spellProto.function == SpellProto.SpellFunctionType.BUFF || spellProto.function == SpellProto.SpellFunctionType.HEAL)
			&& spellProto.strength > 0)
		{
			oppositeLayer = false;
		}
		
		if (user.isEnemy)
		{
			if (oppositeLayer)
			{
				deliv.gameObject.layer = AOC2Values.Layers.TARGET_PLAYER;
			}
			else
			{
				deliv.gameObject.layer = AOC2Values.Layers.TARGET_ENEMY;
			}
		}
		else
		{
			if (oppositeLayer)
			{
				deliv.gameObject.layer = AOC2Values.Layers.TARGET_ENEMY;
			}
			else
			{
				deliv.gameObject.layer = AOC2Values.Layers.TARGET_PLAYER;
			}
		}
		
		deliv.trans.parent = user.trans.parent;
		
		deliv.Init(spellProto, (int)(spellProto.strength * user.GetStat(AOC2Values.UnitStat.STRENGTH)), dir);
        
        if (spellProto.targetted)
        {
            deliv.target = user.targetUnit;  
        }
		
		deliv.trans.localScale = new Vector3(spellProto.size, spellProto.size, spellProto.size);
	}
}
