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
	public AOC2Delivery delivery;
	
	public string name = "Basic Attack";
	
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
	/// The offset of the attack from the character creating it
	/// </summary>
	public float offset = .5f;
	
	#endregion
	
	#region Private
	
	/// <summary>
	/// The on cooldown flag
	/// </summary>
	private bool _onCool = false;
	
	#endregion
	
	#region Properties
	
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
	
	/// <summary>
	/// Gets the range of the spell
	/// </summary>
	public float range{
		get{
			return speed * life + offset + delivery.size;
		}
	}
	
	#endregion
	
	#endregion
	
	#region Functions
	
	public IEnumerator Cool ()
	{
		_onCool = true;
		yield return new WaitForSeconds(coolDown);
		_onCool = false;
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
	public AOC2Delivery Use(Vector3 origin, Vector3 dir)
	{
		//DEBUG
		//Slows down game!
		if (dir != dir.normalized)
		{
			Debug.LogError("NORMALIZE DIRECTION VECTORS!");
		}
		
		if (!_onCool)
		{
			AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get(delivery, origin + dir * offset) as AOC2Delivery;
		
			deliv.Init(damage,speed,life,dir);
			
			AOC2ManagerReferences.combatManager.CoolAttack(this);
			return deliv;
		}
		return null;
	}
	
	#endregion
}
