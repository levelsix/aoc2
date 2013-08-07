using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2Delivery : AOC2Particle {
	
	/// <summary>
	/// The damage.
	/// </summary>
	public int damage;
	
	/// <summary>
	/// The speed.
	/// </summary>
	public float speed;
	
	/// <summary>
	/// The direction.
	/// </summary>
	public Vector3 direction;
	
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
	private AOC2ParticleGenerator particleGen;
	
	/// <summary>
	/// Awake this instance.
	/// Instantiate the collision list
	/// </summary>
	void Awake()
	{
		collList = new List<AOC2Unit>();
		particleGen = GetComponent<AOC2ParticleGenerator>();
	}
	
	// Use this for initialization
	public void Init (int dam, float spd, float frce, float life, Vector3 dir, bool pers, float ret, AOC2Unit targ = null) 
	{
		gameObject.SetActive(true);
		
		damage = dam;
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
			particleGen.overThisTime = life;
			particleGen.Init();
		}
		
		base.Init(life);
	}
	
	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// Passes on to TriggerStay, to keep consistent log
	/// </summary>
	/// <param name='other'>
	/// Other collider
	/// </param>
	void OnTriggerEnter (Collider other)
	{
		OnTriggerStay(other);
	}
	
	IEnumerator AddToCollList(AOC2Unit unit)
	{
		collList.Add(unit);
		yield return new WaitForSeconds(retarget);
		collList.Remove(unit);
	}
	
	void OnTriggerStay (Collider other)
	{
		AOC2Unit unit = other.GetComponent<AOC2Unit>();
		if (unit != null && (target == unit || target == null) && !collList.Contains(unit))
		{
			unit.TakeDamage(this);
			if (!persist)
			{
				Pool();
			}
			else
			{
				StartCoroutine(AddToCollList(unit));
			}
		}
	}
}
