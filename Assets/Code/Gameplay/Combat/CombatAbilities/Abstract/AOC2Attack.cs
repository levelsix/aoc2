using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// A set of full attack values, including a base delivery
/// type, damage, cooldown, etc.
/// </summary>
[System.Serializable]
public class AOC2Attack {
	
	#region Members
	
	#region Public
	
	/// <summary>
	/// Prefab for the base delivery of this
	/// attack
	/// </summary>
	public AOC2DeliveryType deliveryType;
	
	/// <summary>
	/// The damage of the delivery of this attack
	/// </summary>
	private float _damage = 1;
	
	/// <summary>
	/// The lifetime of the delivery after created
	/// </summary>
	private float _life = 1f;
	
	/// <summary>
	/// The movement speed of the delivery
	/// </summary>
	private float _speed = 5f;
	
	/// <summary>
	/// The offset of the attack from the character creating it
	/// </summary>
	private float _offset = .5f;
	
	/// <summary>
	/// Whether the delivery for this attack should persist.
	/// If false, the delivery will pool after the first hit.
	/// </summary>
	private bool _persist = false;
	
	/// <summary>
	/// How often this delivery can hit the same target.
	/// If this shouldn't happen, make sure retarget == life.
	/// Meaningless if !persist
	/// </summary>
	private float _retarget = 1f;
	
	/// <summary>
	/// The size of the delivery
	/// </summary>
	private float _size = 1f;
	
	#endregion
	
	#region Private
	
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Gets the range of the spell
	/// </summary>
	public float range{
		get{
			return _speed * _life + _offset + AOC2ManagerReferences.deliveryList.Deliveries[(int)deliveryType].size;
		}
	}
	
	#endregion
	
	#endregion
	
	#region Functions
	
	public AOC2Attack(float dam, float lifetime, float speed, float offset, AOC2DeliveryType deliv, float size = 1f,
		bool persist = false, float retarget = 0f)
	{
		_damage = dam;
		_life = lifetime;
		_speed = speed;
		_offset = offset;
		deliveryType = deliv;
		_size = size;
		_persist = persist;
		_retarget = retarget;
	}

	
	/// <summary>
	/// Use this attack
	/// </summary>
	/// <param name='origin'>
	/// Origin of the attack
	/// </param>
	/// <param name='dir'>
	/// Direction to send the delivery
	/// </param>
	public void Use(AOC2Unit user, Vector3 origin, Vector3 target)
	{
		Vector3 dir = (target - origin).normalized;
		
		AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get(
			AOC2ManagerReferences.deliveryList.Deliveries[(int)deliveryType], origin + dir * _offset)
			as AOC2Delivery;
		
		if (user.isEnemy)
		{
			deliv.gameObject.layer = AOC2Values.Layers.TARGET_PLAYER;
		}
		else
		{
			deliv.gameObject.layer = AOC2Values.Layers.TARGET_ENEMY;
		}
	
		deliv.Init((int)(_damage * user.power),
			_speed,_life,dir,_persist,_retarget);
		
		Debug.Log("Size: " + _size);
		deliv.transform.localScale = new Vector3(_size, _size, _size);
		
	}
	
	#endregion
}