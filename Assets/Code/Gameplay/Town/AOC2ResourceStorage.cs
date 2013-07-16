using UnityEngine;
using System.Collections;

public class AOC2ResourceStorage : AOC2BuildingUpgrade {
	
	/// <summary>
	/// The resource type
	/// </summary>
	public AOC2Values.Buildings.ResourceType resource;
	
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
		//TODO: Init from a Protocol
		AOC2ManagerReferences.resourceManager.AddStorage(this);
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
