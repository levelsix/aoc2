using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto;

/// <summary>
/// @author Rob Giusti
/// Created by an ability, an object in the game world which will create the
/// physical manifestation of that ability and impart its effects to those that
/// it contacts.
/// </summary>
public class AOC2Delivery : AOC2Particle {
	
	/// <summary>
	/// The spell proto that this delivery derives its features from
	/// </summary>
	public SpellProto spellProto;
	
	/// <summary>
	/// The damage.
	/// </summary>
	public int power;
	
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
	
	/// <summary>
	/// The physical force that this attack will impart
	/// </summary>
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
	
	/// <summary>
	/// Raises the enable event.
	/// Register event delegates.
	/// </summary>
	void OnEnable()
	{
		particleGen.OnGenerationComplete += OnLastParticleDissolve;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Releases event delegates.
	/// </summary>
	void OnDisable()
	{
		particleGen.OnGenerationComplete -= OnLastParticleDissolve;
	}
	
	/// <summary>
	/// Initializes this delivery
	/// </summary>
	/// <param name='proto'>
	/// Proto.
	/// </param>
	/// <param name='pow'>
	/// Power
	/// </param>
	/// <param name='dir'>
	/// Direction
	/// </param>
	/// <param name='targ'>
	/// Target
	/// </param>
	public void Init (SpellProto proto, int pow, Vector3 dir, AOC2Unit targ = null) 
	{
		gObj.SetActive(true);
		
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
	
	/// <summary>
	/// When the last particle dissolves, the delivery pools itself for reuse
	/// </summary>
	void OnLastParticleDissolve()
	{
		Pool();
	}
	
	/// <summary>
	/// Determines whether this instance can hit the specified unit, based on the collision
	/// list and whether this delivery is targeted at a specific unit
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance can hit the specified unit; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='unit'>
	/// The unit that this delivery is attempting to hit
	/// </param>
	public bool CanHit(AOC2Unit unit)
	{
		return unit != null && (target == unit || target == null) && !collList.Contains(unit);
	}
	
	/// <summary>
	/// Hit the specified unit, calling the appropriate reaction method for what
	/// function this ability is
	/// </summary>
	/// <param name='unit'>
	/// Unit which this delivery has hit
	/// </param>
	public void Hit(AOC2Unit unit)
	{
		StartCoroutine(AddToCollList(unit));
		switch (spellProto.function) {
			case SpellProto.SpellFunctionType.ATTACK:
				unit.TakeDamage(this);
				break;
			case SpellProto.SpellFunctionType.BUFF:
				StartCoroutine(ApplyBuff(unit));
				break;	
			case SpellProto.SpellFunctionType.HEAL:
				//TODO Heal recipient
				break;
			default:
				break;
		}
	}
	
	/// <summary>
	/// Applies a buff to the recipient, which fades
	/// after the amount of time specified by the spell proto
	/// </summary>
	/// <param name='unit'>
	/// Unit being buffed
	/// </param>
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
	
	/// <summary>
	/// Adds a unit to the collision list, then removes it after the spell's retarget timer
	/// </summary>
	/// <param name='unit'>
	/// Unit to be added to the list
	/// </param>
	IEnumerator AddToCollList(AOC2Unit unit)
	{
		collList.Add(unit);
		yield return new WaitForSeconds(retarget);
		collList.Remove(unit);
	}
}
