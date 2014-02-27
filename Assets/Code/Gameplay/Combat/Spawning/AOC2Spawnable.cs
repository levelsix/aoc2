using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A spawnable object
/// Can also be a group or table that propigate
/// the spawn call
/// </summary>
public interface AOC2Spawnable {

	/// <summary>
	/// Return a spawnable instance
	/// </summary>
	void Spawn(Vector3 origin, AOC2UnitSpawner parent);
	
	Dictionary<AOC2Unit, int> GetCounts();
}
