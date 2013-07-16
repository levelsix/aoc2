using UnityEngine;
using UnityEditor;
using System.Collections;

public class AOC2SpawnerMenu : MonoBehaviour {

	[MenuItem("Spawners/Add Spawner")]
	static public void AddSpawner()
	{
		GameObject spawner = new GameObject("Spawner");
		
		spawner.AddComponent<AOC2UnitSpawner>();
		
		Selection.activeGameObject = spawner;
	}
	
	[MenuItem("Spawners/Add Spawn Table")]
	static public void AddTable()
	{
		GameObject table = new GameObject("SpawnTable");
		
		table.AddComponent<AOC2SpawnTable>();
		
		Selection.activeGameObject = table;
	}
	
	[MenuItem("Spawners/Add Spawn Group")]
	static public void AddGroup()
	{
		GameObject spGroup = new GameObject("SpawnGroup");
		
		spGroup.AddComponent<AOC2SpawnGroup>();
		
		Selection.activeGameObject = spGroup;
	}
}
