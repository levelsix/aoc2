using UnityEngine;
using System.Collections;

public static class AOC2AbilityLists {

	public static class Warrior
	{
		public static AOC2Ability[] abilities =
		{
			baseAttackAbility,
			powerAttackAbility,
			ironWillAbility,
			cleaveAbility
		};
		
		private const string BASE_ATTACK_NAME = "Base Attack";
		private const float BASE_ATTACK_DAMAGE = 1f;
		private const float BASE_ATTACK_LIFE = .1f;
		private const float BASE_ATTACK_SPEED = 0f;
		private const float BASE_ATTACK_OFFSET = .5f;
		private const float BASE_ATTACK_CAST = 0f;
		private const float BASE_ATTACK_COOL = .3f;
		private const float BASE_ATTACK_SIZE = 1f;
		private const bool BASE_ATTACK_TARGETTED = true;
		private const int BASE_ATTACK_MANA = 0;
		private const AOC2Values.Abilities.TargetType BASE_ATTACK_TARGET = AOC2Values.Abilities.TargetType.ENEMY;
		private const AOC2DeliveryType BASE_ATTACK_DELIVERY = AOC2DeliveryType.SPHERE;
		
		private const string POWER_ATTACK_NAME = "Power Attack";
		private const float POWER_ATTACK_DAMAGE = 1.65f;
		private const float POWER_ATTACK_LIFE = .3f;
		private const float POWER_ATTACK_SPEED = 0f;
		private const float POWER_ATTACK_OFFSET = .5f;
		private const float POWER_ATTACK_CAST = .3f;
		private const float POWER_ATTACK_COOL = 5f;
		private const float POWER_ATTACK_SIZE = 1.5f;
		private const bool POWER_ATTACK_TARGETTED = true;
		private const bool POWER_ATTACK_PERSIST = false;
		private const float POWER_ATTACK_RETARGET = 1f;
		private const int POWER_ATTACK_MANA = 15;
		private const AOC2Values.Abilities.TargetType POWER_ATTACK_TARGET = AOC2Values.Abilities.TargetType.ENEMY;
		private const AOC2DeliveryType POWER_ATTACK_DELIVERY = AOC2DeliveryType.SPHERE;
		
		private const string IRON_WILL_NAME = "Iron Will";
		private const float IRON_WILL_DURATION = 10f;
		private const float IRON_WILL_SCALE = 0.2f;
		private const AOC2Values.UnitStat IRON_WILL_STAT = AOC2Values.UnitStat.DEFENSE;
		private const float IRON_WILL_CAST = 0.5f;
		private const float IRON_WILL_COOL = 20f;
		private const int IRON_WILL_MANA = 30;
		private const AOC2Values.Abilities.TargetType IRON_WILL_TARGET = AOC2Values.Abilities.TargetType.SELF;
		
		private const string CLEAVE_NAME = "Cleave";
		private const float CLEAVE_DAMAGE = 1.4f;
		private const float CLEAVE_LIFE = .5f;
		private const float CLEAVE_SPEED = 0f;
		private const float CLEAVE_OFFSET = 0f;
		private const float CLEAVE_CAST = .4f;
		private const float CLEAVE_COOL = 10f;
		private const float CLEAVE_SIZE = 3f;
		private const bool CLEAVE_TARGETTED = false;
		private const bool CLEAVE_PERSIST = true;
		private const float CLEAVE_RETARGET = 1f;
		private const int CLEAVE_MANA = 35;
		private const AOC2Values.Abilities.TargetType CLEAVE_TARGET = AOC2Values.Abilities.TargetType.SELF;
		private const AOC2DeliveryType CLEAVE_DELIVERY = AOC2DeliveryType.SPHERE;
		
		private static readonly AOC2Attack baseAttack = new AOC2Attack(
			BASE_ATTACK_DAMAGE,
			BASE_ATTACK_LIFE, 
			BASE_ATTACK_SPEED,
			BASE_ATTACK_OFFSET,
			BASE_ATTACK_DELIVERY,
			BASE_ATTACK_SIZE);
		
		public static readonly AOC2AttackAbility baseAttackAbility = new AOC2AttackAbility(
			baseAttack,
			BASE_ATTACK_NAME,
			BASE_ATTACK_CAST, 
			BASE_ATTACK_COOL,
			BASE_ATTACK_MANA,
			BASE_ATTACK_TARGET);
		
		private static readonly AOC2Attack powerAttack = new AOC2Attack(
			POWER_ATTACK_DAMAGE,
			POWER_ATTACK_LIFE,
			POWER_ATTACK_SPEED,
			POWER_ATTACK_OFFSET,
			POWER_ATTACK_DELIVERY,
			POWER_ATTACK_SIZE,
			POWER_ATTACK_PERSIST,
			POWER_ATTACK_RETARGET);
		
		public static readonly AOC2AttackAbility powerAttackAbility = new AOC2AttackAbility(
			powerAttack,
			POWER_ATTACK_NAME,
			POWER_ATTACK_CAST, 
			POWER_ATTACK_COOL,
			POWER_ATTACK_MANA,
			POWER_ATTACK_TARGET);
		
		public static readonly AOC2BuffAbility ironWillAbility = new AOC2BuffAbility(
			IRON_WILL_NAME,
			IRON_WILL_DURATION,
			IRON_WILL_SCALE,
			IRON_WILL_STAT,
			IRON_WILL_CAST,
			IRON_WILL_COOL,
			IRON_WILL_MANA,
			IRON_WILL_TARGET);
		
		private static readonly AOC2Attack cleaveAttack = new AOC2Attack(
			CLEAVE_DAMAGE,
			CLEAVE_LIFE,
			CLEAVE_SPEED,
			CLEAVE_OFFSET,
			CLEAVE_DELIVERY,
			CLEAVE_SIZE,
			CLEAVE_PERSIST,
			CLEAVE_RETARGET);
		
		public static readonly AOC2AttackAbility cleaveAbility = new AOC2AttackAbility(
			cleaveAttack,
			CLEAVE_NAME,
			CLEAVE_CAST, 
			CLEAVE_COOL,
			CLEAVE_MANA,
			CLEAVE_TARGET);
	}
	
	public static class Archer
	{
		private const string BASE_ATTACK_NAME = "Base Attack";
		private const float BASE_ATTACK_DAMAGE = 1f;
		private const float BASE_ATTACK_LIFE = .4f;
		private const float BASE_ATTACK_SPEED = 20f;
		private const float BASE_ATTACK_OFFSET = .5f;
		private const float BASE_ATTACK_CAST = 0f;
		private const float BASE_ATTACK_COOL = .3f;
		private const float BASE_ATTACK_SIZE = .4f;
		private const bool BASE_ATTACK_TARGETTED = true;
		private const int BASE_ATTACK_MANA = 0;
		private const AOC2Values.Abilities.TargetType BASE_ATTACK_TARGET = AOC2Values.Abilities.TargetType.ENEMY;
		private const AOC2DeliveryType BASE_ATTACK_DELIVERY = AOC2DeliveryType.SPHERE;
		
		private const string FAN_SHOT_NAME = "Fan of Arrows";
		private const float FAN_SHOT_DAMAGE = 1.25f;
		private const float FAN_SHOT_LIFE = .5f;
		private const float FAN_SHOT_SPEED = 15f;
		private const float FAN_SHOT_OFFSET = .5f;
		private const float FAN_SHOT_CAST = .2f;
		private const float FAN_SHOT_COOL = 4f;
		private const float FAN_SHOT_SIZE = .4f;
		private const bool FAN_SHOT_TARGETTED = false;
		private const int FAN_SHOT_MANA = 0;
		private const bool FAN_SHOT_PERSIST = true;
		private const float FAN_SHOT_RETARGET = 1f;
		private const AOC2Values.Abilities.TargetType FAN_SHOT_TARGET = AOC2Values.Abilities.TargetType.ENEMY;
		private const AOC2DeliveryType FAN_SHOT_DELIVERY = AOC2DeliveryType.SPHERE;
		private const int FAN_SHOT_BULLETS = 7;
		private const float FAN_SHOT_ARC = 45f;
		
		private static readonly AOC2Attack baseAttack = new AOC2Attack(
			BASE_ATTACK_DAMAGE,
			BASE_ATTACK_LIFE, 
			BASE_ATTACK_SPEED,
			BASE_ATTACK_OFFSET,
			BASE_ATTACK_DELIVERY,
			BASE_ATTACK_SIZE);
		
		public static readonly AOC2AttackAbility baseAttackAbility = new AOC2AttackAbility(
			baseAttack,
			BASE_ATTACK_NAME,
			BASE_ATTACK_CAST, 
			BASE_ATTACK_COOL,
			BASE_ATTACK_MANA,
			BASE_ATTACK_TARGET);
		
		private static readonly AOC2Attack fanAttack = new AOC2Attack(
			FAN_SHOT_DAMAGE,
			FAN_SHOT_LIFE, 
			FAN_SHOT_SPEED,
			FAN_SHOT_OFFSET,
			FAN_SHOT_DELIVERY,
			FAN_SHOT_SIZE,
			FAN_SHOT_PERSIST,
			FAN_SHOT_RETARGET);
		
		public static readonly AOC2AttackAbility fanAttackAbility = new AOC2ArcMultiAttackAbility(
			fanAttack,
			FAN_SHOT_NAME,
			FAN_SHOT_CAST, 
			FAN_SHOT_COOL,
			FAN_SHOT_MANA,
			FAN_SHOT_TARGET,
			FAN_SHOT_BULLETS,
			FAN_SHOT_ARC);
	}
	
	public static class Enemy
	{
		private const string BASE_ATTACK_NAME = "Base Attack";
		private const float BASE_ATTACK_DAMAGE = 1f;
		private const float BASE_ATTACK_LIFE = .1f;
		private const float BASE_ATTACK_SPEED = 0f;
		private const float BASE_ATTACK_OFFSET = .5f;
		private const float BASE_ATTACK_CAST = 0f;
		private const float BASE_ATTACK_COOL = 1f;
		private const float BASE_ATTACK_SIZE = 1f;
		private const bool BASE_ATTACK_TARGETTED = true;
		private const int BASE_ATTACK_MANA = 0;
		private const AOC2Values.Abilities.TargetType BASE_ATTACK_TARGET = AOC2Values.Abilities.TargetType.ENEMY;
		private const AOC2DeliveryType BASE_ATTACK_DELIVERY = AOC2DeliveryType.SPHERE;
				
		private static readonly AOC2Attack baseAttack = new AOC2Attack(
			BASE_ATTACK_DAMAGE,
			BASE_ATTACK_LIFE, 
			BASE_ATTACK_SPEED,
			BASE_ATTACK_OFFSET,
			BASE_ATTACK_DELIVERY,
			BASE_ATTACK_SIZE);
		
		public static readonly AOC2AttackAbility baseAttackAbility = new AOC2AttackAbility(
			baseAttack,
			BASE_ATTACK_NAME,
			BASE_ATTACK_CAST, 
			BASE_ATTACK_COOL,
			BASE_ATTACK_MANA,
			BASE_ATTACK_TARGET);
	}
	
}
