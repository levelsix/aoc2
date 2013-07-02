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
	
	#endregion
	
	#region Private
	
	private bool _onCool = false;
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Gets the range of the spell
	/// </summary>
	public float range{
		get{
			return speed * life;
		}
	}
	
	#endregion
	
	#endregion
	
	#region Functions
	
	public IEnumerator Cool()
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
		if (!_onCool)
		{
			AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get(baseDelivery, origin) as AOC2Delivery;
		
			deliv.Init(damage,speed,life,dir);
			
			return deliv;
		}
		return null;
	}
	
	#endregion
}
