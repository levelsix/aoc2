using UnityEngine;
using System.Collections;
using System;
using com.lvl6.aoc2.proto;

/// <summary>
/// @author Rob Giusti
/// Component attached to a building to turn it into
/// a resource collector.
/// Gathers resources over time
/// </summary>
[RequireComponent (typeof (AOC2Building))]
public class AOC2ResourceCollector : MonoBehaviour {
    
	const int MIN_TO_COLLECT = 60;
	
	/// <summary>
	/// The resource type.
	/// </summary>
	[SerializeField]
	ResourceType resource;
	
	/// <summary>
	/// The capacity.
	/// </summary>
	[SerializeField]
	int capacity = 5000;
	
	/// <summary>
	/// The current amount that has been generated
	/// since the last collection.
	/// </summary>
	public int contents = 0;
	
	/// <summary>
	/// The hourly rate at which this collector generates
	/// its resource.
	/// </summary>
	public int hourlyRate;
	
	/// <summary>
	/// The last collection.
	/// </summary>
	protected long lastCollection;
	
	/// <summary>
	/// The overflow of resources that was unable to be
	/// collected on the last collection attempt. This hangs
	/// around separate from contents, since contents reflects
	/// the current amount since the last collection
	/// </summary>
	public int overflow;
	
	/// <summary>
	/// Gets the total resources in this collector
	/// </summary>
	/// <value>
	/// The total.
	/// </value>
	public int total{
		get
		{
			return overflow + contents;
		}
	}
    
    private AOC2Building _building;
    
    private AOC2BuildingUpgrade _upgrade;
	
	/// <summary>
	/// The UI Popup that signifies that this collector
	/// has enough resources to be harvested
	/// </summary>
	[SerializeField]
	GameObject hasResourcesPopup;
	
    void Awake()
    {
        _building = GetComponent<AOC2Building>();
        _upgrade = GetComponent<AOC2BuildingUpgrade>();
    }
    
	/*
    /// <summary>
    /// Initialize, using the specified protocol.
    /// </summary>
    /// <param name='proto'>
    /// Protocol instance.
    /// </param>
    public void Init(FullUserStructProto proto)
    {
        hourlyRate = proto.fullStruct.income;
        lastCollection = proto.lastCollectTime;
    }
    */
	
	/// <summary>
	/// Raises the enable event.
	/// Register delegates
	/// </summary>
	void OnEnable()
	{
		_building.OnSelect += Collect;
        _upgrade.OnStartUpgrade += OnStartUpgrade;
        _upgrade.OnFinishUpgrade += OnFinishUpgrade;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Release delegates
	/// </summary>
	void OnDisable()
	{
		_building.OnSelect -= Collect;
        _upgrade.OnStartUpgrade -= OnStartUpgrade;
        _upgrade.OnFinishUpgrade -= OnFinishUpgrade;
	}
	
	void Init()
	{
		//TODO build from a BuildingProto
	}
	
	/// <summary>
	/// Collect this instance.
	/// </summary>
	void Collect()
	{
		if (overflow + contents > MIN_TO_COLLECT){
			overflow = AOC2ManagerReferences.resourceManager.AddResource(resource, contents + overflow);
			contents = 0;
			if (overflow > 0)
			{
				AOC2EventManager.Popup.CreatePopup("Not enough storage to store contents!");
			}
			lastCollection = AOC2Math.UnixTimeStamp(DateTime.UtcNow);
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		CalcContents();
		
		//Set the popup's activity to whether this has enough to collect
		//hasResourcesPopup.SetActive(contents + overflow > MIN_TO_COLLECT);
	}
	
	/// <summary>
	/// Calculates the contents current contents of this collector
	/// </summary>
	void CalcContents()
	{
		long secs = AOC2Math.UnixTimeStamp(DateTime.UtcNow) - lastCollection;
		contents = (int)(secs * hourlyRate / 3600);
		if (contents + overflow > capacity)
		{
			contents = capacity - overflow;
		}
	}
	
	public void OnStartUpgrade ()
	{
		overflow += contents;
	}
	
	public void OnFinishUpgrade ()
	{
		lastCollection = _upgrade.finishUpgradeTime;
	}
	
}
