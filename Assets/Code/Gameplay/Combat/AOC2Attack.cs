using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// A set of full attack values, including a base delivery
/// type, damage, cooldown, etc.
/// </summary>
[System.Serializable]
public class AOC2Attack {
	
	/// <summary>
	/// Prefab for the base delivery of this
	/// attack
	/// </summary>
	public AOC2Delivery baseDelivery;
	
	/// <summary>
	/// The damage of the delivery of this attack
	/// </summary>
	public float damage = 1f;
	
	/// <summary>
	/// The lifetime of the delivery after created
	/// </summary>
	public float life = 1f;
	
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
	/// The movement speed of the delivery
	/// </summary>
	public float speed = 5f;
	
	/// <summary>
	/// Gets the range of the spell
	/// </summary>
	public float range{
		get{
			return speed * life;
		}
	}
	
	public AOC2Delivery Use(Vector3 origin, Vector3 dir)
	{
		Debug.Log("Using attack");
		//TODO: USE POOL MANAGER TO CREATE!
		AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get(baseDelivery, origin) as AOC2Delivery;
		
		deliv.Init(damage,speed,life,dir);
		
		return deliv;
	}
}
