using UnityEngine;
using System.Collections;
using proto;

public class AOC2UnitEquipment : MonoBehaviour {

	public AOC2Equipment[] equips = new AOC2Equipment[3];
	
	public AOC2Equipment this[EquipmentType equt]
	{
		get
		{
			return equips[(int)equt];
		}
		set
		{
			if (value.slot == equt){
				equips[(int)equt] = value;
			}
			else
			{
				Debug.LogError("Attempt to assign equipment to wrong slot");
			}
		}
	}
	
	public int this[AOC2Values.UnitStat stat]
	{
		get
		{
			int total = 0;
			foreach (AOC2Equipment item in equips) 
			{
				if (item != null)
				{

					total += item.stats[stat];
				}
			}
			return total;
		}
	}
	
}
