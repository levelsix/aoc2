using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Pool Manager, which keeps track of all unused objects
/// which we want to recycle instead of delete and reinstantiate
/// </summary>
public class AOC2PoolManager : MonoBehaviour {
	
	/// <summary>
	/// A dictionary that maps prefabs to their respective pools
	/// </summary>
	public Dictionary<AOC2Poolable, List<AOC2Poolable>> pools;
	
	/// <summary>
	/// Awake this instance.
	/// Create the pool dictionary and set the manager reference
	/// </summary>
	void Awake()
	{
		pools = new Dictionary<AOC2Poolable, List<AOC2Poolable>>();
		AOC2ManagerReferences.poolManager = this;
	}
	
	/// <summary>
	/// Get the specified prefab at pos.
	/// If there is a pooled instance, re-enables it and returns it
	/// Otherwise, creates a new instance
	/// </summary>
	/// <param name='prefab'>
	/// Prefab of which to get a newly enabled copy
	/// </param>
	/// <param name='pos'>
	/// Position at which to move the object to
	/// </param>
	public AOC2Poolable Get(AOC2Poolable prefab, Vector3 pos)
	{
		AOC2Poolable pooled;
		if(pools.ContainsKey(prefab) && pools[prefab].Count > 0)
		{
			//Get from existing pool
			pooled = pools[prefab][0];
			pools[prefab].RemoveAt(0);
			
			pooled.transform.position = pos;
		}
		else
		{
			//Create new object
			pooled = prefab.Make(pos);
		}
		
		//Set it back up.
		pooled.transform.parent = null;
		pooled.gameObject.SetActive(true);
		
		return pooled;
		
	}
	
	/// <summary>
	/// Pool the specified poolable object.
	/// </summary>
	/// <param name='pooled'>
	/// Instance of an object to pool
	/// </param>
	public void Pool(AOC2Poolable pooled)
	{
		//Disable the poolable
		pooled.gameObject.SetActive(false);
		
		//If no pool, make a new pool
		if (!pools.ContainsKey(pooled.prefab))
		{
			pools[pooled.prefab] = new List<AOC2Poolable>();
		}
		
		//Add it to the pool
		pools[pooled.prefab].Add(pooled);
		pooled.transform.parent = transform;
	}
	
}
