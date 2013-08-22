using UnityEngine;
using System.Collections;
using proto;

[System.Serializable]
public class AOC2Equipment {
	
	public string name;
	
	public int durability;
	
	public EquipmentType slot;

	public AOC2UnitStats stats;
	
	public RarityType rarity;
	
	public AOC2Equipment(EquipmentProto proto)
	{
		stats = new AOC2UnitStats();
		stats.strength = proto.attack;
		stats.defense = proto.defense;
		stats.maxHealth = proto.addHealth;
		stats.maxMana = proto.addMana;
		
		name = proto.name;
		
		rarity = proto.rarity;
	}
	
}
