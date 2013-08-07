using UnityEngine;
using System.Collections;

public class AOC2TargettedAttack : AOC2Attack {
	
	/// <summary>
	/// The range.
	/// </summary>
	private float _range = 5;
	
	/// <summary>
	/// Gets the range of the spell 
	/// </summary>
	/// <value>
	/// The range.
	/// </value>
	override public float range{
		get
		{
			return _range;
		}
	}
	
	public AOC2TargettedAttack(float range, float dam, float lifetime,
		float speed, float force, AOC2DeliveryType deliv, bool targetted, float size = 1f,
		bool persist = false, float retarget = 0f) 
		: base(dam, lifetime, 0, force, deliv, targetted, size, persist, retarget)
	{
		_range = range;
	}
	
	public override void Use (AOC2Unit user, Vector3 origin, Vector3 target)
	{
		AOC2Delivery deliv = AOC2ManagerReferences.poolManager.Get(
			AOC2ManagerReferences.deliveryList.Deliveries[(int)deliveryType], target)
			as AOC2Delivery;
		
		InitDelivery(deliv, user, Vector3.zero);
		
		DoUserAnimation(user);
	}
}
