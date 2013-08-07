using UnityEngine;
using System.Collections;
using System;
using com.lvl6.aoc2.proto;

/// <summary>
/// @author Rob Giusti
/// Component of a building that mananges upgrading,
/// including timing and cost.
/// </summary>
[RequireComponent (typeof (AOC2Building))]
public class AOC2BuildingUpgrade : MonoBehaviour {
	
	/// <summary>
	/// The level.
    /// Level zero buildings are those that are still building,
    /// that way all building/upgrading is tied into a single system
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
	long[] upgradeSpan;
	
	/// <summary>
	/// The upgrade resource.
	/// </summary>
	[SerializeField]
	ResourceType upgradeResource;
	
	/// <summary>
	/// The UI Popup
	/// </summary>
	[SerializeField]
	GameObject upgradePopup;
	
	/// <summary>
	/// The finish upgrade time.
	/// </summary>
	public long finishUpgradeTime;
	
	/// <summary>
	/// The upgrading flag
	/// </summary>
	protected bool upgrading;
	
    /// <summary>
    /// The building component.
    /// </summary>
	protected AOC2Building building;
	
    /// <summary>
    /// The on start upgrade event.
    /// </summary>
    public Action OnStartUpgrade;
    
    /// <summary>
    /// The on upgrade complete event.
    /// </summary>
	public Action OnFinishUpgrade;
	
	/// <summary>
	/// Gets the gems to finish this upgrade immediately
	/// </summary>
	/// <value>
	/// The gems to finish.
	/// </value>
	public int gemsToFinish{
		get
		{
			return AOC2Math.GemsForTime(finishUpgradeTime - AOC2Math.UnixTimeStamp(DateTime.UtcNow));
		}
	}
	
	/// <summary>
	/// Awake this instance.
	/// Set up internal component references
	/// </summary>
	public void Awake()
	{
		building = GetComponent<AOC2Building>();
	}
    
    void Init()
    {
        
    }
	
	void OnSelect()
	{
		upgradePopup.SetActive(true);
	}
	
	void OnDeselect()
	{
		upgradePopup.SetActive(false);
	}
	
	/// <summary>
	/// Spends the cash and starts the upgrade timer
	/// </summary>
	public virtual void StartUpgrade()
	{
		if (level < maxLevel && AOC2ManagerReferences.resourceManager.SpendResource(upgradeResource, upgradeCost[level]))
		{
			finishUpgradeTime = AOC2Math.UnixTimeStamp(DateTime.UtcNow) + upgradeSpan[level];
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
		
		
		if (AOC2Math.UnixTimeStamp(DateTime.UtcNow) > finishUpgradeTime)
		{
			FinishUpgrade();
		}
	}
	
	/// <summary>
	/// Finishes the upgrade with gems.
	/// </summary>
	public void FinishWithGems()
	{
		if (AOC2ManagerReferences.resourceManager.SpendResource(ResourceType.GEMS, gemsToFinish))
		{
			FinishUpgrade();
		}
	}
	
	/// <summary>
	/// Finishes the upgrade.
	/// </summary>
	public virtual void FinishUpgrade()
	{
        //TODO: Intercept this with a server check UpgradeNormStructure
        
		upgrading = false;
		level++;
        
        if (OnFinishUpgrade != null)
        {
            OnFinishUpgrade();  
        }
	}
}
