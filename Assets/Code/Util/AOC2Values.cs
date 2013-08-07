using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using com.lvl6.aoc2.proto;

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
		
		public static int BUILDING =   8;
		public static int PLAYER = 9;
		public static int ENEMY = 10;
		
		public static int TARGET_PLAYER = 13;
		public static int TARGET_ENEMY = 14;
		public static int TARGET_ALL = 15;
		
		public static int TOUCH_PLAYER = 17;
		public static int TOUCH_ENEMY = 18; 
		
		public static int UI = 20;
	}
    
    public static class Scene
    {
        public enum Scenes
        {
            BUILDING_TEST_SCENE,
            COMBAT_TEST_SCENE
        }
        public static Dictionary<Scenes, string> sceneDict = new Dictionary<Scenes, string>()
        {
            {Scenes.BUILDING_TEST_SCENE, "BuildingTestScene"},
            {Scenes.COMBAT_TEST_SCENE, "CombatTestScene"}
        };
        
        public static void ChangeScene(Scenes scene)
        {
            UnityEngine.Application.LoadLevel(sceneDict[scene]);
        }
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

	public static class Animations
	{
		public enum Anim
		{
			IDLE = 0,
			WALK = 1,
			RUN = 2,
			ATTACK = 3,
			BIG_ATTACK = 4,
			HURT = 5,
			DEATH = 6,
			ABILITY_ONE = 7,
			ABILITY_TWO = 8,
			ABILITY_THREE = 9
		}

		public static Dictionary<MonsterType, Dictionary<Anim, string>> anims = new Dictionary<MonsterType, Dictionary<Anim, string>>()
			{
				{
					MonsterType.ROCK_KING, new Dictionary<Anim, string>()
					{
						{Anim.IDLE, "Idle"},
						{Anim.WALK, "Walk"},
						{Anim.RUN, "Run"},
						{Anim.ATTACK, "Attack3-1_copy"},
						{Anim.BIG_ATTACK, "Attack3-3"},
						{Anim.HURT, "Wound"},
						{Anim.DEATH, "Death"},
						{Anim.ABILITY_ONE, "Attack3-2_copy"},
						{Anim.ABILITY_TWO, "Magic_copy"},
						{Anim.ABILITY_THREE, "Attack1_copy"}
					}
				},
				{
					MonsterType.SKELETON, new Dictionary<Anim, string>()
					{
						{Anim.IDLE, "idle"},
						{Anim.WALK, "walk"},
						{Anim.RUN, "run"},
						{Anim.ATTACK, "attack_stab"},
						{Anim.BIG_ATTACK, "attack_stab"},
						{Anim.HURT, "damage_right"},
						{Anim.DEATH, "death"}
					}
				}
			};
		
	}
}
