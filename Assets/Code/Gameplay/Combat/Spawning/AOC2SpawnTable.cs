using UnityEngine;
using System.Collections;
using System;

public class AOC2SpawnTable : AOC2Spawnable {
	
	/// <summary>
	/// The contents that this table can spawn from
	/// </summary>
	[SerializeField]
	AOC2Spawnable[] contents;
	
	/// <summary>
	/// The chances of each corrseponding member of contents
	/// </summary>
	[SerializeField]
	float[] chances;
	
	/// <summary>
	/// Spawn the specified origin.
	/// Does a weighted randomization using the weights in chances
	/// to pick which object from contents to spawn.
	/// </summary>
	/// <param name='origin'>
	/// Origin.
	/// </param>
	/// <exception cref='Exception'>
	/// Is thrown when the lengths of contents and chances don't match
	/// </exception>
	public override void Spawn (Vector3 origin)
	{
		if (chances.Length != contents.Length)
		{
			throw new Exception("Mismatched spawn table!");
		}
		
		float total = 0;
		for (int i = 0; i < chances.Length; i++) 
		{
			total += chances[i];
		}
		float num = UnityEngine.Random.value * total;
		for (int i = 0; i < contents.Length; i++) 
		{
			if (num < chances[i])
			{
				contents[i].Spawn(origin);
				return;
			}
			num -= chances[i];
		}
	}
}
