using UnityEngine;
using System.Collections;

/// <summary>
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
	/// DEBUG: the player character's ranged attack.
	/// </summary>
	public AOC2Attack baseAttack;
	
	/// <summary>
	/// DEBUG: The melee attack.
	/// </summary>
	public AOC2Attack meleeAttack;
	
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
	private AOC2LogicState blinkLogic;
	
	/// <summary>
	/// The base attack logic.
	/// </summary>
	private AOC2LogicState baseAttackLogic;
	
	/// <summary>
	/// The melee attack logic.
	/// </summary>
	private AOC2LogicState meleeAttackLogic;
	
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
	}
	
	/// <summary>
	/// Start this instance.
	/// Set up logic
	/// </summary>
	protected override void Start ()
	{
		AOC2LogicState doNothing = new AOC2LogicDoNothing();
		
		moveLogic = new AOC2LogicChargeAtTarget(_unit);
		moveLogic.AddExit(new AOC2ExitTargetInRange(doNothing, _unit, MIN_MOVE_DIST));
		
		blinkLogic = new AOC2LogicBlinkToTarget(_unit, BLINK_DELAY, BLINK_AFTER_DELAY);
		blinkLogic.AddExit(new AOC2ExitTargetInRange(doNothing, _unit, MIN_MOVE_DIST));
		
		AOC2LogicState chaseState = new AOC2LogicChargeAtTarget(_unit);
		AOC2LogicState attackState = new AOC2LogicUseAttack(_unit, baseAttack, false);
		
		chaseState.AddExit(new AOC2ExitTargetInRange(attackState, _unit, baseAttack.range));
		attackState.AddExit(new AOC2ExitAttackOnCooldown(baseAttack, doNothing));
		
		baseAttackLogic = chaseState;
		
		AOC2LogicState meleeChase = new AOC2LogicChargeAtTarget(_unit);
		AOC2LogicState meleeState = new AOC2LogicUseAttack(_unit, meleeAttack, false);
		
		meleeChase.AddExit(new AOC2ExitTargetInRange(meleeState, _unit, meleeAttack.range));
		meleeState.AddExit(new AOC2ExitAttackOnCooldown(meleeAttack, doNothing));
		
		meleeAttackLogic = meleeChase;
		
		_baseState = doNothing;
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Sets up event delegates
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Controls.OnTap += OnTap;
		AOC2EventManager.Controls.OnDoubleTap += OnDoubleTap;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Removes event delegates
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Controls.OnTap -= OnTap;
		AOC2EventManager.Controls.OnDoubleTap -= OnDoubleTap;
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
			if (baseAttack.onCool)
			{
				return;
			}
			_unit.targetPos = enemyTarget.aPos;
			_current = meleeAttackLogic;
		}
		else
		{
			_unit.targetPos = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos));
			_current = blinkLogic;
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
			if (baseAttack.onCool)
			{
				return;
			}
			_unit.targetPos = enemyTarget.aPos;
			_current = baseAttackLogic;
		}
		else
		{
			_unit.targetPos = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos));
			_current = moveLogic;
		}
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
		LayerMask mask = (int)Mathf.Pow(2,AOC2Values.Layers.TOUCH_ENEMY);
        if (Physics.Raycast(ray, out hit, mask))
		{
			return hit.collider.GetComponent<AOC2ClickBox>().parent;
		}
		return null;
	}
}
