using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2ResourceManager : MonoBehaviour {
	
	/// <summary>
	/// The resources.
	/// Indexed using the AOC2Values.Buildings.Resources enum
	/// </summary>
	public int[] resources = {0, 0, 0};
	
	/// <summary>
	/// The capacity of each resource.
	/// Indexed using the AOC2Values.Buildings.Resources enum
	/// </summary>
	public int[] capacity = {1000, 1000, int.MaxValue};
	
	/// <summary>
	/// The list of storage buildings, for GOLD and TONIC
	/// </summary>
	List<AOC2ResourceStorage>[] storages = new List<AOC2ResourceStorage>[2];
	
	void Awake()
	{
		AOC2ManagerReferences.resourceManager = this;
		for (int i = 0; i < storages.Length; i++) 
		{
			storages[i] = new List<AOC2ResourceStorage>();
		}
	}
	
	
	/// <summary>
	/// Adds the resource.
	/// </summary>
	/// <returns>
	/// The overflow.
	/// </returns>
	/// <param name='resource'>
	/// Resource.
	/// </param>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	public int AddResource(AOC2Values.Buildings.ResourceType resource, int amount)
	{
		int overflow = Collect(resource, amount);
		Store(resource, amount - overflow);
		if (AOC2EventManager.UI.OnChangeResource[(int)resource] != null){
			AOC2EventManager.UI.OnChangeResource[(int)resource](resources[(int)resource]);
		}
		return overflow;
	}
	
	/// <summary>
	/// Collect the specified resource and amount.
	/// On success, returns 0
	/// If there is not enough capacity, returns the amount
	/// that was unable to be stored
	/// </summary>
	/// <param name='resource'>
	/// Resource type.
	/// </param>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	int Collect(AOC2Values.Buildings.ResourceType resource, int amount)
	{
		//If we're collecting over the limit, max it out and return the overflow
		if (resources[(int)resource] + amount > capacity[(int)resource])
		{
			int overflow = (resources[(int)resource] + amount) - capacity[(int)resource];
			resources[(int)resource] = capacity[(int)resource];
			return overflow;
		}
		
		resources[(int)resource] += amount;
		return 0;
	}
	
	/// <summary>
	/// Store the specified resource and amount.
	/// </summary>
	/// <param name='resource'>
	/// Resource.
	/// </param>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	void Store(AOC2Values.Buildings.ResourceType resource, int amount)
	{
		foreach (AOC2ResourceStorage stor in storages[(int)resource]) 
		{
			amount = stor.Store(amount);
		}
	}
	
	/// <summary>
	/// Spends a resource.
	/// </summary>
	/// <returns>
	/// True if the specified amount of the resource
	/// was available and removed
	/// </returns>
	/// <param name='resource'>
	/// The resource
	/// </param>
	/// <param name='amount'>
	/// The amount
	/// </param>
	public bool SpendResource(AOC2Values.Buildings.ResourceType resource, int amount)
	{
		if (Spend (resource, amount))
		{
			RemoveFromStorage(resource, amount);
			if (AOC2EventManager.UI.OnChangeResource[(int)resource] != null)
			{
				AOC2EventManager.UI.OnChangeResource[(int)resource](resources[(int)resource]);
			}
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// Spend the specified resource and amount.
	/// Returns true if the amount was spent.
	/// Returns false if there was not enough of the
	/// specified resource to afford payment.
	/// </summary>
	/// <param name='resource'>
	/// If set to <c>true</c> resource.
	/// </param>
	/// <param name='amount'>
	/// If set to <c>true</c> amount.
	/// </param>
	bool Spend(AOC2Values.Buildings.ResourceType resource, int amount)
	{
		if (resources[(int)resource] > amount)
		{
			resources[(int)resource] -= amount;
			return true;
		}
		return false;
	}
	
	public void AddStorage(AOC2ResourceStorage building)
	{
		storages[(int)building.resource].Add(building);
		OnChangeStorageAmount(building.resource);
	}
	
	/// <summary>
	/// Removes from storage.
	/// </summary>
	/// <param name='resource'>
	/// Resource.
	/// </param>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	void RemoveFromStorage(AOC2Values.Buildings.ResourceType resource, int amount)
	{
		foreach (AOC2ResourceStorage stor in storages[(int)resource]) 
		{
			amount = stor.RemoveAmount(amount);
		}
	}
	
	/// <summary>
	/// Raises the change storage amount event.
	/// Called whenever the capacity for a resource changes, either
	/// through upgrading a storage building or destruction of a
	/// storage building
	/// </summary>
	/// <param name='resource'>
	/// Resource.
	/// </param>
	void OnChangeStorageAmount(AOC2Values.Buildings.ResourceType resource)
	{
		int amount = 0;
		foreach (AOC2ResourceStorage stor in storages[(int)resource])
		{
			amount += stor.capacity;
		}
		capacity[(int)resource] = amount;
	}
	
}
