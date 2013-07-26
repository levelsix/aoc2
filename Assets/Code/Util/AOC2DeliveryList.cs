using UnityEngine;
using System.Collections;

public enum AOC2DeliveryType
{
	BOX,
	SPHERE,
	TORNADO,
	DUST_STORM,
	STARFALL,
	LIGHTNING
}

public class AOC2DeliveryList : MonoBehaviour {

	public AOC2Delivery[] Deliveries;
	
	void Awake()
	{
		AOC2ManagerReferences.deliveryList = this;
	}
	
}
