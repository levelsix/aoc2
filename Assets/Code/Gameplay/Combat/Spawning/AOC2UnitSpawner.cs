using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Unit spawner
/// </summary>
public class AOC2UnitSpawner : MonoBehaviour {
	
	public float range = 15;
	
	public List<AOC2Spawnable> spawns;
	
	public Color debugColor = Color.blue;
	
	/// <summary>
	/// The spawner that must spawn before this spawner.
	/// Used to prevent Update from doing distance calculations
	/// for every unspawned spawn point every step.
	/// Set in editor.
	/// </summary>
	public AOC2UnitSpawner prev;
	
	private Transform _trans;
	
	public bool hasSpawned = false;
	
	void Awake()
	{
		_trans = transform;
	}
	
	public void SpawnWave(int wave)
	{
		if (spawns.Count > wave)
		{
			spawns[wave].Spawn(_trans.position);
		}
	}
	
	public void Spawn()
	{
		if (!hasSpawned)
		{
			foreach (AOC2Spawnable item in spawns) 
			{
				item.Spawn(_trans.position, _trans);
			}
			hasSpawned = true;
		}
	}
	
	void Update()
	{
		if (!hasSpawned && (prev == null || prev.hasSpawned) && PlayerInRange())
		{
			Spawn();
		}
	}
	
	bool PlayerInRange()
	{
		return AOC2ManagerReferences.combatManager.GetClosestPlayerInRange(_trans.position, range) != null;
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = debugColor;
		
		Gizmos.DrawWireSphere (transform.position, 1);
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = debugColor;
		
		Gizmos.DrawWireSphere (transform.position, range);
	}
	
}
