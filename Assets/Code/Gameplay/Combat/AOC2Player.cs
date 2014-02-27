using UnityEngine;
using System.Collections;
using proto;

/// <summary>
/// @author Rob Giusti
/// Unit logic for the player
/// Operates character logic, takes input from either
/// a LocalPlayerController or a NetworkPlayerController
/// </summary>
[RequireComponent (typeof(AOC2Unit))]
public class AOC2Player : AOC2UnitLogic {
	
	/// <summary>
	/// The distance the player will move upon using the
	/// travel ability
	/// </summary>
	public float rollDist = 3;
	
	/// <summary>
	/// The speed of the travel ability
	/// </summary>
	public float rollSpeed = 12;
	
	/// <summary>
	/// The minimum move distance.
	/// If the character is within this distance from its target,
	/// it should not move.
	/// </summary>
	public float MIN_MOVE_DIST = .2f;
	
	/// <summary>
	/// DEBUG
	/// The color of the target.
	/// </summary>
	public Color targetColor = Color.magenta;
	
	/// <summary>
	/// The index of the current attack.
	/// </summary>
	public int attackIndex = -1;
	
	/// <summary>
	/// The ability logics.
	/// </summary>
	private AOC2LogicState[] abilityLogics;
    
	/// <summary>
	/// The basic attack logic.
	/// </summary>
    private AOC2LogicState basicAttackMoveLogic;
	
	/// <summary>
	/// The abilities of this player
	/// </summary>
	public AOC2Ability[] abilities;
	
	/// <summary>
	/// The unit component
	/// </summary>
	public AOC2Unit unit;
	
	/// <summary>
	/// The local player controller component.
	/// </summary>
	public AOC2LocalPlayerController local;
	
	/// <summary>
	/// The move logic.
	/// </summary>
	private AOC2LogicState moveLogic;
	
	/// <summary>
	/// The blink logic.
	/// </summary>
	private AOC2LogicState sprintLogic;
	
	/// <summary>
	/// Awake this instance.
	/// Set up internal components.
	/// DEBUG: Set class abilities and stats
	/// TODO: Load this data from server and initialize from protos
	/// </summary>
	void Awake () 
	{
		unit = GetComponent<AOC2Unit>();
		local = GetComponent<AOC2LocalPlayerController>();
		abilities = new AOC2Ability[4];
		
	}
	
	/// <summary>
	/// Start this instance by initializing the Unit component
	/// </summary>
	void Start()
	{
		unit.Activate();
		SetUpClassFeatures ();
		unit.Init();
	}

	void SetUpClassFeatures ()
	{
		switch(AOC2Whiteboard.playerClass)
		{
		case ClassType.WIZARD:
			abilities[0] = new AOC2Ability(11);
			abilities[1] = new AOC2Ability(10);
			abilities[2] = new AOC2Ability(9);
			abilities[3] = new AOC2Ability(12);
			unit.stats.strength = 120;
			unit.stats.defense = 120;
			unit.stats.attackSpeed = 200;
			unit.stats.maxMana = 500;
			unit.stats.maxHealth = 1500;
			unit.ranged = true;
			break;
		case ClassType.WARRIOR:
			abilities[0] = new AOC2Ability(AOC2AbilityLists.Warrior.cleaveProto);
			abilities[1] = new AOC2Ability(AOC2AbilityLists.Warrior.ironWillProto);
			abilities[2] = new AOC2Ability(AOC2AbilityLists.Warrior.powerAttackProto);
			abilities[3] = new AOC2Ability(AOC2AbilityLists.Warrior.powerAttackProto);
			unit.stats.strength = 100;
			unit.stats.defense = 130;
			unit.stats.attackSpeed = 200;
			unit.stats.maxMana = 250;
			unit.stats.maxHealth = 2000;
			unit.ranged = false;
			unit.basicAttackAbility.range = 5;
			break;
		case ClassType.ARCHER:
			abilities[0] = new AOC2Ability(AOC2AbilityLists.Archer.fanShotProto);
			abilities[1] = new AOC2Ability(AOC2AbilityLists.Archer.marksmanProto);
			abilities[2] = new AOC2Ability(AOC2AbilityLists.Archer.powerShotProto);
			abilities[3] = new AOC2Ability(AOC2AbilityLists.Archer.arrowRainProto);
			unit.stats.strength = 140;
			unit.stats.defense = 120;
			unit.stats.attackSpeed = 250;
			unit.stats.maxMana = 300;
			unit.stats.maxHealth = 1500;
			unit.ranged = true;
			break;
		}
	}
	
	/// <summary>
	/// Start this instance.
	/// Sets up logic state machine.
	/// </summary>
	public override void Init ()
	{
		//Set up all logic states first, so that all pointers in exit states are valid
		AOC2LogicState doNothing = new AOC2LogicDoNothing(unit);
		moveLogic = new AOC2LogicNavigateTowardTarget(unit);
		
		AOC2LogicState basicAttackLogic = new AOC2LogicHighStateAbility(unit, unit.basicAttackAbility);
		
		sprintLogic = new AOC2LogicCombatRoll(unit, rollDist, .3f, rollSpeed);
		abilityLogics = new AOC2LogicState[abilities.Length];
		
        AOC2ExitLogicState noTarget = new AOC2ExitNotOther(new AOC2ExitPlayerHasTarget(unit, null), doNothing);
        
        doNothing.AddExit(new AOC2ExitPlayerHasTarget(unit, basicAttackLogic));
		doNothing.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(moveLogic, unit, MIN_MOVE_DIST), moveLogic));
        
		basicAttackLogic.AddExit(noTarget);
		basicAttackLogic.AddExit(new AOC2ExitWhenComplete(basicAttackLogic, basicAttackLogic));
		
		moveLogic.AddExit(new AOC2ExitWhenComplete(moveLogic, doNothing));
        moveLogic.AddExit(new AOC2ExitPlayerHasTarget(unit, basicAttackLogic));
		
		//sprintLogic = new AOC2LogicSprint(unit);
		//sprintLogic.AddExit(new AOC2ExitTargetInRange(doNothing, unit, MIN_MOVE_DIST));
		
		sprintLogic.AddExit(new AOC2ExitWhenComplete(sprintLogic, doNothing));
		sprintLogic.priority = 2;
		
		//Set up the logic for each ability
		for (int i = 0; i < abilityLogics.Length; i++) {
			abilityLogics[i] = new AOC2LogicHighStateAbility(unit, abilities[i]);
			abilityLogics[i].AddExit(new AOC2ExitWhenComplete(abilityLogics[i], doNothing));
			abilityLogics[i].priority = 1;
		}
		
		logic = new AOC2HFSMLogic(doNothing, unit);
		
		base.Init();
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Sets up event delegates
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Removes event delegates
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
	}
	
	/// <summary>
	/// Uses the specified slotted ability.
	/// </summary>
	/// <param name='index'>
	/// Ability Index.
	/// </param>
	public void UseAbility(int index, bool quick = false)
	{
		//Short here if we don't actually have the mana for the ability
		if (unit.mana < abilities[index].manaCost || abilities[index].onCool)
		{
			return;
		}
		
		if (abilities[index].spellProto.targetType == SpellProto.SpellTargetType.SELF)
		{
			unit.targetPos = unit.aPos;
			unit.targetUnit = unit;
		}
		else if (unit.targetUnit != null)
		{
			unit.targetPos = unit.targetUnit.aPos;
		}
		else if (!quick) //If no target, target the closest enemy
		{
			AOC2Unit closeEn = AOC2ManagerReferences.combatManager.GetClosestEnemy(unit);
			if (closeEn != null)
			{
				TargetEnemy(closeEn);
				unit.targetPos = closeEn.aPos;
			}
		}
		
		SetLogic(abilityLogics[index]);
	}

	/// <summary>
	/// Whenever an enemy dies, check to see if it was our current target,
	/// in which case we need to null out the target
	/// </summary>
	/// <param name='enemy'>
	/// The enemy that died
	/// </param>
	void OnEnemyDeath(AOC2Unit enemy)
	{
		if (enemy == unit.targetUnit)
		{
			if (local != null)
			{
				local.TargetNextEnemy();
			}
			else
			{
				unit.targetUnit = null;
				AOC2ManagerReferences.combatManager.TargetNone();
			}
		}
	}
	
	/// <summary>
	/// Sets a specific enemy to be this player's target.
	/// Also, sets the player to use their auto-attack on the
	/// target
	/// </summary>
	/// <param name='enemy'>
	/// Unit that was just targetted
	/// </param>
	public void TargetEnemy(AOC2Unit enemy)
	{
		AOC2ManagerReferences.combatManager.TargetUnit(enemy);
		
		unit.targetUnit = enemy;
	}
	
	/// <summary>
	/// Sets the target position to a grid point on the ground
	/// </summary>
	/// <param name='pos'>
	/// Grid position to set the target to
	/// </param>
	public void TargetGround(AOC2GridNode pos)
	{
		unit.targetPos = new AOC2Position(pos);
	}
	
	/// <summary>
	/// Uses the travel ability.
	/// TODO: Make logics for the other travel abilities
	/// </summary>
	public void UseTravelAbility()
	{
		SetLogic(sprintLogic);
	}
}