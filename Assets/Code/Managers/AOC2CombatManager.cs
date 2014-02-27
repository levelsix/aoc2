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
	
	public AOC2Objective currObjective;
	
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
		//BuildSpawners();
		//SetObjective(currObjective);
		//SpawnNewWave(0);
	}
	
	/// <summary>
	/// Set up delegates
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Combat.OnSpawnEnemy += OnSpawnEnemy;
		AOC2EventManager.Combat.OnSpawnPlayer += OnSpawnPlayer;
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
		AOC2EventManager.Combat.OnPlayerVictory += OnPlayerVictory;
		AOC2EventManager.Combat.OnPlayerDeath += OnPlayerDeath;
		AOC2EventManager.Combat.OnObjectiveComplete += SetObjective;
	}
	
	/// <summary>
	/// Release delegates
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Combat.OnSpawnEnemy -= OnSpawnEnemy;
		AOC2EventManager.Combat.OnSpawnPlayer -= OnSpawnPlayer;
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
		AOC2EventManager.Combat.OnPlayerVictory -= OnPlayerVictory;
		AOC2EventManager.Combat.OnPlayerDeath -= OnPlayerDeath;
		AOC2EventManager.Combat.OnObjectiveComplete -= SetObjective;
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
	
	#region Objectives & Compass
	
	public void SetObjective(AOC2Objective objective)
	{
		if (currObjective != null)
		{
			currObjective.gameObject.SetActive(false);
		}
		
		currObjective = objective.next;
		
		if (currObjective != null)
		{
			currObjective.gameObject.SetActive(true);
			if (currObjective.OnSetObjective != null)
			{
				currObjective.OnSetObjective();
			}
		}
		else
		{
			AOC2EventManager.Combat.OnPlayerVictory();
		}
	}
	
	#endregion
	
}
