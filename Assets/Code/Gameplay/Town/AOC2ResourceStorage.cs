using UnityEngine;
using System.Collections;
using com.lvl6.proto;

/// <summary>
/// @author Rob Giusti
/// Building component for resource storage.
/// </summary>
public class AOC2ResourceStorage : MonoBehaviour {
	
	/// <summary>
	/// The resource type
	/// </summary>
	public ResourceType resource;
	
	/// <summary>
	/// The max capacity of this building
	/// </summary>
	public int capacity;
	
	/// <summary>
	/// The current total.
	/// </summary>
	public int total;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		AOC2ManagerReferences.resourceManager.AddStorage(this);
	}
    
    public void Init(FullUserStructProto proto)
    {
        capacity = proto.fullStruct.storage; //TODO: Take level into account!
    }
	
	/// <summary>
	/// Store the specified amount.
	/// </summary>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	public int Store(int amount)
	{
		if (total + amount > capacity)
		{
			int overflow = total + amount - capacity;
			total = capacity;
			return overflow;
		}
		
		total += amount;
		return 0;
	}
	
	/// <summary>
	/// Removes the amount.
	/// </summary>
	/// <returns>
	/// The amount.
	/// </returns>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	public int RemoveAmount(int amount)
	{
		if (amount > total)
		{
			amount -= total;
			total = 0;
			return amount;
		}
		
		total -= amount;
		return 0;
	}
}
