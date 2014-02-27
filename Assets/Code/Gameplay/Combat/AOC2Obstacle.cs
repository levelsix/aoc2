using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// An obstacle that blocks the player from continuing on to move of the level.
/// Points to a spawner before it, and destroys itself when all enemies from that spawn
/// point are defeated
/// </summary>
public class AOC2Obstacle : MonoBehaviour {
	
	/// <summary>
	/// The spawn point that must be completed to remove this
	/// obstacle
	/// </summary>
	[SerializeField]
	AOC2UnitSpawner spawnerTrigger;
	
	/// <summary>
	/// Raises the enable event.
	/// Registers event delegates, insuring that a spawn trigger is assigned
	/// </summary>
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
	
	/// <summary>
	/// Releases event delegates
	/// </summary>
	void OnDisable()
	{
		spawnerTrigger.OnDefeat -= OnTrigger;
	}
	
	/// <summary>
	/// When triggered by a spawn point's completion, disables itself
	/// </summary>
	void OnTrigger()
	{
		gameObject.SetActive(false);
	}
	
}
