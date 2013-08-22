using UnityEngine;
using System.Collections;
using proto;

/// <summary>
/// @author Rob Giusti
/// 
/// Compendium of all abilities for all characters.
/// 
/// TODO: Move all this to server-side, so that
/// we can make changes to abilities without having
/// to patch the entire game.
/// </summary>
public class AOC2AbilityLists : MonoBehaviour{
 
	static bool defined = false;
	
	public static class Generic {
	
		public static SpellProto baseMeleeAttackProto = new SpellProto();
		
		public static SpellProto baseRangeAttackProto = new SpellProto();
			
	}
	
	void Awake()
	{
		AOC2ManagerReferences.abilityList = this;
		
		if (!defined)
		{
			
			#region Generic
			
			#region Basic Melee
			Generic.baseMeleeAttackProto.name = "Basic Melee Attack";
			Generic.baseMeleeAttackProto.classType = ClassType.ALL;
			Generic.baseMeleeAttackProto.function = SpellProto.SpellFunctionType.ATTACK;
			Generic.baseMeleeAttackProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Generic.baseMeleeAttackProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Generic.baseMeleeAttackProto.strength = 1f;
			Generic.baseMeleeAttackProto.force = 2f;
			Generic.baseMeleeAttackProto.manaCost = 0;
			Generic.baseMeleeAttackProto.deliveryDuration = 0f;
			Generic.baseMeleeAttackProto.numberParticles = 1;
			Generic.baseMeleeAttackProto.particleSpeed = 0f;
			Generic.baseMeleeAttackProto.particleDuration = .1f;
			Generic.baseMeleeAttackProto.hitsPerParticle = 1;
			Generic.baseMeleeAttackProto.deliverySpeed = 0f;
			Generic.baseMeleeAttackProto.targetted = true;
			Generic.baseMeleeAttackProto.size = 1f;
			Generic.baseMeleeAttackProto.castTime = .3f;
			Generic.baseMeleeAttackProto.coolDown = 1f;
			Generic.baseMeleeAttackProto.stat = SpellProto.UnitStat.STRENGTH;
			#endregion
			#region Basic Range
			Generic.baseRangeAttackProto.name = "Basic Range Attack";
			Generic.baseRangeAttackProto.classType = ClassType.ALL;
			Generic.baseRangeAttackProto.function = SpellProto.SpellFunctionType.ATTACK;
			Generic.baseRangeAttackProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Generic.baseRangeAttackProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Generic.baseRangeAttackProto.strength = 1f;
			Generic.baseRangeAttackProto.force = 1f;
			Generic.baseRangeAttackProto.manaCost = 0;
			Generic.baseRangeAttackProto.deliveryDuration = 0f;
			Generic.baseRangeAttackProto.numberParticles = 1;
			Generic.baseRangeAttackProto.particleSpeed = 10f;
			Generic.baseRangeAttackProto.particleDuration = .5f;
			Generic.baseRangeAttackProto.hitsPerParticle = 1;
			Generic.baseRangeAttackProto.deliverySpeed = 0f;
			Generic.baseRangeAttackProto.targetted = true;
			Generic.baseRangeAttackProto.size = .5f;
			Generic.baseRangeAttackProto.castTime = .2f;
			Generic.baseRangeAttackProto.coolDown = 1f;
			Generic.baseRangeAttackProto.stat = SpellProto.UnitStat.STRENGTH;
			#endregion
			
			#endregion
			
			#region Warrior
			
			#region Power Attack
			Warrior.powerAttackProto.name = "Power Attack";
			Warrior.powerAttackProto.classType = ClassType.WARRIOR;
			Warrior.powerAttackProto.function = SpellProto.SpellFunctionType.ATTACK;
			Warrior.powerAttackProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Warrior.powerAttackProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Warrior.powerAttackProto.strength = 1.65f;
			Warrior.powerAttackProto.manaCost = 15;
			Warrior.powerAttackProto.force = 3f;
			Warrior.powerAttackProto.deliveryDuration = 0f;
			Warrior.powerAttackProto.numberParticles = 1;
			Warrior.powerAttackProto.particleSpeed = 0f;
			Warrior.powerAttackProto.particleDuration = 0.3f;
			Warrior.powerAttackProto.hitsPerParticle = 1;
			Warrior.powerAttackProto.deliverySpeed = 0f;
			Warrior.powerAttackProto.targetted = true;
			Warrior.powerAttackProto.size = 1.5f;
			Warrior.powerAttackProto.castTime = 0.3f;
			Warrior.powerAttackProto.coolDown = 5f;
			Warrior.powerAttackProto.stat = SpellProto.UnitStat.STRENGTH;
			#endregion
			#region Iron Will
			Warrior.ironWillProto.name = "Iron Will";
			Warrior.ironWillProto.classType = ClassType.WARRIOR;
			Warrior.ironWillProto.function = SpellProto.SpellFunctionType.BUFF;
			Warrior.ironWillProto.targetType = SpellProto.SpellTargetType.SELF;
			Warrior.ironWillProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Warrior.ironWillProto.strength = .2f;
			Warrior.ironWillProto.manaCost = 30;
			Warrior.ironWillProto.force = 0f;
			Warrior.ironWillProto.deliveryDuration = 10f;
			Warrior.ironWillProto.numberParticles = 1;
			Warrior.ironWillProto.particleSpeed = 0;
			Warrior.ironWillProto.particleDuration = 0.3f;
			Warrior.ironWillProto.hitsPerParticle = 1;
			Warrior.ironWillProto.deliverySpeed = 0;
			Warrior.ironWillProto.targetted = true;
			Warrior.ironWillProto.size = 1f;
			Warrior.ironWillProto.castTime = 0.5f;
			Warrior.ironWillProto.coolDown = 20f;
			Warrior.ironWillProto.stat = SpellProto.UnitStat.RESISTANCE;
			#endregion
			#region Cleave
			Warrior.cleaveProto.name = "Cleave";
			Warrior.cleaveProto.classType = ClassType.WARRIOR;
			Warrior.cleaveProto.function = SpellProto.SpellFunctionType.ATTACK;
			Warrior.cleaveProto.targetType = SpellProto.SpellTargetType.SELF;
			Warrior.cleaveProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Warrior.cleaveProto.strength = 1.3f;
			Warrior.cleaveProto.manaCost = 35;
			Warrior.cleaveProto.force = 5f;
			Warrior.cleaveProto.deliveryDuration = 10f;
			Warrior.cleaveProto.numberParticles = 1;
			Warrior.cleaveProto.particleSpeed = 0;
			Warrior.cleaveProto.particleDuration = 0.3f;
			Warrior.cleaveProto.hitsPerParticle = 10;
			Warrior.cleaveProto.deliverySpeed = 0;
			Warrior.cleaveProto.targetted = false;
			Warrior.cleaveProto.size = 3;
			Warrior.cleaveProto.castTime = 0f;
			Warrior.cleaveProto.coolDown = 10f;
			Warrior.cleaveProto.retargetTime = 1f;
			Warrior.cleaveProto.stat = SpellProto.UnitStat.STRENGTH;
			#endregion
			
			#endregion
			
			#region Archer
			
			#region Power Shot
			Archer.powerShotProto.name = "Power Shot";
			Archer.powerShotProto.stat = SpellProto.UnitStat.STRENGTH;
			Archer.powerShotProto.function = SpellProto.SpellFunctionType.ATTACK;
			Archer.powerShotProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Archer.powerShotProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Archer.powerShotProto.strength = 1.25f;
			Archer.powerShotProto.manaCost = 10;
			Archer.powerShotProto.force = 3;
			Archer.powerShotProto.deliveryDuration = 0;
			Archer.powerShotProto.deliverySpeed = 0;
			Archer.powerShotProto.particleDuration = 0.4f;
			Archer.powerShotProto.particleSpeed = 20f;
			Archer.powerShotProto.numberParticles = 1;
			Archer.powerShotProto.size = 0.5f;
			Archer.powerShotProto.castTime = .3f;
			Archer.powerShotProto.coolDown = 4;
			Archer.powerShotProto.targetted = true;
			#endregion
			#region Marksman
			Archer.marksmanProto.name = "Marksman";
			Archer.marksmanProto.stat = SpellProto.UnitStat.ATTACK_SPEED;
			Archer.marksmanProto.function = SpellProto.SpellFunctionType.BUFF;
			Archer.marksmanProto.targetType = SpellProto.SpellTargetType.SELF;
			Archer.marksmanProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Archer.marksmanProto.strength = 20;
			Archer.marksmanProto.manaCost = 30;
			Archer.marksmanProto.force = 3;
			Archer.marksmanProto.deliveryDuration = 0;
			Archer.marksmanProto.particleDuration = .1f;
			Archer.marksmanProto.numberParticles = 1;
			Archer.marksmanProto.size = .1f;
			Archer.marksmanProto.castTime = .5f;
			Archer.marksmanProto.coolDown = 20f;
			Archer.marksmanProto.targetted = true;
			#endregion
			#region Fan Shot
			Archer.fanShotProto.name = "Fan of Arrows";
			Archer.fanShotProto.stat = SpellProto.UnitStat.STRENGTH;
			Archer.fanShotProto.function = SpellProto.SpellFunctionType.ATTACK;
			Archer.fanShotProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Archer.fanShotProto.directionType = SpellProto.SpellDirectionType.ARC;
			Archer.fanShotProto.strength = 1.1f;
			Archer.fanShotProto.manaCost = 35;
			Archer.fanShotProto.force = 1.5f;
			Archer.fanShotProto.deliveryDuration = .5f;
			Archer.fanShotProto.particleDuration = .5f;
			Archer.fanShotProto.particleSpeed = 15;
			Archer.fanShotProto.numberParticles = 7;
			Archer.fanShotProto.size = .3f;
			Archer.fanShotProto.castTime = .2f;
			Archer.fanShotProto.coolDown = 10;
			Archer.fanShotProto.angle = 120;
			Archer.fanShotProto.retargetTime = 1;
			Archer.fanShotProto.hitsPerParticle = 1;
			#endregion
			#region Arrow Rain
			Archer.arrowRainProto.name = "Rain of Arrows";
			Archer.arrowRainProto.stat = SpellProto.UnitStat.STRENGTH;
			Archer.arrowRainProto.function = SpellProto.SpellFunctionType.ATTACK;
			Archer.arrowRainProto.targetType = SpellProto.SpellTargetType.SELF;
			Archer.arrowRainProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Archer.arrowRainProto.strength = 1.3f;
			Archer.arrowRainProto.manaCost = 20;
			Archer.arrowRainProto.force = 1;
			Archer.arrowRainProto.deliveryDuration = 1;
			Archer.arrowRainProto.particleDuration = .2f;
			Archer.arrowRainProto.numberParticles = 20;
			Archer.arrowRainProto.size = .2f;
			Archer.arrowRainProto.area = 5;
			Archer.arrowRainProto.castTime = .5f;
			Archer.arrowRainProto.coolDown = 10;
			Archer.arrowRainProto.retargetTime = .5f;
			#endregion
			
			#endregion
			
			#region Wizard
			
			#region Lightning Strike
			Wizard.lightningStrikeProto.name = "Lightning Strike";
			Wizard.lightningStrikeProto.stat = SpellProto.UnitStat.STRENGTH;
			Wizard.lightningStrikeProto.function = SpellProto.SpellFunctionType.ATTACK;
			Wizard.lightningStrikeProto.targetType = SpellProto.SpellTargetType.TARGETTED;
			Wizard.lightningStrikeProto.targetted = true;
			Wizard.lightningStrikeProto.strength = 1.8f;
			Wizard.lightningStrikeProto.manaCost = 10;
			Wizard.lightningStrikeProto.force = 1.5f;
			Wizard.lightningStrikeProto.particleDuration = 0.3f;
			Wizard.lightningStrikeProto.numberParticles = 1;
			Wizard.lightningStrikeProto.size = 1;
			Wizard.lightningStrikeProto.castTime = .3f;
			Wizard.lightningStrikeProto.coolDown = 4;
			Wizard.lightningStrikeProto.range = 5;
			Wizard.lightningStrikeProto.particleType = SpellProto.SpellParticleType.LIGHTNING;
			#endregion
			#region Ice Armor
			Wizard.iceArmorProto.name = "Ice Armor";
			Wizard.iceArmorProto.stat = SpellProto.UnitStat.DEFENSE;
			Wizard.iceArmorProto.function = SpellProto.SpellFunctionType.BUFF;
			Wizard.iceArmorProto.targetType = SpellProto.SpellTargetType.SELF;
			Wizard.iceArmorProto.targetted = true;
			Wizard.iceArmorProto.strength = 40;
			Wizard.iceArmorProto.manaCost = 30;
			Wizard.iceArmorProto.deliveryDuration = 10;
			Wizard.iceArmorProto.particleDuration = .1f;
			Wizard.iceArmorProto.size = .1f;
			Wizard.iceArmorProto.numberParticles = 1;
			Wizard.iceArmorProto.castTime = .5f;
			Wizard.iceArmorProto.coolDown = 20;
			#endregion
			#region Propulsion
			Wizard.propulsionProto.name = "Propulsion";
			Wizard.propulsionProto.stat = SpellProto.UnitStat.STRENGTH;
			Wizard.propulsionProto.function = SpellProto.SpellFunctionType.ATTACK;
			Wizard.propulsionProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Wizard.propulsionProto.targetted = false;
			Wizard.propulsionProto.directionType = SpellProto.SpellDirectionType.STRAIGHT;
			Wizard.propulsionProto.strength = 1.6f;
			Wizard.propulsionProto.manaCost = 50;
			Wizard.propulsionProto.force = 4;
			Wizard.propulsionProto.deliveryDuration = .5f;
			Wizard.propulsionProto.deliverySpeed = 15;
			Wizard.propulsionProto.particleDuration = .3f;
			Wizard.propulsionProto.numberParticles = 8;
			Wizard.propulsionProto.size = 1;
			Wizard.propulsionProto.castTime = .2f;
			Wizard.propulsionProto.coolDown = 10;
			Wizard.propulsionProto.retargetTime = 1f;
			Wizard.propulsionProto.particleType = SpellProto.SpellParticleType.STARFALL;
			#endregion
			#region Ball Lightning
			Wizard.ballLightningProto.name = "Ball Lightning";
			Wizard.ballLightningProto.stat = SpellProto.UnitStat.STRENGTH;
			Wizard.ballLightningProto.function = SpellProto.SpellFunctionType.ATTACK;
			Wizard.ballLightningProto.targetted = false;
			Wizard.ballLightningProto.targetType = SpellProto.SpellTargetType.PERSONAL;
			Wizard.ballLightningProto.directionType = SpellProto.SpellDirectionType.SCATTERED;
			Wizard.ballLightningProto.strength = 1.2f;
			Wizard.ballLightningProto.manaCost = 30;
			Wizard.ballLightningProto.force = 1;
			Wizard.ballLightningProto.deliveryDuration = 1;
			Wizard.ballLightningProto.deliverySpeed = 10;
			Wizard.ballLightningProto.particleDuration = .2f;
			Wizard.ballLightningProto.particleSpeed = 8;
			Wizard.ballLightningProto.numberParticles = 20;
			Wizard.ballLightningProto.size = 1;
			Wizard.ballLightningProto.castTime = .3f;
			Wizard.ballLightningProto.coolDown = 8;
			Wizard.ballLightningProto.hitsPerParticle = 1;
			#endregion
			
			#endregion
			
			defined = true;
		}
	}
    
	public static class Warrior
	{
		public static SpellProto powerAttackProto = new SpellProto();
		public static SpellProto ironWillProto = new SpellProto();
		public static SpellProto cleaveProto = new SpellProto();
	}
	
	public static class Archer
	{
		public static SpellProto powerShotProto = new SpellProto();
		public static SpellProto marksmanProto = new SpellProto();
		public static SpellProto fanShotProto = new SpellProto();
		public static SpellProto arrowRainProto = new SpellProto();
	}
	
    public static class Wizard
    {
		public static SpellProto lightningStrikeProto = new SpellProto();
		public static SpellProto iceArmorProto = new SpellProto();
		public static SpellProto propulsionProto = new SpellProto();
		public static SpellProto ballLightningProto = new SpellProto();
    }
	
}
