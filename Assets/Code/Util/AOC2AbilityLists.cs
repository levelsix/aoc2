using UnityEngine;
using System.Collections;

public static class AOC2AbilityLists {

	public static class Warrior
	{
		public static AOC2Ability[] abilities =
		{
			baseAttackAbility,
			powerAttackAbility
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
		private const AOC2DeliveryType BASE_ATTACK_DELIVERY = AOC2DeliveryType.BOX;
		
		private const string POWER_ATTACK_NAME = "Power Attack";
		private const float POWER_ATTACK_DAMAGE = 1.65f;
		private const float POWER_ATTACK_LIFE = .3f;
		private const float POWER_ATTACK_SPEED = 0f;
		private const float POWER_ATTACK_OFFSET = 0f;
		private const float POWER_ATTACK_CAST = .3f;
		private const float POWER_ATTACK_COOL = 5f;
		private const float POWER_ATTACK_SIZE = 2f;
		private const bool POWER_ATTACK_TARGETTED = false;
		private const bool POWER_ATTACK_PERSIST = true;
		private const float POWER_ATTACK_RETARGET = 1f;
		private const AOC2DeliveryType POWER_ATTACK_DELIVERY = AOC2DeliveryType.BOX;
		
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
			BASE_ATTACK_COOL);
		
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
			POWER_ATTACK_COOL);
	}
	
	public static class Enemy
	{
		private const string BASE_ATTACK_NAME = "Base Attack";
		private const float BASE_ATTACK_DAMAGE = 1f;
		private const float BASE_ATTACK_LIFE = .1f;
		private const float BASE_ATTACK_SPEED = 0f;
		private const float BASE_ATTACK_OFFSET = .5f;
		private const float BASE_ATTACK_CAST = 0f;
		private const float BASE_ATTACK_COOL = .3f;
		private const float BASE_ATTACK_SIZE = 1f;
		private const bool BASE_ATTACK_TARGETTED = true;
		private const AOC2DeliveryType BASE_ATTACK_DELIVERY = AOC2DeliveryType.BOX;
				
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
			BASE_ATTACK_COOL);
	}
	
}
