using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// AOC2 Combat Manager.
/// Keeps track of all units in combat, and provides
/// utility functions for finding targets
/// </summary>
public class AOC2CombatManager : MonoBehaviour {
	
	/// <summary>
	/// The players and all helper units.
	/// Serialized to view for debug purposes in editor.
	/// </summary>
	[SerializeField]
	private List<AOC2Unit> _allies;
	
	/// <summary>
	/// The enemies.
	/// Serialized to view for debug purposes in editor.
	/// </summary>
	[SerializeField]
	private List<AOC2Unit> _enemies;
	
	public AOC2UnitSpawner[] _spawners;
	
	[SerializeField]
	Transform targetMarker;
	
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
		WarmPools();
		//SpawnNewWave(0);
	}
	
	/// <summary>
	/// Figures out how many of each unit we're going to need, then pre-warms
	/// the pools
	/// </summary>
	void WarmPools()
	{
		Dictionary<AOC2Spawnable, int> spawns = new Dictionary<AOC2Spawnable, int>();
		foreach (AOC2UnitSpawner item in _spawners) 
		{
			AOC2Math.MergeDicts<AOC2Spawnable>(spawns, item.GetSpawnDict());
		}
		
		foreach (KeyValuePair<AOC2Spawnable, int> item in spawns) 
		{
			AOC2ManagerReferences.poolManager.Warm(item.Key as AOC2Poolable, item.Value);
		}
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
			//AOC2EventManager.Combat.OnEnemiesClear();
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
	
	public void TargetUnit(AOC2Unit unit)
	{
		targetMarker.parent = unit.transform;
		targetMarker.localPosition = Vector3.zero;
		targetMarker.gameObject.SetActive(true);
	}
	
	public void TargetNone()
	{
		targetMarker.parent = transform;
		targetMarker.gameObject.SetActive(false);
	}
	
	public AOC2Unit GetClosestFromList(Vector3 pos, List<AOC2Unit> list)
	{
		AOC2Unit closest = null;
		float dist = float.NaN;
		for (int i = 0; i < list.Count; i++) 
		{
			if (closest == null)
			{
				closest = list[i];
				dist = AOC2Math.GroundDistanceSqr(pos, closest.aPos.position);
			}
			else
			{
				float checkDist = AOC2Math.GroundDistanceSqr(pos, list[i].aPos.position);
				if (checkDist < dist)
				{
					closest = list[i];
					dist = checkDist;
				}
			}
		}
		return closest;
	}
	
	public AOC2Unit GetClosestFromList(AOC2Unit toThis, List<AOC2Unit> list)
	{
		return GetClosestFromList(toThis.aPos.position, list);
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
	
	public AOC2Unit GetClosestPlayer(Vector3 pos)
	{
		return GetClosestFromList(pos, _allies);
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
	/// </param>
	public AOC2Unit GetClosestPlayerInRange(AOC2Unit toThis, float range)
	{
		return GetClosestPlayerInRange(toThis.aPos.position, range);
	}
	
	public AOC2Unit GetClosestPlayerInRange(Vector3 pos, float range)
	{
		//Faster to square the range than root the distance
		float sqrRange = range * range;
		
		AOC2Unit closest = GetClosestPlayer(pos);
		
		if (closest != null && AOC2Math.GroundDistanceSqr(pos, closest.aPos.position) < sqrRange)
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
	public void CoolAbility(AOC2Ability ability, AOC2Unit user)
	{
		StartCoroutine(ability.Cool(user));
	}
	
	void SpawnNewWave(int wave)
	{
		for (int i = 0; i < _spawners.Length; i++) 
		{
			//_spawners[i].SpawnWave(wave);
		}
	}
	
}
