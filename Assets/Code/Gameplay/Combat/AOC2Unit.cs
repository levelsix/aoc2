using UnityEngine;
using System;
using System.Collections;
using proto;

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
	
	public AOC2DoubleLerpBar healthBarPrefab;
	
	[HideInInspector]
	public AOC2DoubleLerpBar healthBar;
	
	/// <summary>
	/// The Health Bar Offset, multiplied by size
	/// </summary>
	public Vector3 healthBarOffset;

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
	public AOC2Position targetPos = null;
	
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
	
	/// <summary>
	/// Action triggered after this unit inits itself.
	/// Synchronizes initialization with other components that need to initialize
	/// themselves.
	/// </summary>
	public Action OnInit;
	
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
	
	public AOC2UnitEquipment equipment;
    
    public Transform trans;
	
	public AOC2Model model;
	
	/// <summary>
	/// The local controller.
	/// Mainly, just check != null to see if certain events should
	/// be triggered
	/// </summary>
	public AOC2LocalPlayerController localController;
	
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
	override public void Spawn(Vector3 origin, Transform parent = null)
	{
		AOC2Unit unit = AOC2ManagerReferences.poolManager.Get(this, origin) as AOC2Unit;
		unit.Init();
		if (parent != null)
		{
			unit.trans.parent = parent;
		}
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
		unit.DebugTint(tint);
		return unit;
	}
	
	/// <summary>
	/// Awake this instance.
	/// Set up position and logic
	/// </summary>
	void Awake()
	{
		_logic = GetComponent<AOC2UnitLogic>();
        trans = transform;
		aPos = new AOC2Position(trans);
        nav = GetComponent<NavMeshAgent>();
		localController = GetComponent<AOC2LocalPlayerController>();
		if (model == null)
		{
			model = GetComponent<AOC2Model>();
		}
		equipment = GetComponent<AOC2UnitEquipment>();
	}
	
	void Start()
	{
		nav.speed = GetStat(AOC2Values.UnitStat.MOVE_SPEED);
	}
	
	void Update()
	{
		if (healthBar != null)
		{
			healthBar.trans.position = trans.position + healthBarOffset;
		}
	}
	
	/// <summary>
	/// Debug: Init without proto
	/// Initialize this instance.
	/// Sets health, layer, and initializes the logic
	/// </summary>
	public void Init()
	{
		targetPos = new AOC2Position(aPos.position);
        
        if (ranged)
        {
            basicAttackAbility = new AOC2Ability(AOC2AbilityLists.Generic.baseRangeAttackProto);
        }
        else
        {
            basicAttackAbility = new AOC2Ability(AOC2AbilityLists.Generic.baseMeleeAttackProto);   
        }
        
		health = GetStat(AOC2Values.UnitStat.HEALTH);
		
		mana = GetStat(AOC2Values.UnitStat.MANA);
		
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
	
	public bool UseMana(int amount)
	{
		if (mana < amount)
		{
			//TODO UI & Sound on failed mana use
			
			return false;
		}
		mana -= amount;
		
		if (localController != null && AOC2EventManager.Combat.OnPlayerManaChange != null)
		{
			AOC2EventManager.Combat.OnPlayerManaChange(this);
		}
		
		return true;
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
		int damage = (int) (((deliv.power) - (GetStat(AOC2Values.UnitStat.DEFENSE) * BASE_DEFENSE_MOD)) * AOC2Math.ResistanceMod(GetStat(AOC2Values.UnitStat.RESISTANCE)));
		if (damage > 0){
			health -= damage;
			
			DamageTextPopup(damage);
			if (healthBarPrefab != null)
			{
				ManageHealthBar(damage);
			}
			
			//Debug.Log("Damage: " + deliv.damage + ", Taken: " + damage);
			if (health <= 0)
			{
				StartCoroutine(Die());
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
	
	void DamageTextPopup(float amount)
	{
		AOC2DamageText text = AOC2ManagerReferences.gameUIManager.GrabUIRef(AOC2ManagerReferences.combatPrefabs.damageText, trans.position) 
			as AOC2DamageText;
		text.Init(this, amount);
	}
	
	void ManageHealthBar(float amount)
	{
		if (healthBar == null)
		{
			healthBar = AOC2ManagerReferences.gameUIManager.GrabUIRef(healthBarPrefab, trans.position) as AOC2DoubleLerpBar;
			healthBar.Init();
			healthBar.OnPool += OnHealthBarPool;
		}
		healthBar.SetAmounts((health+amount)/GetStat(AOC2Values.UnitStat.HEALTH), ((float)health)/GetStat(AOC2Values.UnitStat.HEALTH));
	}
	
	void OnHealthBarPool()
	{
		healthBar.OnPool -= OnHealthBarPool;
		healthBar = null;
	}
	
	public void Knockback(AOC2Delivery delivery)
	{
		//Debug.Log("Knockback");
		float force = delivery.spellProto.force - mass;
		
		if (force > 0)
		{
			Vector3 knockbackDir = delivery.direction;
			if (knockbackDir == Vector3.zero)
			{
				knockbackDir = (aPos.position - delivery.transform.position).normalized;
			}
			
			AOC2LogicState knockbackLogic = new AOC2LogicKnockedBack(this, force, knockbackDir);
			knockbackLogic.AddExit(new AOC2ExitWhenComplete(knockbackLogic, _logic.logic.current));
			
			_logic.SetLogic(knockbackLogic);
		}
	}
	
	/// <summary>
	/// Kill this instance.
	/// </summary>
	public IEnumerator Die()
	{
		//TODO: Play death animation
		yield return null;
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
		return Move(direction, GetStat(AOC2Values.UnitStat.MOVE_SPEED));
	}
	
	/// <summary>
	/// Sprint in the specified direction.
	/// </summary>
	/// <param name='direction'>
	/// Direction.
	/// </param>
	public bool Sprint(Vector3 direction)
	{
		return Move(direction, sprintMod * GetStat(AOC2Values.UnitStat.MOVE_SPEED));
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
	
	public int GetStat(SpellProto.UnitStat stat)
	{
		return GetStat((AOC2Values.UnitStat) stat);
	}
	
	public int GetStat(AOC2Values.UnitStat stat)
	{
		if (equipment != null)
		{
			return stats[stat] + equipment[stat];
		}
		else
		{
			return stats[stat];
		}
	}
	
	public override System.Collections.Generic.Dictionary<AOC2Spawnable, int> GetCounts ()
	{
		return new System.Collections.Generic.Dictionary<AOC2Spawnable, int>() {{this, 1}};
	}
}
