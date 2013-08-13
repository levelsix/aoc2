using UnityEngine;
using System.Collections;

/// <summary>
/// A collection of prefabs 
/// </summary>
public class AOC2CombatPrefabs : MonoBehaviour {
	
	public AOC2DamageText damageText;
	
	void Awake()
	{
		AOC2ManagerReferences.combatPrefabs = this;
	}
}
