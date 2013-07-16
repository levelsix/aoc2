using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (AOC2Building))]
public class AOC2ResourceCollector : AOC2BuildingUpgrade {
	
	const int MIN_TO_COLLECT = 60;
	
	/// <summary>
	/// The resource type.
	/// </summary>
	[SerializeField]
	AOC2Values.Buildings.ResourceType resource;
	
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
	protected DateTime lastCollection;
	
	/// <summary>
	/// The overflow of resources that was unable to be
	/// collected on the last collection attempt. This hangs
	/// around separate from contents, since contents reflects
	/// the current amount since the last collection
	/// </summary>
	public int overflow;
	
	private AOC2Building building;
	
	[SerializeField]
	GameObject popup;
	
	void Awake()
	{
		building = GetComponent<AOC2Building>();
		building.OnSelect += Collect;
	}
	
	void Start()
	{
		//TODO build from a BuildingProto
		lastCollection = DateTime.UtcNow;
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
			lastCollection = DateTime.UtcNow;
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		CalcContents();
		
		//Set the popup's activity to whether this has enough to collect
		popup.SetActive(contents + overflow > MIN_TO_COLLECT);
	}
	
	/// <summary>
	/// Calculates the contents current contents of this collector
	/// </summary>
	void CalcContents()
	{
		TimeSpan span = DateTime.UtcNow - lastCollection;
		contents = (int)(span.TotalSeconds * hourlyRate / 3600);
		if (contents + overflow > capacity)
		{
			contents = capacity - overflow;
		}
	}
	
	public override void StartUpgrade ()
	{
		overflow += contents;
		base.StartUpgrade();
	}
	
	public override void FinishUpgrade ()
	{
		lastCollection = finishUpgradeTime;
		base.FinishUpgrade();
	}
	
}
