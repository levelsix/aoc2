using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// @author Rob Giusti
/// Unit spawner
/// </summary>
[RequireComponent (typeof (AOC2Objective))]
public class AOC2UnitSpawner : MonoBehaviour {
	
	public float range = 15;
	
	public AOC2SpawnGroup spawns = new AOC2SpawnGroup();
	
	private List<AOC2Unit> _currSpawns = new List<AOC2Unit>();
	
	public Color debugColor = Color.blue;
	
	public Transform trans;
	
	public bool hasSpawned = false;
	
	public bool defeated = false;
	
	public Action OnDefeat;
	
	public AOC2Objective objective;
	
	public int index = 0;
	
	void Awake()
	{
		trans = transform;
		objective = GetComponent<AOC2Objective>();
		if (AOC2Whiteboard.dungeonData.spawns.ContainsKey(index))
		{
			foreach (var item in AOC2Whiteboard.dungeonData.spawns[index]) 
			{
				for (int i = 0; i < item.Value; i++) {
					spawns.contents.Add(AOC2Whiteboard.dungeonData.monsterTable[item.Key]);
				}
			}
		}
	}
	
	void OnEnable()
	{
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
	}
	
	public void Spawn()
	{
		if (!hasSpawned)
		{
			spawns.Spawn(trans.position, this);
			hasSpawned = true;
		}
	}
	
	public void AddUnit(AOC2Unit unit)
	{
		_currSpawns.Add(unit);
	}
	
	void Update()
	{
		if (!hasSpawned && PlayerInRange())
		{
			Spawn();
		}
	}
	
	bool PlayerInRange()
	{
		return AOC2ManagerReferences.combatManager.GetClosestPlayerInRange(trans.position, range) != null;
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
			_currSpawns.Remove(enemy);
			if (_currSpawns.Count == 0)
			{
				Defeat();
			}
		}
	}
	
	void Defeat()
	{
		defeated = true;
		
		objective.Complete();
		
		//AOC2EventManager.Combat.OnObjectiveComplete(this);
		
		if (OnDefeat != null)
		{
			OnDefeat();
		}
		gameObject.SetActive(false);
	}
	
	public Dictionary<AOC2Unit, int> GetSpawnDict()
	{
		return spawns.GetCounts();
	}
	
}
