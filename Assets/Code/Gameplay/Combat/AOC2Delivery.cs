using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2Delivery : MonoBehaviour, AOC2Poolable {
	
	/// <summary>
	/// The damage.
	/// </summary>
	[HideInInspector]
	public int damage;
	
	/// <summary>
	/// The speed.
	/// </summary>
	[HideInInspector]
	public float speed;
	
	/// <summary>
	/// The direction.
	/// </summary>
	[HideInInspector]
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
	
	/// <summary>
	/// The pointer to this delivery's own prefab.
	/// </summary>
	private AOC2Delivery _prefab;
	
	/// <summary>
	/// The collision list.
	/// Holds recently hit units. Don't hit them again
	/// until outside of the collList.
	/// </summary>
	private List<AOC2Unit> collList;
	
	/// <summary>
	/// Awake this instance.
	/// Instantiate the collision list
	/// </summary>
	void Awake()
	{
		collList = new List<AOC2Unit>();
	}
	
	/// <summary>
	/// A public getter and setter, so that we can
	/// make this part of the Poolable interface
	/// </summary>
	public AOC2Poolable prefab{
		get
		{
			return _prefab;
		}
		set
		{
			_prefab = value as AOC2Delivery;
		}
	}
	
	/// <summary>
	/// Make the poolable object, setting its prefab
	/// </summary>
	/// <param name='origin'>
	/// The point at which to make the object
	/// </param>
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2Delivery deliv = Instantiate(this, origin, Quaternion.identity) as AOC2Delivery;
		deliv.prefab = this;
		return deliv;
	}
	
	// Use this for initialization
	public void Init (int dam, float spd, float life, Vector3 dir, bool pers, float ret, AOC2Unit targ = null) 
	{
		gameObject.SetActive(true);
		
		damage = dam;
		speed = spd;
		direction = dir;
		retarget = ret;
		persist = pers;
		
		target = targ;
		
		//Clear the collision list
		collList.RemoveRange(0, collList.Count);
		
		StartCoroutine(DieAfterLifetime(life));
	}
	
	/// <summary>
	/// Dies the after lifetime.
	/// Uses instances to make sure that we don't
	/// accidentally kill a delivery because a previous cycle
	/// of the gameobject died
	/// </summary>
	IEnumerator DieAfterLifetime(float life)
	{
		yield return new WaitForSeconds(life);
		Pool();
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
	
	public void Pool()
	{
		AOC2ManagerReferences.poolManager.Pool(this);
	}
}
