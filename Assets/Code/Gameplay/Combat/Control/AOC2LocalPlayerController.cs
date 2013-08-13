using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.lvl6.aoc2.proto;

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
	
	public Transform moveCursor;
	
	const int QUEUE_MAX = 5;
	
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
		AOC2EventManager.Combat.OnPlayerHealthChange(unit);
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
		AOC2EventManager.Combat.SetPlayerAttack += SetPlayerAttack;
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
		AOC2EventManager.Combat.SetPlayerAttack -= SetPlayerAttack;
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
			AOC2EventManager.Combat.OnPlayerHealthChange(unit);
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
		player.UseAbility(index);
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
			
		//player.UseAbility(0); //Index 0 should be travel ability
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
		AOC2Unit enemyTarget = TryTargetEnemy(data.pos);
		if (enemyTarget != null)
		{
			player.TargetEnemy(enemyTarget);
		}
		else //if (HitGround(data.pos))
		{
			SetGroundTarget(data.pos);
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
		Vector3 movePoint = AOC2ManagerReferences.gridManager.ScreenToGround(screenPos);
		player.TargetGround (AOC2ManagerReferences.gridManager.ScreenToPoint(screenPos));
		moveCursor.position = movePoint;
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
		if (player.attackTarget == null)
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
			player.attackTarget = null;
			AOC2ManagerReferences.combatManager.TargetNone();
		}
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
