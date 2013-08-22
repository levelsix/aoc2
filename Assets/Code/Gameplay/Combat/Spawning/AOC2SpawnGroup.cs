using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A group of Spawnables that all get spawned together
/// </summary>
public class AOC2SpawnGroup : AOC2Spawnable {
	
	/// <summary>
	/// The contents of this spawn group
	/// </summary>
	[SerializeField]
	AOC2Spawnable[] contents;
	
	/// <summary>
	/// The area around the spawn point to place the different spawns
	/// </summary>
	[SerializeField]
	float area = 3f;
	
	/// <summary>
	/// Spawn at the specified origin.
	/// </summary>
	/// <param name='origin'>
	/// Origin.
	/// </param>
	public override void Spawn (Vector3 origin, Transform parent = null)
	{
		Vector3 offset;
		for (int i = 0; i < contents.Length; i++) 
		{
			offset = new Vector3(Mathf.Sin(2*Mathf.PI*i/contents.Length), 0, Mathf.Cos(2*Mathf.PI*i/contents.Length)) * area;
			contents[i].Spawn(origin + offset, parent);
		}
	}
	
	public override Dictionary<AOC2Spawnable, int> GetCounts ()
	{
		Dictionary<AOC2Spawnable, int> spawns = new Dictionary<AOC2Spawnable, int>();
		foreach (AOC2Spawnable con in contents) 
		{
			AOC2Math.AddDicts<AOC2Spawnable>(spawns, con.GetCounts());
		}
		return spawns;
	}
}
