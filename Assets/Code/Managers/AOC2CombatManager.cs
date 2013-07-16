using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AOC2 Combat Manager.
/// Keeps track of all units in combat, and provides
/// utility functions for finding targets
/// </summary>
public class AOC2CombatManager : MonoBehaviour {
	
	/// <summary>
	/// The players.
	/// </summary>
	public List<AOC2Unit> _allies;
	
	/// <summary>
	/// The enemies.
	/// </summary>
	public List<AOC2Unit> _enemies;
	
	public AOC2UnitSpawner[] _spawners;
	
	private int _currWave = 0;
	
	/// <summary>
	/// The number of waves in this combat
	/// </summary>
	private int waves = 0;
	
	/// <summary>
	/// Awake this instance.
	/// Set manager reference and set up lists
	/// </summary>
	void Awake()
	{
		AOC2ManagerReferences.combatManager = this;
		_allies = new List<AOC2Unit>();
		_enemies = new List<AOC2Unit>();
	}
	
	void Start()
	{
		BuildSpawners();
		SpawnNewWave(0);
	}
	
	/// <summary>
	/// Finds all of the spawners in the scene, and sets waves
	/// to the value of the greatest spawner size
	/// </summary>
	void BuildSpawners()
	{
		Object[] objs = FindObjectsOfType(typeof(AOC2UnitSpawner));
		_spawners = new AOC2UnitSpawner[objs.Length];
		for (int i = 0; i < objs.Length; i++) 
		{
			_spawners[i] = objs[i] as AOC2UnitSpawner;
			
			//If this has the most waves so far, set waves to it
			if (_spawners[i].spawns.Count > waves)
			{
				waves = _spawners[i].spawns.Count;
			}
		}
	}
	
	/// <summary>
	/// Set up delegates
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Combat.OnSpawnEnemy += OnSpawnEnemy;
		AOC2EventManager.Combat.OnSpawnPlayer += OnSpawnPlayer;
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
		AOC2EventManager.Combat.OnEnemiesClear += OnEnemiesCleared;
		AOC2EventManager.Combat.OnPlayerVictory += OnPlayerVictory;
		AOC2EventManager.Combat.OnPlayerDeath += OnPlayerDeath;
	}
	
	/// <summary>
	/// Release delegates
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Combat.OnSpawnEnemy -= OnSpawnEnemy;
		AOC2EventManager.Combat.OnSpawnPlayer -= OnSpawnPlayer;
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
		AOC2EventManager.Combat.OnEnemiesClear -= OnEnemiesCleared;
		AOC2EventManager.Combat.OnPlayerVictory -= OnPlayerVictory;
		AOC2EventManager.Combat.OnPlayerDeath -= OnPlayerDeath;
	}
	
	#region Delegates
	
	/// <summary>
	/// Raises the spawn enemy event.
	/// Adds the enemy to the list
	/// </summary>
	/// <param name='unit'>
	/// Unit being added to enemy list
	/// </param>
	void OnSpawnEnemy(AOC2Unit unit)
	{
		_enemies.Add(unit);
	}
	
	/// <summary>
	/// Raises the spawn player event.
	/// Adds the player to the list
	/// </summary>
	/// <param name='unit'>
	/// Unit to be added
	/// </param>
	void OnSpawnPlayer(AOC2Unit unit)
	{
		_allies.Add(unit);
	}
	
	/// <summary>
	/// Raises the enemy death event.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	void OnEnemyDeath(AOC2Unit unit)
	{
		_enemies.Remove(unit);
		if (_enemies.Count == 0)
		{
			AOC2EventManager.Combat.OnEnemiesClear();
		}
	}
	
	/// <summary>
	/// Raises the player death event.
	/// </summary>
	/// <param name='unit'>
	/// Unit.
	/// </param>
	void OnPlayerDeath(AOC2Unit unit)
	{
		
	}
	
	/// <summary>
	/// Raises the enemies cleared event.
	/// Spawns the next wave. If no more waves,
	/// triggers player victory
	/// </summary>
	void OnEnemiesCleared()
	{
			if (++_currWave < waves)
			{
				SpawnNewWave(_currWave);
			}
			else
			{
				if (AOC2EventManager.Combat.OnPlayerVictory != null)
				{
					AOC2EventManager.Combat.OnPlayerVictory();
				}
			}
	
	}
	
	/// <summary>
	/// Raises the player victory event.
	/// </summary>
	void OnPlayerVictory()
	{
		AOC2EventManager.Popup.CreatePopup("Player Victory!");
	}
	
	#endregion
	
	#region Targetting Utilities
	
	public AOC2Unit GetClosestFromList(AOC2Unit toThis, List<AOC2Unit> list)
	{
		AOC2Unit closest = null;
		float dist = float.NaN;
		for (int i = 0; i < list.Count; i++) 
		{
			if (closest == null)
			{
				closest = list[i];
				dist = AOC2Math.GroundDistanceSqr(toThis.aPos.position, closest.aPos.position);
			}
			else
			{
				float checkDist = AOC2Math.GroundDistanceSqr(toThis.aPos.position, list[i].aPos.position);
				if (checkDist < dist)
				{
					closest = list[i];
					dist = checkDist;
				}
			}
		}
		return closest;
	}
	
	/// <summary>
	/// Gets the closest player unit
	/// </summary>
	/// <returns>
	/// The closest player unit
	/// </returns>
	/// <param name='toThis'>
	/// The unit that we're trying to find the closest
	/// player to
	/// </param>
	public AOC2Unit GetClosestPlayer(AOC2Unit toThis)
	{
		return GetClosestFromList(toThis, _allies);
	}
	
	public AOC2Unit GetClosestEnemy(AOC2Unit toThis)
	{
		return GetClosestFromList(toThis, _enemies);
	}
	
	/// <summary>
	/// Gets the closest player unit in range.
	/// </summary>
	/// <returns>
	/// The closest player unit in range.
	/// </returns>
	/// <param name='toThis'>
	/// The unit that we're trying to find the closest
	/// player to
	/// </param
	public AOC2Unit GetClosestPlayerInRange(AOC2Unit toThis, float range)
	{
		//Faster to square the range than root the distance
		float sqrRange = range * range;
		
		AOC2Unit closest = GetClosestPlayer(toThis);
		
		if (closest != null && AOC2Math.GroundDistanceSqr(toThis.aPos.position, closest.aPos.position) < sqrRange)
		{
			return closest;
		}
		return null;
	}
	
	#endregion
	
	/// <summary>
	/// Surrogate to call Cool on an attack.
	/// Because attacks are not MonoBehaviours, they
	/// cannot call coroutines on themselves.
	/// </summary>
	/// <param name='ability'>
	/// Attack to cooldown
	/// </param>
	public void CoolAbility(AOC2Ability ability)
	{
		StartCoroutine(ability.Cool());
	}
	
	void SpawnNewWave(int wave)
	{
		for (int i = 0; i < _spawners.Length; i++) 
		{
			_spawners[i].SpawnWave(wave);
		}
	}
	
}
