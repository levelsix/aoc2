using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// @author Rob Giusti
/// Unit spawner
/// </summary>
public class AOC2UnitSpawner : MonoBehaviour {
	
	public float range = 15;
	
	public List<AOC2Spawnable> spawns;
	
	public List<AOC2Unit> currSpawns = new List<AOC2Unit>();
	
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
	
	public bool defeated = false;
	
	public Action OnDefeat;
	
	void Awake()
	{
		_trans = transform;
	}
	
	void OnEnable()
	{
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
		if (prev != null)
		{
			prev.OnDefeat += Spawn;
		}
	}
	
	void OnDisable()
	{
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
		if (prev != null)
		{
			prev.OnDefeat -= Spawn;
		}
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
		foreach (AOC2Unit item in GetComponentsInChildren<AOC2Unit>()) 
		{
			currSpawns.Add(item);
		}
	}
	
	void Update()
	{
		if (!hasSpawned && (prev == null || prev.defeated) && PlayerInRange())
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
	
	void OnEnemyDeath(AOC2Unit enemy)
	{
		if (hasSpawned)
		{
			currSpawns.Remove(enemy);
			if (currSpawns.Count == 0)
			{
				defeated = true;
				if (OnDefeat != null)
				{
					OnDefeat();
				}
				gameObject.SetActive(false);
			}
		}
	}
	
	public Dictionary<AOC2Spawnable, int> GetSpawnDict()
	{
		Dictionary<AOC2Spawnable, int> dict = new Dictionary<AOC2Spawnable, int>();
		foreach (AOC2Spawnable item in spawns) 
		{
			AOC2Math.MergeDicts<AOC2Spawnable>(dict, item.GetCounts());
		}
		return dict;
	}
	
}
