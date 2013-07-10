using UnityEngine;
using System;
using System.Collections;

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
	/// The max health.
	/// </summary>
	public float maxHealth = 1;
	
	/// <summary>
	/// The move speed.
	/// </summary>
	public float moveSpeed = 1;
	
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
	/// The local on death event for this unit
	/// </summary>
	public Action OnDeath;
	
	/// <summary>
	/// The Position of this unit.
	/// Transform wrapper that allows us to 
	/// </summary>
	public AOC2Position aPos;
	
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
	
	#endregion
	
	#region Private
	
	/// <summary>
	/// The current health.
	/// </summary>
	private float _health = 1;
	
	/// <summary>
	/// The prefab.
	/// </summary>
	private AOC2Poolable _prefab;
	
	/// <summary>
	/// The logic for this unit.
	/// </summary>
	private AOC2UnitLogic _logic;
	
	#endregion
	
	#region Constant
	
	//private static readonly Plane GROUND_PLANE = new Plane(Vector3.up, Vector3.zero);
	
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
	}
	
	/// <summary>
	/// DEBUG
	/// Start this instance.
	/// </summary>
	void Start()
	{
		DebugTint(tint);
	}
	
	/// <summary>
	/// Initialize this instance.
	/// Sets health, layer, and initializes the logic
	/// </summary>
	public void Init()
	{
		_health = maxHealth;
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
		if (_logic != null)
		{
			_logic.Init();
		}
	}
	
	/// <summary>
	/// Debugs tint.
	/// </summary>
	/// <param name='color'>
	/// Color to tint
	/// </param>
	private void DebugTint(Color color)
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
		_health -= deliv.damage;
		if (_health <= 0)
		{
			Die();
		}
	}
	
	/// <summary>
	/// Kill this instance.
	/// </summary>
	public void Die()
	{
		//TODO: Play death animation
		
		
		if (isEnemy)
		{
			AOC2EventManager.Combat.OnEnemyDeath(this);
		}
		else
		{
			AOC2EventManager.Combat.OnPlayerDeath(this);
		}
		
		if (OnDeath != null)
		{
			OnDeath();
		}
		
		Pool();
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
		direction.Normalize();
		
		aPos.position += direction * moveSpeed * Time.deltaTime;
	}
	
	public void Sprint(Vector3 direction)
	{
		direction.Normalize();
		
		aPos.position += direction * moveSpeed * Time.deltaTime;
	}
	
	#endregion
	
	#region Collision [Empty]
	

	
	#endregion
	
	#endregion
}
