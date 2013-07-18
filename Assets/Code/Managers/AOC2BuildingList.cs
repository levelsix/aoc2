using UnityEngine;
using System.Collections;
using com.lvl6.proto;

/// <summary>
/// @author Rob Giusti
/// Singleton list of StructProtos for each building, until we have
/// the server stuff up and running.
/// </summary>
public class AOC2BuildingList {
	
	public FullStructProto townHall = new FullStructProto();
	public FullStructProto goldColl = new FullStructProto();
	public FullStructProto goldStor = new FullStructProto();
	public FullStructProto tonicColl = new FullStructProto();
	public FullStructProto tonicStor = new FullStructProto();
	public FullStructProto blackSmith = new FullStructProto();
	public FullStructProto wizard = new FullStructProto();
	
	private static AOC2BuildingList _buildingList;
	
	public static AOC2BuildingList Get()
	{
		if (_buildingList == null)
		{
			_buildingList = new AOC2BuildingList();
		}
		return _buildingList;
	}
	
	private AOC2BuildingList()
	{
		goldColl.name = "Gold Collector";
		goldColl.income = 500;
		goldColl.minutesToBuild = 10;
		goldColl.minutesToUpgradeBase = 10;
		goldColl.price = 100;
		
		goldColl.minLevel.Add(0);
		goldColl.minLevel.Add(0);
		goldColl.minLevel.Add(1);
		
		goldColl.xLength = 2;
		goldColl.yLength = 2;
	}
	
}
