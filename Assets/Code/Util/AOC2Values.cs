using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// A collection of commonly used magic values, grouped for
/// maximum readibility
/// </summary>
public static class AOC2Values 
{
	public static class Layers
	{
		public static int DEFAULT = 0;
		public static int TRANSPARENT_FX = 1;
		public static int IGNORE_RAYCAST = 2;
		public static int WATER = 4;
		
		public static int BUILDING = 8;
		public static int PLAYER = 9;
		public static int ENEMY = 10;
		
		public static int TARGET_PLAYER = 13;
		public static int TARGET_ENEMY = 14;
		public static int TARGET_ALL = 15;
		
		public static int TOUCH_PLAYER = 17;
		public static int TOUCH_ENEMY = 18;
		
		public static int UI = 20;
	}
	
	public enum UnitStat
	{
		STRENGTH = 0,
		DEFENSE = 1,
		RESISTANCE = 2,
		MOVE_SPEED = 3,
		ATTACK_SPEED = 4,
		MANA = 5,
		HEALTH = 6,
		COUNT = 7 //Used whenever we need the length of the stat list
	}
	
	public static class Abilities
	{
		public enum TargetType
		{
			ENEMY = 0,
			SELF = 1
		}
	}
	
	public static class Buildings
	{
		public enum ResourceType
		{
			GOLD = 0,
			TONIC = 1,
			GEMS = 2
		}
	}
}
