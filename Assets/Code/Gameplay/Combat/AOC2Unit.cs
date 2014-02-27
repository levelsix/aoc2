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
public class AOC2Unit : MonoBehaviour, AOC2Poolable {
	
	#region Members
	
	#region Public
	
	/// <summary>
	/// Debug string that reflects the current logic state that this
	/// unit is in.
	/// </summary>
	public string currentLogicState = "";

	/// <summary>
	/// The stats of this unit.
	/// </summary>
	public AOC2UnitStats stats;
	
	/// <summary>
	/// The nav mesh agent component.
	/// Character speed is passed to this to make sure
	/// that the unit moves at the proper speed.
	/// </summary>
	public NavMeshAgent nav;
	
	/// <summary>
	/// The Prefab for this unit's health bar.
	/// Useful for if we want different units to display different
	/// health bars.
	/// If this prefab is null, the unit will not display a health bar
	/// when damaged.
	/// </summary>
	public AOC2DoubleLerpBar healthBarPrefab;
	
	/// <summary>
	/// The game UI health bar that this unit is currently using
	/// to display its health. 
	/// </summary>
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
    
	/// <summary>
	/// Whether this unit is ranged or not
	/// </summary>
    public bool ranged;
	
	/// <summary>
	/// Whether or not this unit is currently active
	/// </summary>
	public bool _activated = false;
	
	/// <summary>
	/// If the unit is within this distance from its movement target,
	/// it will snap to its target. Prevents stuttering around a target point.
	/// </summary>
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
	/// <summary>
	/// The monster proto, if this unit is a monster
	/// </summary>
	public MonsterProto monsterProto;
	
	#endregion
	
	/// <summary>
	/// Gets or sets the prefab of this unit.
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
				_prefab = value as AOC2Poolable;
			}
		}
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="AOC2Unit"/> is activated.
	/// When set, also triggers the OnActivate or OnDeactivate event
	/// </summary>
	/// <value>
	/// <c>true</c> if activated; otherwise, <c>false</c>.
	/// </value>
	public bool activated{
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
    
	/// <summary>
	/// This unit's unique ID number.
	/// Used for debug purposes.
	/// </summary>
    public int id;
    
	/// <summary>
	/// Static ticker for the next ID to assign to a unit.
	/// Incremented every time we assign an ID.
	/// </summary>
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
	
	/// <summary>
	/// The equipment component.
	/// Used to look up stats.
	/// TODO: Determine Unit stats at Init-time to already be a combination of equipment
	/// and character stats. NOTE: Keep in mind the effects of durability mid-combat.
	/// </summary>
	public AOC2UnitEquipment equipment;
    
	/// <summary>
	/// The transform component.
	/// Reference is stored here so that we can easily move and turn the unit
	/// without needing to call GetComponent every time.
	/// </summary>
    public Transform trans;
	
	/// <summary>
	/// The game object.
	/// Reference is stored here so that activation/deactivation can be
	/// done without needing to call GetComponent every time.
	/// </summary>
	public GameObject gameObj;
	
	/// <summary>
	/// Reference to the model component.
	/// </summary>
	public AOC2Model model;
	
	/// <summary>
	/// The local controller component. Null if this unit is not the
	/// local player's character.
	/// Mainly, only used to check != null to see if certain events should
	/// be triggered.
	/// TODO: Clean up code so that we don't need a pointer to this
	/// If certain events should be tied to this, they should happen in this.
	/// </summary>
	public AOC2LocalPlayerController localController;
	
	#endregion
	
	#region Constants
	
	/// <summary>
	/// The modifier used against the unit's defense stat to determine how much damage
	/// is actually blocked.
	/// Can also be determined as the amount of damage that one point of defense stops.
	/// </summary>
	const float BASE_DEFENSE_MOD = .25f;
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Poolable's getter for transform that defers to
	/// the internally kept trans reference. Using monobehaviour's
	/// built-in transform property does a GetComponent call every
	/// time we check it, which gets very inefficient very quickly.
	/// </summary>
	/// <value>
	/// A reference to the transform component of this object.
	/// </value>
	public Transform transf{
		get
		{
			return trans;
		}
	}
	
	/// <summary>
	/// Poolable's getter for gameObject that defers to the
	/// internally kept gameObj reference. Using monobehaviour's
	/// built-in transform property does a GetComponent call
	/// every time we check it, which gets very inefficient very
	/// quickly.
	/// </summary>
	/// <value>
	/// A reference to the gameObject component of this object.
	/// </value>
	public GameObject gObj{
		get
		{
			return gameObj;
		}
	}
	
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
	public void Spawn(Vector3 origin, AOC2UnitSpawner parent)
	{
		AOC2Unit unit = AOC2ManagerReferences.poolManager.Get(this, origin) as AOC2Unit;
		unit.Init();
		if (parent != null)
		{
			parent.AddUnit(unit);
			unit.trans.parent = parent.trans;
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
		//unit.prefab = this;
        unit.id = nextID++;
		//unit.TintModel(tint);
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
		gameObj = gameObject;
		aPos = new AOC2Position(trans);
        nav = GetComponent<NavMeshAgent>();
		localController = GetComponent<AOC2LocalPlayerController>();
		if (model == null)
		{
			model = GetComponent<AOC2Model>();
		}
		equipment = GetComponent<AOC2UnitEquipment>();
	}
	
	#endregion
	
	/// <summary>
	/// Update this instance.
	/// If there is a health bar attached to this unit, move it appropriately
	/// </summary>
	void Update()
	{
		if (healthBar != null)
		{
			healthBar.trans.position = trans.position + healthBarOffset;
		}
	}
	
	//Need an Init with Player Stats!
	
	public void Init(MonsterProto proto)
	{
		targetPos = new AOC2Position(aPos.position);
		
		stats.strength = proto.attack;
		stats.defense = proto.defense;
		stats.maxHealth = proto.maxHealth;
		stats.maxMana = proto.maxMana;
		
		tint = new Color(proto.color.r/255f, proto.color.g/255f, proto.color.b/255f);
		
		monsterProto = proto;
		
		TintModel(tint);
		
		Init ();
	}
	
	/// <summary>
	/// Debug: Init without proto
	/// Initialize this instance.
	/// Sets health, layer, and initializes the logic
	/// </summary>
	public void Init()
	{
		targetPos = new AOC2Position(aPos.position);
		
		nav.speed = GetStat(AOC2Values.UnitStat.MOVE_SPEED);
		
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
			gameObj.layer = AOC2Values.Layers.ENEMY;
		}
		else
		{
			AOC2EventManager.Combat.OnSpawnPlayer(this);
			gameObj.layer = AOC2Values.Layers.PLAYER;
		}
	}
	
	/// <summary>
	/// Debugs tint.
	/// </summary>
	/// <param name='color'>
	/// Color to tint
	/// </param>
	public void TintModel(Color color)
	{
		tint = color;
		if (model != null)
		{
			model.Tint(color);
		}
	}
	
	/// <summary>
	/// Attempts to use the unit's mana.
	/// Fails if there is not enough mana
	/// </summary>
	/// <returns>
	/// Whether there was enough mana to be used
	/// </returns>
	/// <param name='amount'>
	/// The amount of mana to be spent
	/// </param>
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
		if (health > 0 && damage > 0){
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
	}
	
	/// <summary>
	/// Creates the damage text popup.
	/// </summary>
	/// <param name='amount'>
	/// Amount of damage taken
	/// </param>
	void DamageTextPopup(float amount)
	{
		AOC2DamageText text = AOC2ManagerReferences.gameUIManager.GrabUIRef(AOC2ManagerReferences.combatPrefabs.damageText, trans.position) 
			as AOC2DamageText;
		text.Init(this, amount);
	}
	
	/// <summary>
	/// Sets the health bar up to this unit with the proper scaling
	/// </summary>
	/// <param name='amount'>
	/// Amount that health has changed by
	/// </param>
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
	
	/// <summary>
	/// When the health bar pools, releases pointer to health bar
	/// </summary>
	void OnHealthBarPool()
	{
		healthBar.OnPool -= OnHealthBarPool;
		healthBar = null;
	}
	
	/// <summary>
	/// Determines and applies knockback force, overriding the current logic if
	/// </summary>
	/// <param name='delivery'>
	/// Delivery.
	/// </param>
	public void Knockback(AOC2Delivery delivery)
	{
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
	/// Kill and pool this instance.
	/// </summary>
	public IEnumerator Die()
	{
		
		_logic.logic.OnExitState();
		_logic.SetLogic(new AOC2LogicDoNothing(this));
		
		model.SetAnimation(AOC2Values.Animations.Anim.DEATH, true);
		
		IEnumerator waitUntilAnimated = model.WaitUntilAnimationComplete(AOC2Values.Animations.Anim.DEATH);
		
		if (gameObj.activeSelf){
			
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
		
		while(waitUntilAnimated.MoveNext())
		{
			yield return waitUntilAnimated.Current;
		}
		
		model.SetAnimation(AOC2Values.Animations.Anim.DEATH, false);
		
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
	
	#endregion
	
	/// <summary>
	/// Activate this instance.
	/// </summary>
	public void Activate()
	{
		activated = true;
	}
	
	/// <summary>
	/// Deactivate this instance.
	/// </summary>
	public void Deactivate()
	{
		activated = false;
	}
	
	#endregion
	
	/// <summary>
	/// Gets the specified stat.
	/// </summary>
	/// <returns>
	/// The value of stat
	/// </returns>
	/// <param name='stat'>
	/// The UnitStat enum 
	/// </param>
	public int GetStat(SpellProto.UnitStat stat)
	{
		return GetStat((AOC2Values.UnitStat) stat);
	}
	
	/// <summary>
	/// Gets the specified stat.
	/// </summary>
	/// <returns>
	/// The value of stat
	/// </returns>
	/// <param name='stat'>
	/// The UnitStat enum 
	/// </param>
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
	
	/*
	/// <summary>
	/// Gets a dictionary containing a reference to this with a single counter.
	/// When the spawners check for counts, this will add an entry for this enemy through
	/// the dictionary merging process.
	/// </summary>
	/// <returns>
	/// A dictionary containing a reference to this unit with a single count
	/// </returns>
	public System.Collections.Generic.Dictionary<AOC2Spawnable, int> GetCounts ()
	{
		return new System.Collections.Generic.Dictionary<AOC2Spawnable, int>() {{this, 1}};
	}
	*/

}
