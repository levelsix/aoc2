using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A spawnable object
/// Can also be a group or table that propigate
/// the spawn call
/// </summary>
public abstract class AOC2Spawnable : MonoBehaviour {

	/// <summary>
	/// Return a spawnable instance
	/// </summary>
	abstract public void Spawn(Vector3 origin, Transform parent = null);
	
	abstract public Dictionary<AOC2Spawnable, int> GetCounts();
}
