using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Pool Manager, which keeps track of all unused objects
/// which we want to recycle instead of delete and reinstantiate
/// </summary>
public class AOC2PoolManager : MonoBehaviour {

	public Dictionary<AOC2Poolable, List<AOC2Poolable>> pools;
	
	void Awake()
	{
		pools = new Dictionary<AOC2Poolable, List<AOC2Poolable>>();
		AOC2ManagerReferences.poolManager = this;
	}
	
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
		
		pooled.gameObject.SetActive(true);
		
		return pooled;
		
	}
	
	
	public void Pool(AOC2Poolable pooled)
	{
		//Disable the poolable
		pooled.gameObject.SetActive(false);
		
		if (!pools.ContainsKey(pooled.prefab))
		{
			//If no pool, make a new pool
			pools[pooled.prefab] = new List<AOC2Poolable>();
		}
		
		//Add it to the pool
		pools[pooled.prefab].Add(pooled);
	}
	
}
