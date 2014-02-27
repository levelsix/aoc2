using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// A group of Spawnable Units that all get spawned together
/// </summary>
[System.Serializable]
public class AOC2SpawnGroup : AOC2Spawnable {
	
	/// <summary>
	/// The contents of this spawn group
	/// </summary>
	public List<AOC2SpawnMonster> contents;
	
	/// <summary>
	/// The area around the spawn point to place the different spawns
	/// </summary>
	public float area = .5f;
	
	public AOC2SpawnGroup()
	{
		contents = new List<AOC2SpawnMonster>();
	}
	
	public AOC2SpawnGroup(List<AOC2SpawnMonster> spawns)
	{
		contents = spawns;
	}
	
	/// <summary>
	/// Spawn at the specified origin.
	/// </summary>
	/// <param name='origin'>
	/// Origin.
	/// </param>
	public void Spawn (Vector3 origin, AOC2UnitSpawner parent)
	{
		Vector3 offset;
		for (int i = 0; i < contents.Count; i++) 
		{
			offset = new Vector3(Mathf.Sin(2*Mathf.PI*i/contents.Count), 0, Mathf.Cos(2*Mathf.PI*i/contents.Count)) * area;
			contents[i].Spawn(origin + offset, parent);
		}
	}
	
	public Dictionary<AOC2Unit, int> GetCounts ()
	{
		Dictionary<AOC2Unit, int> spawns = new Dictionary<AOC2Unit, int>();
		foreach (AOC2SpawnMonster con in contents) 
		{
			AOC2Math.AddDicts<AOC2Unit>(spawns, con.GetCounts());
		}
		return spawns;
	}
}
