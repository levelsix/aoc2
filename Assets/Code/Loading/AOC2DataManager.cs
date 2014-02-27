﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using proto;

/// <summary>
/// @author Rob Giusti
/// AOC2DataManager
/// Manager which keeps a static dictionary of all data loaded into the game
/// </summary>
public class AOC2DataManager : MonoBehaviour {
	
	/// <summary>
	/// A dictionary that maps types to lists of their 
	/// </summary>
	static Dictionary<Type, IDictionary> dataDict = new Dictionary<Type, IDictionary>();
	
	bool enemiesDone = false;
	
	bool loadComplete{
		get
		{
			return enemiesDone;
		}
		set
		{
			enemiesDone = value;
		}
	}
	
	void Awake()
	{
		AOC2ManagerReferences.dataManager = this;
	}
	
	IEnumerator Start()
	{
		while(true)
		{
			yield return new WaitForSeconds(5);
			GC.Collect();
		}
	}
	
	/// <summary>
	/// Adds the specified type to the data dictionary and creates a list
	/// for it.
	/// </summary>
	/// <param name='typ'>
	/// Type.
	/// </param>
	void AddType(Type typ)
	{
		dataDict.Add(typ, new Dictionary<int, object>());
	}
	
	/// <summary>
	/// Checks whether there already exists a dictionary entry for this type.
	/// If there isn't, adds it.
	/// </summary>
	/// <param name='typ'>
	/// Type.
	/// </param>
	void CheckType(Type typ)
	{
		if (!dataDict.ContainsKey(typ))
		{
			AddType(typ);
		}
	}
	
	/// <summary>
	/// Gets the data of the specified type and id.
	/// </summary>
	/// <param name='type'>
	/// Type.
	/// </param>
	/// <param name='id'>
	/// Identifier.
	/// </param>
	public object Get(Type type, int id)
	{
		CheckType(type);
		return dataDict[type][id];
	}
	
	/// <summary>
	/// Load the specified obj and id.
	/// </summary>
	/// <param name='obj'>
	/// Object.
	/// </param>
	/// <param name='id'>
	/// Identifier.
	/// </param>
	public void Load(object obj, int id)
	{
		Type typ = obj.GetType();
		CheckType(typ);
		dataDict[typ][id] = obj;
	}
	
	public void LoadLevel(CombatRoomProto proto)
	{
		loadComplete = false;
		StartCoroutine(LoadLevelWhenComplete(proto));
		BakeLevel(proto);
	}
	
	/// <summary>
	/// Bakes the level.
	/// Queues the level to load asychronously while
	/// enemies and deliveries are also loaded
	/// </summary>
	/// <param name='proto'>
	/// Combat room to start baking
	/// </param>
	public void BakeLevel(CombatRoomProto proto)
	{	
		Debug.Log("Baking the level");
		AOC2Whiteboard.dungeonData = new AOC2DungeonData();
		
		Dictionary<int, Dictionary<int, int>> spawnPointDict = GetSpawnGroups(proto);
		
		AOC2Whiteboard.dungeonData.spawns = spawnPointDict;
		
		Dictionary<int, int> bakeSpawnDict = new Dictionary<int, int>();
		foreach (Dictionary<int,int> item in spawnPointDict.Values) {
			AOC2Math.MergeDicts<int>(bakeSpawnDict, item);
		}
		StartCoroutine(BakeEnemies(bakeSpawnDict));
		
		//TODO: Bake player?
		
		//TODO: Bake deliveries based on player and enemy abilities?
	}
	
	/// <summary>
	/// Starts the asynchronous loading of the level while other scene objects are being baked
	/// </summary>
	/// <param name='proto'>
	/// Proto for the combat room to be loaded
	/// </param>
	IEnumerator LoadLevelWhenComplete(CombatRoomProto proto)
	{
		//TODO: Determine scene from proto
		AsyncOperation asy = AOC2Values.Scene.ChangeScene(AOC2Values.Scene.Scenes.DUNGEON_TEST_SCENE);
		asy.allowSceneActivation = false;
		while(!loadComplete)
		{
			yield return null;
		}
		Debug.Log("Loading Level");
		asy.allowSceneActivation = true;
		yield return asy;
	}
	
	/// <summary>
	/// Bakes the enemies, over frames so that we can show a loading
	/// bar to show that the game isn't just frozen
	/// </summary>
	/// <param name='spawnDict'>
	/// Dictionary of how many of each enemy to bake
	/// </param>
	IEnumerator BakeEnemies(Dictionary<int, int> spawnDict)
	{
		AOC2SpawnMonster unit;
		AOC2Whiteboard.dungeonData.monsterTable = new Dictionary<int, AOC2SpawnMonster>();
		foreach (KeyValuePair<int, int> item in spawnDict) 
		{
			unit = new AOC2SpawnMonster(AOC2ManagerReferences.dataManager.Get(typeof(MonsterProto), item.Key) as MonsterProto);
			AOC2Whiteboard.dungeonData.monsterTable[item.Key] = unit;
			for (int i = 0; i < item.Value; i++) 
			{
				Debug.Log("Baking a " + item.Key);
				AOC2ManagerReferences.poolManager.Warm(unit);
#if UNITY_EDITOR
				do
				{
					yield return null;
				}while(!Input.GetKeyDown(KeyCode.Space));
#else
				yield return null;
#endif
			}
		}
		Debug.Log("Enemies Baked");
		enemiesDone = true;
	}
	
	Dictionary<int, Dictionary<int, int>> GetSpawnGroups(CombatRoomProto proto)
	{
		Dictionary<int, Dictionary<int,int>> spawnDictDict = new Dictionary<int, Dictionary<int, int>>();
		
		for (int i = 0; i < proto.monsters.Count; i++) 
		{
			if (!spawnDictDict.ContainsKey(proto.spawnPoint[i]))
			{
				spawnDictDict[proto.spawnPoint[i]] = new Dictionary<int, int>();
			}
			if (!spawnDictDict[proto.spawnPoint[i]].ContainsKey(proto.monsters[i]))
			{
				spawnDictDict[proto.spawnPoint[i]][proto.monsters[i]] = 0;
			}
			spawnDictDict[proto.spawnPoint[i]][proto.monsters[i]]++;
		}
		
		return spawnDictDict;
	}
	
	/// <summary>
	/// Determines the spawn groups.
	/// </summary>
	/// <returns>
	/// The spawn groups.
	/// </returns>
	/// <param name='proto'>
	/// Proto.
	/// </param>
	AOC2SpawnGroup[] DetermineSpawnGroups(CombatRoomProto proto)
	{
		Dictionary<int, AOC2SpawnGroup> spawnPoints = new Dictionary<int, AOC2SpawnGroup>();
		int highestIndex = 0;
		
		//Iterate through each monster listed in the CombatRoomProto
		AOC2SpawnMonster unit;
		for (int i = 0; i < proto.monsters.Count; i++) {
			unit = new AOC2SpawnMonster(Get(typeof(MonsterProto), proto.monsters[i]) as MonsterProto);
			if (!spawnPoints.ContainsKey(proto.spawnPoint[i]))
			{
				if (proto.spawnPoint[i] > highestIndex)
				{
					highestIndex = proto.spawnPoint[i];
				}
				spawnPoints[proto.spawnPoint[i]] = new AOC2SpawnGroup();
			}
			spawnPoints[proto.spawnPoint[i]].contents.Add(unit);
		}
		
		AOC2SpawnGroup[] spawns = new AOC2SpawnGroup[highestIndex + 1];
		foreach (KeyValuePair<int, AOC2SpawnGroup> entry in spawnPoints)
		{
			spawns[entry.Key] = entry.Value;
		}
		
		//Make sure that we don't have any null entries in the list, which would designate
		//empty spawn points
		for (int i = 0; i < spawns.Length; i++) 
		{
			if (spawns[i] == null)
			{
				spawns[i] = new AOC2SpawnGroup();
			}
		}
		
		
		return spawns;
	}
		
}