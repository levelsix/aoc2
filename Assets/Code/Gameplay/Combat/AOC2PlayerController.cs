using UnityEngine;
using System.Collections;

/// <summary>
/// Unit logic for the player controller.
/// Ties together character logic and touch controls
/// </summary>
[RequireComponent (typeof(AOC2Unit))]
public class AOC2PlayerController : AOC2UnitLogic {
	
	public float MIN_MOVE_DIST = .2f;
	
	public AOC2Attack baseAttack;
	
	private AOC2Position target;
	
	AOC2Unit _unit;
	
	private AOC2LogicState moveLogic;
	
	private AOC2LogicState baseAttackLogic;
	
	// Use this for initialization
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
		
		baseAttackLogic = new AOC2LogicChargeAtTarget(_unit);
		
		AOC2LogicState chaseState = new AOC2LogicChargeAtTarget(_unit);
		AOC2LogicState attackState = new AOC2LogicUseAttack(_unit, baseAttack, false);
		
		attackState.AddExit(new AOC2ExitAttackOnCooldown(baseAttack, doNothing));
		baseAttackLogic.AddExit(new AOC2ExitTargetInRange(attackState, _unit, baseAttack.range));
		
		_baseState = doNothing;
	}
	
	void OnEnable()
	{
		AOC2EventManager.Controls.OnTap += OnTap;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Controls.OnTap -= OnTap;
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
		LayerMask mask = (int)Mathf.Pow(2,AOC2Values.Layers.ENEMY);
        if (Physics.Raycast(ray, out hit, mask))
		{
			return hit.collider.GetComponent<AOC2Unit>();
		}
		return null;
	}
}
