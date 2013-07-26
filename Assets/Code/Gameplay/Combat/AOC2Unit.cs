using UnityEngine;
using System;
using System.Collections;
using com.lvl6.proto;

/// <summary>
/// @author Rob Giusti
/// Base Unit class. All Units, Player and Enemy, use this component.
/// Ties together movement, spawning, dieing, pooling, and logic
/// </summary>
[RequireComponent (typeof (BoxCollider))]
public class AOC2Unit : AOC2Spawnable, AOC2Poolable {
	
	#region Members
	
	#region Public
	
	/// <summary>
	/// The protocol buffer that this unit was created from
	/// </summary>
    public UnitProto proto;
	
	/// <summary>
	/// The stats of this unit.
	/// </summary>
	public AOC2UnitStats stats;
	
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
    /// The basic attack ability.
    /// </summary>
    [HideInInspector]
    public AOC2Ability basicAttackAbility;
    
    public bool ranged;
	
	#region Action Delegates
	
	/// <summary>
	/// The local event for taking damage.
	/// </summary>
	public Action<int> OnDamage;
	
	/// <summary>
	/// The local on death event for this unit
	/// </summary>
	public Action OnDeath;
	
	public Action OnUseAbility;
	
	public Action OnPathfind;
	
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
		aPos = new AOC2Position(transform);
        _trans = transform;
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
		
		_trans.Translate(0, _trans.localScale.y / 2, 0);
	}
        
    /// <summary>
    /// Initialize this instance, using a UnitProto.
    /// Sets health, layer, and initializes the logic
    /// </summary>
    public void Init(UnitProto _proto)
    {
        proto = _proto;
        
        stats = new AOC2UnitStats(_proto.stats);
        health = stats.maxHealth;
        mana = stats.maxMana;
        
        name = _proto.name;
     
        DebugTint(AOC2Math.ProtoToColor(proto.color));
        
        _trans.localScale = new Vector3(_proto.size, _proto.size, _proto.size);
		
		_trans.Translate(0, _proto.size / 2, 0);
            
        if (_logic != null)
        {
            _logic.Init();
        }
         
        if (_proto.enemy)
        {
            gameObject.layer = AOC2Values.Layers.ENEMY;
            AOC2EventManager.Combat.OnSpawnEnemy(this);
        }
        else
        {
            gameObject.layer = AOC2Values.Layers.PLAYER;
            AOC2EventManager.Combat.OnSpawnPlayer(this);
        }
    }
	
	/// <summary>
	/// Debugs tint.
	/// </summary>
	/// <param name='color'>
	/// Color to tint
	/// </param>
	public void DebugTint(Color color)
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Color[] colorArr = new Color[mesh.vertices.Length];
		for (int i = 0; i < mesh.vertices.Length; i++) {
			colorArr[i] = color;
		}
		mesh.colors = colorArr;
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
			else if (OnDamage != null)
			{
				OnDamage(damage);
			}
		}
		else
		{
			Debug.Log("High defense, attack blocked!");
		}
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
	/// Move in the specified direction.
	/// </summary>
	/// <param name='direction'>
	/// Direction.
	/// </param>
	public void Move(Vector3 direction)
	{		
		direction.y = 0;
		
		direction.Normalize();
		
		aPos.position += direction * stats.moveSpeed * Time.deltaTime;
	}
	
	public void Sprint(Vector3 direction)
	{
		direction.Normalize();
		
		aPos.position += direction * stats.moveSpeed * sprintMod * Time.deltaTime;
	}
	
	#endregion
	
	#region Collision [Empty]
	

	
	#endregion
	
	#endregion
}
