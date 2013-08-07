using UnityEngine;
using System;
using System.Collections;
using com.lvl6.aoc2.proto;

/// <summary>
/// @author Rob Giusti
/// Base Unit class. All Units, Player and Enemy, use this component.
/// Ties together movement, spawning, dieing, pooling, and logic
/// </summary>
[RequireComponent (typeof (BoxCollider))]
public class AOC2Unit : AOC2Spawnable, AOC2Poolable {
	
	#region Members
	
	#region Public
	
	public string currentLogicState = "";

	/// <summary>
	/// The stats of this unit.
	/// </summary>
	public AOC2UnitStats stats;

	public NavMeshAgent nav;
	
	/// <summary>
	/// Gets the full power of this unit,
	/// for attacks.
	/// Strength + Weapon Power
	/// TODO: Add weapon defense
	/// </summary>
	public int power{
		get
		{
			return stats.strength;
		}
	}
	
	/// <summary>
	/// Gets the full defense of this unit
	/// Defense + Weapon Defense
	/// TODO: Add weapon defense
	/// </summary>
	public int defense{
		get
		{
			return stats.defense;
		}
	}
	
	/// <summary>
	/// The mass of this unit.
	/// Affects knockback from attacks.
	/// </summary>
	public float mass = 1;
	
	/// <summary>
	/// The sprint mod.
	/// </summary>
	public float sprintMod = 2;
	
	/// <summary>
	/// Whether or not this Unit is an enemy
	/// </summary>
	public bool isEnemy = true;
	
	/// <summary>
	/// DEBUG
	/// The tint.
	/// </summary>
	public Color tint = Color.white;
	
	/// <summary>
	/// The target position.
	/// </summary>
	public AOC2Position targetPos;
	
	/// <summary>
	/// The target unit.
	/// </summary>
	public AOC2Unit targetUnit;
	
	/// <summary>
	/// The Position of this unit.
	/// Transform wrapper that allows us to 
	/// </summary>
	public AOC2Position aPos;
	
	/// <summary>
	/// The can be knocked back flag
	/// </summary>
	public bool canBeKnockedBack = true;
	
	/// <summary>
	/// The can be hit flag
	/// </summary>
	public bool canBeHit = true;
    
    /// <summary>
    /// The basic attack ability.
    /// </summary>
    [HideInInspector]
    public AOC2Ability basicAttackAbility;
    
    public bool ranged;
	
	public bool _activated = false;
	
	public const float MIN_MOVE_DIST = .1f;
	
	#region Action Delegates
	
	/// <summary>
	/// The local event for taking damage.
	/// </summary>
	public Action<int> OnDamage;
	
	/// <summary>
	/// The local on death event for this unit
	/// </summary>
	public Action OnDeath;
	
	/// <summary>
	/// The event, triggered by the animation system, which an
	/// ability logic can tie into to properly time attacks
	/// </summary>
	public Action OnUseAbility;
	
	/// <summary>
	/// The event, triggered by the animation system, which AI
	/// logic can tie into to properly time itself
	/// </summary>
	public Action OnAnimationEnd;
	
	public Action OnPathfind;
	
	/// <summary>
	/// The activation event, used to turn on animations and AI logic
	/// for this unit
	/// </summary>
	public Action OnActivate;
	
	/// <summary>
	/// The deactivation event, used to turn off animations and AI logic
	/// for this unit
	/// </summary>
	public Action OnDeactivate;
	
	#endregion
	
	/// <summary>
	/// Gets or sets the prefab of this unit
	/// Only sets if the prefab is not set
	/// </summary>
	/// <value>
	/// The prefab.
	/// </value>
	public AOC2Poolable prefab{
		get
		{
			return _prefab;
		}
		set
		{
			//Nothing should be able to change this once its set
			if (_prefab == null)
			{
				_prefab = value;
			}
		}
	}
	
	public bool activated
	{
		get
		{
			return _activated;
		}
		set
		{
			_activated = value;
			nav.enabled = value;
			if (_activated)
			{
				if (OnActivate != null)
				{
					OnActivate();
				}
			}
			else if (OnDeactivate != null)
			{
				OnDeactivate();
			}
		}
	}
	
	public AOC2MoveAbility basicMove;
    
    public int id;
    
    private static int nextID = 0;
	
	#endregion
	
	#region Private
	
	/// <summary>
	/// The current health.
	/// </summary>
	public float health = 1;
	
	/// <summary>
	/// The current mana.
	/// </summary>
	public float mana = 1;
    
	/// <summary>
	/// The prefab.
	/// </summary>
	private AOC2Poolable _prefab;
	
	/// <summary>
	/// The logic for this unit.
	/// </summary>
	private AOC2UnitLogic _logic;
    
    private Transform _trans;
	
	public AOC2Model model;
	
	#endregion
	
	#region Constants
	
	const float BASE_DEFENSE_MOD = .25f;
	
	#endregion
	
	#endregion
	
	#region Functions
	
	#region Initialization
	
	/// <summary>
	/// Spawn an instance of this prefab using the pool manager
	/// </summary>
	/// <param name='origin'>
	/// Position to spawn at
	/// </param>
	override public void Spawn(Vector3 origin)
	{
		AOC2Unit unit = AOC2ManagerReferences.poolManager.Get(this, origin) as AOC2Unit;
		unit.Init();
	}
	
	/// <summary>
	/// Make an instance of this prefab using the pool manager
	/// </summary>
	/// <param name='origin'>
	/// Position to make at
	/// </param>
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2Unit unit = Instantiate(this, origin, Quaternion.identity) as AOC2Unit;
		unit.prefab = this;
        unit.id = nextID++;
		return unit;
	}
	
	/// <summary>
	/// Awake this instance.
	/// Set up position and logic
	/// </summary>
	void Awake()
	{
		_logic = GetComponent<AOC2UnitLogic>();
        _trans = transform;
		aPos = new AOC2Position(_trans);
        nav = GetComponent<NavMeshAgent>();
		if (model == null)
		{
			model = GetComponent<AOC2Model>();
		}
	}
	
	void Start()
	{
		nav.speed = stats.moveSpeed;
	}
	
	/// <summary>
	/// Debug: Init without proto
	/// Initialize this instance.
	/// Sets health, layer, and initializes the logic
	/// </summary>
	public void Init()
	{
        DebugTint(tint);
        
        if (ranged)
        {
            basicAttackAbility = new AOC2AttackAbility(AOC2AbilityLists.Generic.baseRangeAttackAbility);
        }
        else
        {
            basicAttackAbility = new AOC2AttackAbility(AOC2AbilityLists.Generic.baseMeleeAttackAbility);   
        }
        
		health = stats.maxHealth;
		
		mana = stats.maxMana;
		
		if (_logic != null)
		{
			_logic.Init();
		}
		
		if (isEnemy)
		{
			AOC2EventManager.Combat.OnSpawnEnemy(this);
			gameObject.layer = AOC2Values.Layers.ENEMY;
		}
		else
		{
			AOC2EventManager.Combat.OnSpawnPlayer(this);
			gameObject.layer = AOC2Values.Layers.PLAYER;
		}
		
		//_trans.Translate(0, _trans.localScale.y / 2, 0);
	}
	
	/// <summary>
	/// Debugs tint.
	/// </summary>
	/// <param name='color'>
	/// Color to tint
	/// </param>
	public void DebugTint(Color color)
	{
		if (model != null)
		{
			model.Tint(color);
		}
	}
	
	#endregion
	
	#region Death Logic
	
	/// <summary>
	/// Takes damage from a delivery
	/// </summary>
	/// <param name='deliv'>
	/// Delivery that has hit this unit
	/// </param>
	public void TakeDamage(AOC2Delivery deliv)
	{
		int damage = (int) (((deliv.damage) - (defense * BASE_DEFENSE_MOD)) * AOC2Math.ResistanceMod(stats.resistance));
		if (damage > 0){
			health -= damage;
			//Debug.Log("Damage: " + deliv.damage + ", Taken: " + damage);
			if (health <= 0)
			{
				Die();
			}
			else 
			{
				if (canBeKnockedBack)
				{
					Knockback(deliv);
				}
				if (OnDamage != null)
				{
					OnDamage(damage);
				}
			}
		}
		else
		{
			//Debug.Log("High defense, attack blocked!");
		}
	}
	
	public void Knockback(AOC2Delivery delivery)
	{
		//Debug.Log("Knockback");
		
		Vector3 knockbackDir = delivery.direction;
		if (knockbackDir == Vector3.zero)
		{
			knockbackDir = (aPos.position - delivery.transform.position).normalized;
		}
		
		AOC2LogicState knockbackLogic = new AOC2LogicKnockedBack(this, delivery.force, knockbackDir);
		knockbackLogic.AddExit(new AOC2ExitWhenComplete(knockbackLogic, _logic.logic.current));
		
		//_logic.SetLogic(knockbackLogic);
	}
	
	/// <summary>
	/// Kill this instance.
	/// </summary>
	public void Die()
	{
		//TODO: Play death animation
		if (gameObject.activeSelf){
        Pool();
		
        if (OnDeath != null)
        {
            OnDeath();
        }
        
		if (isEnemy)
		{
			AOC2EventManager.Combat.OnEnemyDeath(this);
		}
		else
		{
			AOC2EventManager.Combat.OnPlayerDeath(this);
		}
		
		}
	}
	
	/// <summary>
	/// Pool this instance.
	/// </summary>
	public void Pool()
	{
		AOC2ManagerReferences.poolManager.Pool(this);
	}
	
	#endregion
	
	#region Movement
	
	/// <summary>
	/// Move in the specified direction, at the specified speed
	/// </summary>
	/// <param name='direction'>
	/// Direction.
	/// </param>
	public bool Move(Vector3 direction, float speed, bool turnForward = true)
	{		
		direction.y = 0;
		
		direction.Normalize();
		
		if (model != null && turnForward)
		{
			model.transform.forward = direction;
		}
		
		if (AOC2Math.GroundDistanceSqr(aPos.position, targetPos.position) < MIN_MOVE_DIST)
		{
			aPos.position = targetPos.position;
			return true;
		}
		
		aPos.position += direction * speed * Time.deltaTime;
		return false;
	}
	
	/// <summary>
	/// Move in the specified direction at this unit's base speed
	/// </summary>
	/// <param name='direction'>
	/// Direction.
	/// </param>
	public bool Move(Vector3 direction)
	{
		return Move(direction, stats.moveSpeed);
	}
	
	/// <summary>
	/// Sprint in the specified direction.
	/// </summary>
	/// <param name='direction'>
	/// Direction.
	/// </param>
	public bool Sprint(Vector3 direction)
	{
		return Move(direction, sprintMod * stats.moveSpeed);
	}
	
	public void Activate()
	{
		activated = true;
	}
	
	public void Deactivate()
	{
		activated = false;
	}
	
	#endregion
	
	#region Collision [Empty]
	


	#endregion
	
	#endregion
}
