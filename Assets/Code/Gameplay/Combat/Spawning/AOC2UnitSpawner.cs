using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Unit spawner
/// </summary>
public class AOC2UnitSpawner : MonoBehaviour {
	
	public List<AOC2Spawnable> spawns;
	
	public Color debugColor = Color.blue;
	
	private Transform _trans;
	
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
	
	void OnDrawGizmos()
	{
		Gizmos.color = debugColor;
		Gizmos.DrawWireSphere (transform.position, 1);
	}
	
}
