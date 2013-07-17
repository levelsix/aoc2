using UnityEngine;
using System.Collections;

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
	public override void Spawn (Vector3 origin)
	{
		Debug.Log(name);
		Vector3 offset;
		for (int i = 0; i < contents.Length; i++) 
		{
			offset = new Vector3(Mathf.Sin(2*Mathf.PI*i/contents.Length), 0, Mathf.Cos(2*Mathf.PI*i/contents.Length)) * area;
			contents[i].Spawn(origin + offset);
		}
	}
}
