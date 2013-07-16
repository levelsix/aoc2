using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Wrapper for a unit's stats. Editable from inside
/// the editor. Can be indexed using the [] operator
/// and the UnitStat enum.
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
}
