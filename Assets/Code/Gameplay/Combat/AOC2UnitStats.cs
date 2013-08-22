using UnityEngine;
using System.Collections;
using proto;

/// <summary>
/// @author Rob Giusti
/// Wrapper for a unit's stats. Editable from inside
/// the editor. Can be indexed using the [] operator
/// and the UnitStat enum. Can be added together using
/// the addition operator.
/// </summary>
[System.Serializable]
public class AOC2UnitStats {
	
	public int strength = 10;
	public int defense = 10;
	public int resistance = 100;
	public int moveSpeed = 5;
	public int attackSpeed = 1;
	public int maxMana = 100;
	public int maxHealth = 100;
	
	/// <summary>
	/// Empty constructor.
	/// Initializes a new instance of the <see cref="AOC2UnitStats"/> class with
	/// default values.
	/// </summary>
	public AOC2UnitStats(){}
	
	/// <summary>
	/// Gets the <see cref="AOC2UnitStats"/> with the specified stat.
	/// </summary>
	/// <param name='stat'>
	/// Stat.
	/// </param>
	public int this[AOC2Values.UnitStat stat]
	{
		get
		{
			return GetStat(stat);
		}
		set
		{
			SetStat(stat, value);
		}
	}
	
	public int this[SpellProto.UnitStat stat]
	{
		get
		{
			return GetStat((AOC2Values.UnitStat)stat);
		}
		set
		{
			SetStat(stat, value);
		}
	}
	
	/// <summary>
	/// Indexer using an enum so that we can access
	/// a particular stat just by knowing which stat
	/// we want to access
	/// </summary>
	/// <param name='stat'>
	/// Stat.
	/// </param>
	public int this[int i]
	{
		get
		{
			return GetStat((AOC2Values.UnitStat)i);
		}
		set
		{
			SetStat((AOC2Values.UnitStat)i, value);
		}
	}
	
	/// <summary>
	/// Gets the stat according to the enum
	/// </summary>
	/// <returns>
	/// The stat.
	/// </returns>
	/// <param name='stat'>
	/// Stat.
	/// </param>
	int GetStat(AOC2Values.UnitStat stat)
	{			
		switch(stat)
		{
			case AOC2Values.UnitStat.STRENGTH:
				return strength;
			case AOC2Values.UnitStat.DEFENSE:
				return defense;
			case AOC2Values.UnitStat.RESISTANCE:
				return resistance;
			case AOC2Values.UnitStat.MOVE_SPEED:
				return moveSpeed;
			case AOC2Values.UnitStat.ATTACK_SPEED:
				return attackSpeed;
			case AOC2Values.UnitStat.MANA:
				return maxMana;
			case AOC2Values.UnitStat.HEALTH:
				return maxHealth;
			default:
				return 0;
		}
	}
	
	void SetStat(SpellProto.UnitStat stat, int val)
	{
		SetStat((AOC2Values.UnitStat)stat, val);
	}
	
	/// <summary>
	/// Sets the stat.
	/// </summary>
	/// <param name='stat'>
	/// Stat.
	/// </param>
	/// <param name='val'>
	/// Value.
	/// </param>
	void SetStat(AOC2Values.UnitStat stat, int val)
	{
		switch(stat)
		{
			case AOC2Values.UnitStat.STRENGTH:
				strength = val; 
				break;
			case AOC2Values.UnitStat.DEFENSE:
				defense = val; 
				break;
			case AOC2Values.UnitStat.RESISTANCE:
				resistance = val; 
				break;
			case AOC2Values.UnitStat.MOVE_SPEED:
				moveSpeed = val; 
				break;
			case AOC2Values.UnitStat.ATTACK_SPEED:
				attackSpeed = val; 
				break;
			case AOC2Values.UnitStat.MANA:
				maxMana = val; 
				break;
			case AOC2Values.UnitStat.HEALTH:
				maxHealth = val; 
				break;
			default:
				break;
		}
	}
	
	/// <summary>
	/// Addition overload; loads two sets of stats together.
	/// Used to add equipment to a character's base stats
	/// </summary>
	/// <param name='us1'>
	/// The first <see cref="AOC2UnitStats"/> to add.
	/// </param>
	/// <param name='us2'>
	/// The second <see cref="AOC2UnitStats"/> to add.
	/// </param>
	/// <returns>
	/// The <see cref="AOC2UnitStats"/> that is the sum of the values of <c>us1</c> and <c>us2</c>.
	/// </returns>
	public static AOC2UnitStats operator +(AOC2UnitStats us1, AOC2UnitStats us2)
	{
		AOC2UnitStats stats = new AOC2UnitStats();
		for (int i = 0; i < (int)AOC2Values.UnitStat.COUNT; i++) {
			stats[i] = us1[i] + us2[i];
		}
		return stats;
	}
}
