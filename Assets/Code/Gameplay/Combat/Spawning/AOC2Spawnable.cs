using UnityEngine;
using System.Collections;

/// <summary>
/// A spawnable object
/// Can also be a group or table that propigate
/// the spawn call
/// </summary>
public abstract class AOC2Spawnable : MonoBehaviour {

	/// <summary>
	/// Return a spawnable instance
	/// </summary>
	abstract public AOC2Spawnable Spawn(Vector3 origin);
	
}
