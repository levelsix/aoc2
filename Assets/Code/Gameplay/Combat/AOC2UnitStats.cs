using UnityEngine;
using System.Collections;
using com.lvl6.aoc2.proto;

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
			AOC2Values.UnitStat stat = (AOC2Values.UnitStat) i;
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
		set
		{
			AOC2Values.UnitStat stat = (AOC2Values.UnitStat) i;
			switch(stat)
			{
				case AOC2Values.UnitStat.STRENGTH:
					strength = value; 
					break;
				case AOC2Values.UnitStat.DEFENSE:
					defense = value; 
					break;
				case AOC2Values.UnitStat.RESISTANCE:
					resistance = value; 
					break;
				case AOC2Values.UnitStat.MOVE_SPEED:
					moveSpeed = value; 
					break;
				case AOC2Values.UnitStat.ATTACK_SPEED:
					attackSpeed = value; 
					break;
				case AOC2Values.UnitStat.MANA:
					maxMana = value; 
					break;
				case AOC2Values.UnitStat.HEALTH:
					maxHealth = value; 
					break;
				default:
					break;
			}
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
