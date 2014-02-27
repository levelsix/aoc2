using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto;

/// <summary>
/// @author Rob Giusti
/// Unit logic for the player controller.
/// Ties together character logic and touch controls
/// </summary>
[RequireComponent (typeof(AOC2Unit))]
public class AOC2LocalPlayerController : MonoBehaviour {
	
	/// <summary>
	/// The targetter collider, which moves around on the drag event
	/// </summary>
	public AOC2Targetter targetter;
	
	/// <summary>
	/// The unit component
	/// </summary>
	public AOC2Unit unit;
	
	/// <summary>
	/// The player component.
	/// </summary>
	public AOC2Player player;
	
	private int queuedAbilityIndex = -1;
	
	private bool queuedAbility{
		get
		{
			return queuedAbilityIndex >= 0;
		}
	}
	
	public Transform moveCursor;
	
	const int QUEUE_MAX = 16;
	
	[SerializeField]
	List<AOC2Unit> targetQueue = new List<AOC2Unit>(QUEUE_MAX);
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () 
	{
		unit = GetComponent<AOC2Unit>();
		player = GetComponent<AOC2Player>();
	}
	
	/// <summary>
	/// Start this instance.
	/// Set up logic
	/// </summary>
	protected void Start ()
	{
		AOC2EventManager.Combat.OnPlayerHealthChange(unit, 0);
		AOC2EventManager.Combat.OnPlayerManaChange(unit);
		targetter.localPlayerController = this;
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Sets up event delegates
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Controls.OnTap[0] += OnTap;
		AOC2EventManager.Controls.OnDoubleTap[0] += OnDoubleTap;
		AOC2EventManager.Controls.OnStartHold[0] += OnStartHoldOne;
		AOC2EventManager.Combat.SetPlayerAbility += SetPlayerAttack;
		AOC2EventManager.Combat.UseQueuedPlayerAbility += UseQueuedAbility;
		AOC2EventManager.Combat.OnEnemyDeath += OnEnemyDeath;
		unit.OnDamage += OnDamage;
		//unit.OnAnimationEnd += OnAnimationEnd;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Removes event delegates
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Controls.OnTap[0] -= OnTap;
		AOC2EventManager.Controls.OnDoubleTap[0] -= OnDoubleTap;
		AOC2EventManager.Controls.OnStartHold[0] -= OnStartHoldOne;
		AOC2EventManager.Combat.SetPlayerAbility -= SetPlayerAttack;
		AOC2EventManager.Combat.UseQueuedPlayerAbility -= UseQueuedAbility;
		AOC2EventManager.Combat.OnEnemyDeath -= OnEnemyDeath;
		unit.OnDamage -= OnDamage;
		//unit.OnAnimationEnd -= OnAnimationEnd;
	}
	
	/// <summary>
	/// Raises the damage event.
	/// </summary>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	void OnDamage(int amount)
	{
		if (AOC2EventManager.Combat.OnPlayerHealthChange != null)
		{
			AOC2EventManager.Combat.OnPlayerHealthChange(unit, -amount);
		}
		
		if (AOC2EventManager.NetCombat.OnLocalPlayerTakeDamage != null)
		{
			AOC2EventManager.NetCombat.OnLocalPlayerTakeDamage(unit, amount);
		}
	}
	
	/// <summary>
	/// Sets the player attack.
	/// </summary>
	/// <param name='index'>
	/// Attack Index.
	/// </param>
	void SetPlayerAttack(int index)
	{
		//player.UseAbility(index);
		queuedAbilityIndex = index;
	}
	
	void UseQueuedAbility()
	{
		UseQueuedAbility(false);
	}
	
	void UseQueuedAbility(bool quick)
	{
		if (queuedAbility)
		{
			player.UseAbility(queuedAbilityIndex, quick);
			queuedAbilityIndex = -1;
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
		player.unit.targetPos = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos, true));
		
		player.UseTravelAbility();
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
		if (queuedAbility)
		{
			SetGroundTarget(data.pos);
			UseQueuedAbility(true);
		}
		else
		{
			AOC2Unit enemyTarget = TryTargetEnemy(data.pos);
			if (enemyTarget != null)
			{
				player.TargetEnemy(enemyTarget);
			}
			else
			{
				SetGroundTarget(data.pos);
			}
		}
	}
	
	/// <summary>
	/// Sets the ground target for the player
	/// </summary>
	/// <param name='screenPos'>
	/// Screen position.
	/// </param>
	public void SetGroundTarget(Vector3 screenPos)
	{
		ClearTargets();
		
		Vector3 movePoint = AOC2ManagerReferences.gridManager.ScreenToGround(screenPos);
		player.TargetGround (AOC2ManagerReferences.gridManager.ScreenToPoint(screenPos));
		moveCursor.gameObject.SetActive(true);
		moveCursor.position = movePoint;
	}
	
	void ClearTargets()
	{
		player.unit.targetUnit = null;
		targetQueue.Clear();
		AOC2ManagerReferences.combatManager.TargetNone();
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
	bool HitGround(Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
		{
			return hit.collider.GetComponent<AOC2Ground>() != null;
		}
		return false;
	}
	
	public void EnqueueTarget(AOC2Unit target)
	{
		moveCursor.gameObject.SetActive(false);
		if (player.unit.targetUnit == null)
		{
			player.TargetEnemy(target);
		}
		else if (targetQueue.Count < QUEUE_MAX)
		{
			targetQueue.Add(target);
		}
	}
	
	public void TargetNextEnemy()
	{
		if (targetQueue.Count > 0)
		{
			player.TargetEnemy(targetQueue[0]);
			targetQueue.RemoveAt(0);
		}
		else
		{
			ClearTargets();
			unit.targetPos = new AOC2Position(unit.aPos.position);
		}
	}
	
	public void OnStartHoldOne(AOC2TouchData data)
	{
		SetGroundTarget(data.pos);
		SetPlayerAttack(0);
	}
	
	public void OnStartHoldTwo(AOC2TouchData data)
	{
		
	}
	
	public void OnAnimationEnd()
	{
		if (targetQueue.Count > 0)
		{
			player.TargetEnemy(targetQueue[0]);
			targetQueue.RemoveAt(0);
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
		int mask = 1 << AOC2Values.Layers.TOUCH_ENEMY;
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane - Camera.main.nearClipPlane, mask))
		{
			return hit.collider.GetComponent<AOC2ClickBox>().parent;
		}
		return null;
	}
	
	void OnEnemyDeath(AOC2Unit enemy)
	{
		while(targetQueue.Contains(enemy))
		{
			targetQueue.Remove(enemy);	
		}
	}
}
