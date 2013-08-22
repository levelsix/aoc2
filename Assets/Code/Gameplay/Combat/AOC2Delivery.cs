using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto;

public class AOC2Delivery : AOC2Particle {
	
	public SpellProto spellProto;
	
	/// <summary>
	/// The damage.
	/// </summary>
	public int power;
	
	/// <summary>
	/// The speed.
	/// </summary>
	//public float speed;
	
	/// <summary>
	/// The target.
	/// If this attack does not have a target, it will hit the
	/// first enemy that it comes into contact with.
	/// Otherwise, it will target this enemy specifically
	/// </summary>
	public AOC2Unit target;
	
	/// <summary>
	/// Whether this delivery persists past the first hit
	/// </summary>
	public bool persist;
	
	/// <summary>
	/// The size of the delivery
	/// Must be hand-set.
	/// </summary>
	public float size;
	
	/// <summary>
	/// The retarget timer. Same as Lifetime if this isn't
	/// a DoT attack.
	/// </summary>
	public float retarget;
	
	public float force;
	
	/// <summary>
	/// The collision list.
	/// Holds recently hit units. Don't hit them again
	/// until outside of the collList.
	/// </summary>
	private List<AOC2Unit> collList;
	
	/// <summary>
	/// The particle generator component, if it exists.
	/// </summary>
	private AOC2DeliveryParticleGenerator particleGen;
	
	/// <summary>
	/// Awake this instance.
	/// Instantiate the collision list
	/// </summary>
	public override void Awake()
	{
		collList = new List<AOC2Unit>();
		particleGen = GetComponent<AOC2DeliveryParticleGenerator>();
		base.Awake();
	}
	
	/*
	public void Init (AOC2Attack att, int dam, float spd, float frce, float life, Vector3 dir, bool pers, float ret, AOC2Unit targ = null) 
	{
		attack = att;
		
		gameObject.SetActive(true);

		power = dam;
		speed = spd;
		direction = dir;
		retarget = ret;
		persist = pers;
		force = frce;

		target = targ;

		//Clear the collision list
		collList.RemoveRange(0, collList.Count);

		if (particleGen != null)
		{
			particleGen.prefab = AOC2ManagerReferences.deliveryList.deliveries[(int)att.deliveryType];
			particleGen.overThisTime = life;
			particleGen.Init();
		}

		base.Init(life);
	}
	*/
	
	
	// Use this for initialization
	public void Init (SpellProto proto, int pow, Vector3 dir, AOC2Unit targ = null) 
	{
		gameObject.SetActive(true);
		
		spellProto = proto;
		
		power = pow;
		
		direction = dir;
		
		retarget = proto.retargetTime;
		
		speed = proto.deliverySpeed;
		
		target = targ;
		
		//Clear the collision list
		collList.RemoveRange(0, collList.Count);
		
		if (particleGen != null)
		{
			particleGen.prefab = AOC2ManagerReferences.deliveryList.deliveries[(int)proto.particleType];
			//particleGen.overThisTime = life;
			particleGen.Init(spellProto);
			
		}
	}
	
	void OnLastParticleDissolve()
	{
		Pool();
	}
	
	public bool CanHit(AOC2Unit unit)
	{
		return unit != null && (target == unit || target == null) && !collList.Contains(unit);
	}
	
	public void Hit(AOC2Unit unit)
	{
		StartCoroutine(AddToCollList(unit));
		switch (spellProto.function) {
			case SpellProto.SpellFunctionType.ATTACK:
				unit.TakeDamage(this);
				break;
			case SpellProto.SpellFunctionType.BUFF:
				StartCoroutine(ApplyBuff(unit));
				//TODO Buff/debuff recipient
				break;	
			case SpellProto.SpellFunctionType.HEAL:
				//TODO Heal recipient
				break;
			default:
				break;
		}
	}
	
	IEnumerator ApplyBuff(AOC2Unit unit)
	{
		float amount;
        if (spellProto.strength > 3)
        {
            amount = spellProto.strength;
        }
        else
        {
            amount = unit.GetStat(spellProto.stat) * spellProto.strength;
        }
		unit.stats[spellProto.stat] += (int)amount;
		
		yield return new WaitForSeconds(spellProto.deliveryDuration);
		unit.stats[spellProto.stat] -= (int)amount;
	}
	
	IEnumerator AddToCollList(AOC2Unit unit)
	{
		collList.Add(unit);
		Debug.Log("Adding " + unit.name + " #" + unit.id + " to coll list for " + spellProto.name);
		yield return new WaitForSeconds(retarget);
		collList.Remove(unit);
		Debug.Log("Removing " + unit.name + " #" + unit.id + " from coll list for " + spellProto.name);
	}
}
