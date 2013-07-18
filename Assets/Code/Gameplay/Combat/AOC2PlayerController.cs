using UnityEngine;
using System.Collections;
using com.lvl6.proto;

/// <summary>
/// @author Rob Giusti
/// Unit logic for the player controller.
/// Ties together character logic and touch controls
/// </summary>
[RequireComponent (typeof(AOC2Unit))]
public class AOC2PlayerController : AOC2UnitLogic {
	
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
	/// The abilities of this player
	/// </summary>
	public AOC2Ability[] abilities;
	
	/// <summary>
	/// The attack target.
	/// </summary>
	public AOC2Unit attackTarget;
	
	/// <summary>
	/// The unit component
	/// </summary>
	AOC2Unit _unit;
	
	/// <summary>
	/// The move logic.
	/// </summary>
	private AOC2LogicState moveLogic;
	
	/// <summary>
	/// The blink logic.
	/// </summary>
	private AOC2LogicState sprintLogic;
	
	/*
	/// <summary>
	/// The base attack logic.
	/// </summary>
	private AOC2LogicState baseAttackLogic;
	
	/// <summary>
	/// The melee attack logic.
	/// </summary>
	private AOC2LogicState meleeAttackLogic;
	*/
	
	/// <summary>
	/// The delay before a blink begins; the casting time
	/// </summary>
	public float BLINK_DELAY = .5f;
	
	/// <summary>
	/// The delay after a blink ends; the vulnerability period
	/// </summary>
	public float BLINK_AFTER_DELAY = .5F;
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () 
	{
		_unit = GetComponent<AOC2Unit>();
		abilities = new AOC2Ability[4];
		abilities[0] = AOC2AbilityLists.Archer.baseAttackAbility;
		abilities[1] = AOC2AbilityLists.Warrior.powerAttackAbility;
		abilities[2] = AOC2AbilityLists.Archer.fanAttackAbility;
		abilities[3] = AOC2AbilityLists.Warrior.cleaveAbility;
	}
	
	/// <summary>
	/// Start this instance.
	/// Set up logic
	/// </summary>
	protected override void Start ()
	{
		AOC2LogicState doNothing = new AOC2LogicDoNothing();
		
		moveLogic = new AOC2LogicFollowPath(_unit);
		moveLogic.AddExit(new AOC2ExitWhenComplete(moveLogic, doNothing));
		
		sprintLogic = new AOC2LogicSprint(_unit);
		sprintLogic.AddExit(new AOC2ExitTargetInRange(doNothing, _unit, MIN_MOVE_DIST));
		
		abilityLogics = new AOC2LogicState[abilities.Length];
		
		AOC2LogicState abilityState;
		
		//Set up the logic for each ability
		for (int i = 0; i < abilityLogics.Length; i++) {
			abilityLogics[i] = new AOC2LogicMoveTowardTarget(_unit);
			abilityState = new AOC2LogicUseAbility(_unit, abilities[i], false);
			abilityState.AddExit(new AOC2ExitWhenComplete(abilityState, doNothing));
			abilityLogics[i].AddExit(new AOC2ExitTargetInRange(abilityState, _unit, abilities[i].range));
		}
		
		_baseState = doNothing;
		
		AOC2EventManager.Combat.OnPlayerHealthChange(_unit);
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Sets up event delegates
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Controls.OnTap[0] += OnTap;
		AOC2EventManager.Controls.OnDoubleTap[0] += OnDoubleTap;
		AOC2EventManager.Combat.SetPlayerAttack += SetPlayerAttack;
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
		_unit.OnDamage += OnDamage;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Removes event delegates
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Controls.OnTap[0] -= OnTap;
		AOC2EventManager.Controls.OnDoubleTap[0] -= OnDoubleTap;
		AOC2EventManager.Combat.SetPlayerAttack -= SetPlayerAttack;
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
		_unit.OnDamage -= OnDamage;
	}
	
	void OnDamage(int amount)
	{
		AOC2EventManager.Combat.OnPlayerHealthChange(_unit);
	}
	
	/// <summary>
	/// Sets the player attack.
	/// </summary>
	/// <param name='index'>
	/// Attack Index.
	/// </param>
	void SetPlayerAttack(int index)
	{
		if (abilities[index].targetType == AOC2Values.Abilities.TargetType.SELF)
		{
			_unit.targetPos = _unit.aPos;
			
			abilityLogics[index].Start();
			_current = abilityLogics[index];
		}
		else if (attackTarget != null)
		{
			_unit.targetPos = attackTarget.aPos;
			
			abilityLogics[index].Start();
			_current = abilityLogics[index];
		}
		else if (A)
		{
			AOC2Unit closeEn = AOC2ManagerReferences.combatManager.GetClosestEnemy(_unit);
			if (closeEn != null)
			{
				_unit.targetPos = closeEn.aPos;
			}
			
			abilityLogics[index].Start();
			_current = abilityLogics[index];
		}
	}
	
	/// <summary>
	/// DEBUG: Determines whether a double-tap should start a blink or melee attack
	/// TODO: Make a more abstractable movement system and less-hardcoded melee attack
	/// </summary>
	/// <param name='data'>
	/// Touch data.
	/// </param>
	void OnDoubleTap(AOC2TouchData data)
	{
		AOC2Unit enemyTarget = TryTargetEnemy(data.pos);
		if (enemyTarget != null)
		{
			
		}
		else if(TargetGround(data.pos))
		{
			_unit.targetPos = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos));
			
			sprintLogic.Start();
			_current = sprintLogic;
		}
	}
	
	/// <summary>
	/// Raises the tap event.
	/// Attempts to attack an enemy or move
	/// </summary>
	/// <param name='data'>
	/// Touch data.
	/// </param>
	void OnTap(AOC2TouchData data)
	{
		AOC2Unit enemyTarget = TryTargetEnemy(data.pos);
		if (enemyTarget != null)
		{
			TargetEnemy(enemyTarget);
		}
		else if (TargetGround(data.pos))
		{
			_unit.targetPos = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos));
			moveLogic.Start();
			_current = moveLogic;
		}
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
			unit.DebugTint(unit.tint);
			attackTarget = null;
		}
	}
	
	/// <summary>
	/// DEBUG: Tints the enemy that was selected
	/// TODO: Have some UI or other visual element
	/// that identifies the targetted enemy
	/// </summary>
	/// <param name='unit'>
	/// Unit that was just targetted
	/// </param>
	void TargetEnemy(AOC2Unit unit)
	{
		if (attackTarget != null)
		{
			attackTarget.DebugTint(attackTarget.tint);
		}
		attackTarget = unit;
		attackTarget.DebugTint(targetColor);
	}
	
	/// <summary>
	/// Targets the ground from a click/tap
	/// </summary>
	/// <returns>
	/// Whether the ground was hit
	/// </returns>
	/// <param name='screenPos'>
	/// The clicked/tapped screen position.
	/// </param>
	bool TargetGround(Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
		{
			return hit.collider.GetComponent<AOC2Ground>() != null;
		}
		return false;
	}
	
	/// <summary>
	/// Tries to target an enemy using a raycast.
	/// </summary>
	/// <returns>
	/// The target enemy.
	/// Null if no enemy.
	/// </returns>
	/// <param name='screenPos'>
	/// Screen position.
	/// </param>
	AOC2Unit TryTargetEnemy(Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit hit;
		int mask = 1 << AOC2Values.Layers.TOUCH_ENEMY;
        if (Physics.Raycast(ray, out hit, Camera.main.far - Camera.main.near, mask))
		{
			return hit.collider.GetComponent<AOC2ClickBox>().parent;
		}
		return null;
	}
}
