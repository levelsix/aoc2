using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Unity Editor menus for quickly adding spawn
/// objects into the game. Accessed from the top
/// menu within the unity editor
/// </summary>
public class AOC2SpawnerMenu : MonoBehaviour {
	
	/// <summary>
	/// Adds an empty spawner into the scene
	/// </summary>
	[MenuItem("Spawners/Add Spawner")]
	static public void AddSpawner()
	{
		GameObject spawner = new GameObject("Spawner");
		
		spawner.AddComponent<AOC2UnitSpawner>();
		
		Selection.activeGameObject = spawner;
	}
	
	/// <summary>
	/// Creates a spawn table and puts it in the scene
	/// </summary>
	[MenuItem("Spawners/Add Spawn Table")]
	static public void AddTable()
	{
		GameObject table = new GameObject("SpawnTable");
		
		table.AddComponent<AOC2SpawnTable>();
		
		Selection.activeGameObject = table;
	}
	
	/// <summary>
	/// Creates a spawn group and puts it in the scene
	/// </summary>
	[MenuItem("Spawners/Add Spawn Group")]
	static public void AddGroup()
	{
		GameObject spGroup = new GameObject("SpawnGroup");
		
		spGroup.AddComponent<AOC2SpawnGroup>();
		
		Selection.activeGameObject = spGroup;
	}
}
