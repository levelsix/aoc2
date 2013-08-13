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
	public readonly float _damage = 1;
	
	/// <summary>
	/// The lifetime of the delivery after created
	/// </summary>
	public readonly float _life = 1f;
	
	/// <summary>
	/// The movement speed of the delivery
	/// </summary>
	public readonly float _speed = 5f;
	
	/// <summary>
	/// Whether the delivery for this attack should persist.
	/// If false, the delivery will pool after the first hit.
	/// </summary>
	public readonly bool _persist = false;
	
	/// <summary>
	/// How often this delivery can hit the same target.
	/// If this shouldn't happen, make sure retarget == life.
	/// Meaningless if !persist
	/// </summary>
	public readonly float _retarget = 1f;
	
	/// <summary>
	/// The size of the delivery
	/// </summary>
	public readonly float _size = 1f;
	
	/// <summary>
	/// The force of the attack; how much knockback it causes
	/// </summary>
	public readonly float _force = 2f;
	
    /// <summary>
    /// The targetted.
    /// </summary>
    public readonly bool _targetted = true;
    
	#endregion
	
	#region Private
	
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Gets the range of the spell
	/// </summary>
	virtual public float range{
		get{
			return _speed * _life + _size;
		}
	}
	
	#endregion
	
	#endregion
	
	#region Functions
	
	public AOC2Attack(float dam, float lifetime, float speed, float force,
		AOC2DeliveryType deliv, bool targetted, float size = 1f,
		bool persist = false, float retarget = 0f)
	{
		_damage = dam;
		_life = lifetime;
		_speed = speed;
		_force = force;
		deliveryType = deliv;
		_size = size;
		_persist = persist;
		_retarget = retarget;
        _targetted = targetted;
        
	}
	
	protected void DoUserAnimation(AOC2Unit user)
	{
		if (!user.model.usesTriggers)
		{
			user.model.SetAnimationFlag(AOC2Values.Animations.Anim.ATTACK, true);
		}
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
	public virtual void Use(AOC2Unit user, Vector3 origin, Vector3 target)
	{
		Vector3 dir = (target - origin).normalized;
		
		AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get(
			AOC2ManagerReferences.deliveryList.Deliveries[(int)deliveryType], origin + dir * user.transform.localScale.x / 2)
			as AOC2Delivery;
		
		InitDelivery(deliv, user, dir);
		
		DoUserAnimation(user);
		
		//If there is a direction (i.e. not a self-targetting ability), turn towards it
		if (dir != Vector3.zero)
		{
			user.transform.forward = dir;
		}
	}
	
	protected void InitDelivery(AOC2Delivery deliv, AOC2Unit user, Vector3 dir)
	{
		if (user.isEnemy)
		{
			deliv.gameObject.layer = AOC2Values.Layers.TARGET_PLAYER;
		}
		else
		{
			deliv.gameObject.layer = AOC2Values.Layers.TARGET_ENEMY;
		}
	
		deliv.Init((int)(_damage * user.power),
			_speed, _force, _life, dir, _persist, _retarget);
        
        if (_targetted)
        {
            deliv.target = user.targetUnit;  
        }
		
		deliv.transform.localScale = new Vector3(_size, _size, _size);
	}
	
	#endregion
}
