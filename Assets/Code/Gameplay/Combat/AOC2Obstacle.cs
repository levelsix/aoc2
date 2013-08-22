using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// An obstacle that blocks the player from continuing on to move of the level.
/// Points to a spawner before it, and destroys itself when all enemies from that spawn
/// point are defeated
/// </summary>
public class AOC2Obstacle : MonoBehaviour {
	
	[SerializeField]
	AOC2UnitSpawner spawnerTrigger;
	
	void OnEnable()
	{
		if (spawnerTrigger == null)
		{
			Debug.LogError("Spawner trigger not set in Obstacle");
		}
		else
		{
			spawnerTrigger.OnDefeat += OnTrigger;
		}
	}
	
	void OnDisable()
	{
		if (spawnerTrigger != null)
		{
			spawnerTrigger.OnDefeat -= OnTrigger;
		}
	}
	
	void OnTrigger()
	{
		gameObject.SetActive(false);
	}
	
}
