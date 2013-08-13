using UnityEngine;
using System.Collections;
using com.lvl6.aoc2.proto;

/// <summary>
/// @author Rob Giusti
/// Unit logic for the player
/// Operates character logic, takes input from either
/// a LocalPlayerController or a NetworkPlayerController
/// </summary>
[RequireComponent (typeof(AOC2Unit))]
public class AOC2Player : AOC2UnitLogic {
	
	public float rollDist = 3;
	public float rollSpeed = 12;
	
	/// <summary>
	/// The minimum move distance.
	/// If the character is within this distance from its target,
	/// it should not move.
	/// </summary>
	public float MIN_MOVE_DIST = .2f;
	
	/// <summary>
	/// Constant for AB testing.
	/// Flip to change test.
	/// </summary>
	public bool A = true;
	
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
	/// The attack target.
	/// </summary>
	public AOC2Unit attackTarget
	{
		get
		{
			return unit.targetUnit;
		}
		set
		{
			unit.targetUnit = value;
		}
	}
	
	/// <summary>
	/// The unit component
	/// </summary>
	public AOC2Unit unit;
	
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
	/// </summary>
	void Awake () 
	{
		unit = GetComponent<AOC2Unit>();
		local = GetComponent<AOC2LocalPlayerController>();
		abilities = new AOC2Ability[3];
		abilities[0] = new AOC2AttackAbility(AOC2AbilityLists.Warrior.cleaveAbility);
		abilities[1] = new AOC2BuffAbility(AOC2AbilityLists.Warrior.ironWillAbility);
		abilities[2] = new AOC2AttackAbility(AOC2AbilityLists.Warrior.powerAttackAbility);
	}
	
	void Start()
	{
		unit.Init();
		unit.Activate();
	}
	
	/// <summary>
	/// Start this instance.
	/// Set up logic
	/// </summary>
	public override void Init ()
	{
		AOC2LogicState doNothing = new AOC2LogicDoNothing(unit);
        
        AOC2LogicState useBaseAttack = new AOC2LogicUseAbility(unit, unit.basicAttackAbility);
        basicAttackMoveLogic = new AOC2LogicNavigateTowardTarget(unit);
        
        AOC2ExitLogicState noTarget = new AOC2ExitNotOther(new AOC2ExitPlayerHasTarget(this, null), doNothing);
        
        doNothing.AddExit(new AOC2ExitPlayerHasTarget(this, basicAttackMoveLogic));
        
        basicAttackMoveLogic.AddExit(new AOC2ExitTargetInRange(useBaseAttack, unit, unit.basicAttackAbility.range));
        basicAttackMoveLogic.AddExit(noTarget);
        
        useBaseAttack.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, unit, unit.basicAttackAbility.range), basicAttackMoveLogic));
        useBaseAttack.AddExit(noTarget);
		useBaseAttack.AddExit(new AOC2ExitWhenComplete(useBaseAttack, useBaseAttack)); //Loop autoattack once animation hits
		
		moveLogic = new AOC2LogicNavigateTowardTarget(unit);
		moveLogic.AddExit(new AOC2ExitWhenComplete(moveLogic, doNothing));
		
		//sprintLogic = new AOC2LogicSprint(unit);
		//sprintLogic.AddExit(new AOC2ExitTargetInRange(doNothing, unit, MIN_MOVE_DIST));
		
		sprintLogic = new AOC2LogicCombatRoll(unit, rollDist, .3f, rollSpeed);
		sprintLogic.AddExit(new AOC2ExitWhenComplete(sprintLogic, doNothing));
		sprintLogic.priority = 2;
		
		abilityLogics = new AOC2LogicState[abilities.Length];
		
		//Set up the logic for each ability
		for (int i = 0; i < abilityLogics.Length; i++) {
			abilityLogics[i] = new AOC2LogicHighStateAbility(unit, abilities[i]);
			abilityLogics[i].AddExit(new AOC2ExitWhenComplete(abilityLogics[i], doNothing));
			abilityLogics[i].priority = 1;
		}
		
		logic = new AOC2HFSMLogic(basicAttackMoveLogic, unit);
		
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
	public void UseAbility(int index)
	{
		if (abilities[index].targetType == AOC2Values.Abilities.TargetType.SELF)
		{
			unit.targetPos = unit.aPos;
		}
		else if (attackTarget != null)
		{
			unit.targetPos = attackTarget.aPos;
		}
		else //If no target, target the closest enemy
		{
			AOC2Unit closeEn = AOC2ManagerReferences.combatManager.GetClosestEnemy(unit);
			if (closeEn != null)
			{
				unit.targetPos = closeEn.aPos;
			}
		}
		
		SetLogic(abilityLogics[index]);
	}

	/// <summary>
	/// Whenever an enemy dies, check to see if it was our current target,
	/// in which case we need to null out the target
	/// </summary>
	/// <param name='unit'>
	/// The enemy that died
	/// </param>
	void OnEnemyDeath(AOC2Unit unit)
	{
		if (unit == attackTarget)
		{
			if (local != null)
			{
				local.TargetNextEnemy();
			}
			else
			{
				attackTarget = null;
				AOC2ManagerReferences.combatManager.TargetNone();
			}
		}
	}
	
	/// <summary>
	/// Sets a specific enemy to be this player's target.
	/// Also, sets the player to use their auto-attack on the
	/// target
	/// </summary>
	/// <param name='unit'>
	/// Unit that was just targetted
	/// </param>
	public void TargetEnemy(AOC2Unit unit)
	{
		AOC2ManagerReferences.combatManager.TargetUnit(unit);
		
		attackTarget = unit;

		//unit.targetPos = attackTarget.aPos;

		//SetLogic(basicAttackMoveLogic);
	}
	
	public void TargetGround(AOC2GridNode pos)
	{
		unit.targetPos = new AOC2Position(pos);
		
		SetLogic(moveLogic);
	}
	
	public void UseTravelAbility()
	{
		SetLogic(sprintLogic);
	}
	
	void OnGizmoDraw()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(unit.targetPos.position, 1f);
	}
	
	public override void SetLogic (AOC2LogicState state)
	{
		//Debug.Log("Setting player logic to " + state.GetType().Name);
		base.SetLogic (state);
	}
}