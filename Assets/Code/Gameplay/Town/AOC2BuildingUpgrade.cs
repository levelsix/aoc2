using UnityEngine;
using System.Collections;
using System;

public abstract class AOC2BuildingUpgrade : MonoBehaviour {
	
	/// <summary>
	/// The level.
	/// From [0,maxLevel)
	/// </summary>
	protected int level;
	
	/// <summary>
	/// The max level.
	/// </summary>
	protected int maxLevel;
	
	/// <summary>
	/// The upgrade costs
	/// </summary>
	[SerializeField]
	int[] upgradeCost;
	
	/// <summary>
	/// The upgrade spants
	/// </summary>
	[SerializeField]
	TimeSpan[] upgradeSpan;
	
	/// <summary>
	/// The upgrade resource.
	/// </summary>
	[SerializeField]
	AOC2Values.Buildings.ResourceType upgradeResource;
	
	/// <summary>
	/// The finish upgrade time.
	/// </summary>
	protected DateTime finishUpgradeTime;
	
	/// <summary>
	/// The upgrading flag
	/// </summary>
	protected bool upgrading;
	
	/// <summary>
	/// Because levels index from zero but the player thinks
	/// levels start at one, this is the offset between what
	/// the internal system understands as the level and what
	/// the player thinks the level is.
	/// </summary>
	/// <value>
	/// The display level.
	/// </value>
	public int displayLevel{
		get
		{
			return level + 1;
		}
	}
	
	/// <summary>
	/// Gets the gems to finish this upgrade immediately
	/// </summary>
	/// <value>
	/// The gems to finish.
	/// </value>
	public int gemsToFinish{
		get
		{
			return AOC2Math.GemsForTime(finishUpgradeTime - DateTime.UtcNow);
		}
	}
	
	/// <summary>
	/// Spends the cash and starts the upgrade timer
	/// </summary>
	public virtual void StartUpgrade()
	{
		if (level < maxLevel - 1 && AOC2ManagerReferences.resourceManager.SpendResource(upgradeResource, upgradeCost[level]))
		{
			finishUpgradeTime = DateTime.UtcNow + upgradeSpan[level];
			upgrading = true;
		}
	}
	
	/// <summary>
	/// Checks if the building has past the upgrade timer
	/// </summary>
	public virtual void CheckUpgrade()
	{
		//Short the function if not upgrading
		if (!upgrading)
		{
			Debug.LogError("Should be checking if upgrading before calling CheckUpgrade()");
			return;
		}
		
		
		if (DateTime.Now > finishUpgradeTime)
		{
			FinishUpgrade();
		}
	}
	
	/// <summary>
	/// Finishes the upgrade with gems.
	/// </summary>
	public void FinishWithGems()
	{
		if (AOC2ManagerReferences.resourceManager.SpendResource(AOC2Values.Buildings.ResourceType.GEMS, gemsToFinish))
		{
			FinishUpgrade();
		}
	}
	
	/// <summary>
	/// Finishes the upgrade.
	/// </summary>
	public virtual void FinishUpgrade()
	{
		upgrading = false;
		level++;
	}
}
