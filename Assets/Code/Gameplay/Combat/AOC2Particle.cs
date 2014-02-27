using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// @author Rob Giusti
/// Our own implementation of particles for particle systems,
/// which can be easily pooled and reused.
/// </summary>
public class AOC2Particle : MonoBehaviour, AOC2Poolable {
	
	/// <summary>
	/// The prefab of this particle, to be used for pooling indexing
	/// </summary>
	private AOC2Particle _prefab;
	
	/// <summary>
	/// The transform component
	/// </summary>
	public Transform trans;
	
	/// <summary>
	/// The game object component
	/// </summary>
	public GameObject gameObj;
	
	/// <summary>
	/// The max amount of time that this particle will be
	/// active for
	/// </summary>
	public float lifeTime = float.NaN;
	
	/// <summary>
	/// The direction that this is travelling in
	/// </summary>
	public Vector3 direction = Vector3.zero;
	
	/// <summary>
	/// The speed at which this particle is movign
	/// </summary>
	public float speed = 0f;
	
	/// <summary>
	/// Any subemitters attached to this particle.
	/// Set in Inspector!
	/// </summary>
	public ParticleEmitter[] emitters;
	
	/// <summary>
	/// Event fired when this particle dissolves and pools
	/// itself.
	/// </summary>
	public Action<AOC2Particle> OnDissolve;
	
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
	
	/// <summary>
	/// Gets or sets the prefab.
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
			_prefab = value as AOC2Particle;
		}
	}
	
	/// <summary>
	/// Make a copy of this particle at the specified position
	/// </summary>
	/// <param name='origin'>
	/// Origin at which to create the particle
	/// </param>
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2Particle parti = Instantiate(this, origin, Quaternion.identity) as AOC2Particle;
		parti.prefab = this;
		return parti;
	}
	
	/// <summary>
	/// Awake this instance.
	/// Assign internal component references.
	/// </summary>
	virtual public void Awake()
	{
		emitters = GetComponentsInChildren<ParticleEmitter>();
		trans = transform;
		gameObj = gameObject;
	}
	
	/// <summary>
	/// Update this instance by moving the particle appropriately
	/// </summary>
	virtual public void Update()
	{
		Move(Time.deltaTime);
	}
	
	virtual public void Move(float time)
	{
		trans.position = trans.position + direction * speed * time;
	}
	
	/// <summary>
	/// Init this particle with the specified life, direction and moveSpeed.
	/// </summary>
	/// <param name='life'>
	/// Life.
	/// </param>
	/// <param name='direct'>
	/// Direction.
	/// </param>
	/// <param name='moveSpeed'>
	/// Move speed.
	/// </param>
	public void Init(float life, Vector3 direct, float moveSpeed)
	{
		direction = direct;
		
		speed = moveSpeed;
		
		foreach (ParticleEmitter emitter in emitters) 
		{
			if (emitter != null){
				emitter.Emit();
			}
		}
		
		StartCoroutine(PoolAfterLifetime(life));
	}
	
	/// <summary>
	/// Pools this after its lifetime runs out
	/// </summary>
	/// <param name='life'>
	/// Max lifetime of the particle
	/// </param>
	public IEnumerator PoolAfterLifetime(float life)
	{
		yield return new WaitForSeconds(life);
		Pool();
	}
	
	/// <summary>
	/// Pool this instance and fire the dissolve event
	/// </summary>
	public void Pool()
	{
		if (OnDissolve != null)
		{
			OnDissolve(this);
		}
		AOC2ManagerReferences.poolManager.Pool(this);
	}
}
