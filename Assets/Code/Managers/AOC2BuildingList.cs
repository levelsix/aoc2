using UnityEngine;
using System.Collections;
using proto;

/// <summary>
/// @author Rob Giusti
/// Singleton list of StructProtos for each building, until we have
/// the server stuff up and running.
/// </summary>
public class AOC2BuildingList {
	
	public StructureProto townHall = new StructureProto();
	public StructureProto goldColl = new StructureProto();
	public StructureProto goldStor = new StructureProto();
	public StructureProto tonicColl = new StructureProto();
	public StructureProto tonicStor = new StructureProto();
	public StructureProto blackSmith = new StructureProto();
	public StructureProto wizard = new StructureProto();
	
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
		goldColl.function = StructureProto.StructureFunction.INCOME;
		goldColl.functionResource = ResourceType.GOLD;
		goldColl.functionValue = 500;
		goldColl.buildTime = 10;
		goldColl.buildCost = 100;
		
		goldColl.size = new CoordinateProto();
		
		goldColl.size.x = 2;
		goldColl.size.y = 2;
	}
	
}
