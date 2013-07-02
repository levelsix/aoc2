using UnityEngine;
using System.Collections;

/// <summary>
/// Static class that maintains references to all
/// manager classes in the scene. All managers
/// need to add their references on Awake(), and
/// things can reference this class at Start() at
/// the earliest
/// </summary>
public static class AOC2ManagerReferences {

	public static AOC2ControlManager controlManager;
	public static AOC2GridManager gridManager;
	public static AOC2BuildingManager buildingManager;
	public static AOC2PoolManager poolManager;
	
}
