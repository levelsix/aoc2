using UnityEngine;
using System.Collections;
using proto;

/// <summary>
/// @author Rob Giusti
/// A piece of equipment that adds its stats to its user's
/// </summary>
[System.Serializable]
public class AOC2Equipment {
	
	/// <summary>
	/// The name.
	/// </summary>
	public string name;
	
	/// <summary>
	/// The current durability.
	/// </summary>
	public int durability;
	
	/// <summary>
	/// The slot.
	/// </summary>
	public EquipmentType slot;
	
	/// <summary>
	/// The stats.
	/// </summary>
	public AOC2UnitStats stats;
	
	/// <summary>
	/// The rarity.
	/// </summary>
	public RarityType rarity;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2Equipment"/> class.
	/// </summary>
	/// <param name='proto'>
	/// Proto to derive values from.
	/// </param>
	public AOC2Equipment(EquipmentProto proto)
	{
		stats = new AOC2UnitStats();
		stats.strength = proto.attack;
		stats.defense = proto.defense;
		stats.maxHealth = proto.addHealth;
		stats.maxMana = proto.addMana;
		
		slot = proto.type;
		
		name = proto.name;
		
		rarity = proto.rarity;
		
		//TODO: Determine current durability
	}
	
}
