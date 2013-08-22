using UnityEngine;
using System.Collections;
using System;
using proto;

///<summary>
///@author Rob Giusti
///Rewrite of PrefabGenerator.js from the Dynamic Elements Effects package
///Rewritten in C# using coroutines for efficiency and for ability to use
///alongside our other gameplay code without having to make cross-language
///calls. 
///</summary>
public class AOC2DeliveryParticleGenerator : MonoBehaviour {
	
	SpellProto spellProto;	
	
	/// <summary>
	/// Rotation type.
	/// NONE: Passes direction from Delivery
	/// RANDOM: Creates in a random direction, within the given deviation from
	/// the Deilvery's direction
	/// ARC: 
	/// </summary>
	enum RotationType 
	{
		NONE,
		RANDOM_DIRECTION,
		RANDOM_AREA,
		ARC
	}
	
	/// <summary>
	/// The rotation type of this generator
	/// </summary>
	[SerializeField]
	RotationType rotType;
	
	/// <summary>
	/// The list of prefabs that this can make.
	/// Note that they all have to be poolable.
	/// </summary>
	public AOC2Particle prefab;
	
	/// <summary>
	/// The total number of particles that this generator
	/// will create
	/// </summary>
	public int thisManyTimes = 3;
	
	/// <summary>
	/// The time that it will generate particles over
	/// </summary>
	public float overThisTime = 1.0f;
	
	/// <summary>
	/// The delay time before this generator starts making particles
	/// </summary>
	public float delayTime = 0f;
	
	/// <summary>
	/// The size of the area in which this can distribute its particles
	/// </summary>
	public float area;
	
	/// <summary>
	/// The maximum rotation. 
	/// </summary>
	public float rotMax;
	
	/// <summary>
	/// The deviations in each direction.
	/// Stored as part of the class so that we don't have
	/// to allocate more data during function execution.
	/// </summary>
	private float x_dev, z_dev;
	
	/// <summary>
	/// The current rotation, in degrees
	/// </summary>
	private float rotCur;
	
	private BetterList<AOC2Particle> activeParticles = new BetterList<AOC2Particle>();
	
	/// <summary>
	/// The transform component
	/// </summary>
	public Transform trans;
	
	AOC2Delivery delivery;
	
	GameObject gameObj;
	
	/// <summary>
	/// The on generation begin event.
	/// If there is a delay, this is fired afterwards.
	/// </summary>
	Action OnGenerationBegin;
	
	/// <summary>
	/// The on generation complete event.
	/// </summary>
	Action OnGenerationComplete;
	
	/// <summary>
	/// Awake this instance.
	/// Set up component references.
	/// </summary>
	void Awake()
	{
		trans = transform;
		gameObj = gameObject;
		delivery = GetComponent<AOC2Delivery>();
	}
	
	public void Init(SpellProto proto)
	{
		spellProto = proto;
		
		thisManyTimes = spellProto.numberParticles;
		
		overThisTime = spellProto.deliveryDuration;
		
		Init ();
	}

	public void Init()
	{
		activeParticles.Clear();
		
		StartCoroutine(RunGenerator());
	}
	
	public virtual IEnumerator RunGenerator () 
	{
		AOC2Particle particle;
		Vector3 dir = delivery.direction;
		float angle = Mathf.Atan2 (dir.z, dir.x);
		float minAngle = angle - spellProto.angle/2 * Mathf.Deg2Rad;
		for (int i = 0; i < thisManyTimes; i++)
		{
			x_dev = trans.position.x + (UnityEngine.Random.value * area) - (area * 0.5f);
			z_dev = trans.position.z + (UnityEngine.Random.value * area) - (area * 0.5f);
			
			switch (spellProto.directionType) {
				case SpellProto.SpellDirectionType.ARC:
					angle = minAngle + i * spellProto.angle * Mathf.Deg2Rad / thisManyTimes;
					dir = new Vector3(Mathf.Cos (angle), 0, Mathf.Sin (angle));
					break;
				case SpellProto.SpellDirectionType.SCATTERED:
					angle = UnityEngine.Random.value * 360;
					dir = new Vector3(Mathf.Cos (angle), 0, Mathf.Sin (angle));
					break;
				default:
					break;
			}
			
			particle = AOC2ManagerReferences.poolManager.Get(prefab, new Vector3(x_dev, trans.position.y, z_dev)) as AOC2Particle;
			particle.GetComponent<AOC2DeliveryParticle>().Init(spellProto.particleDuration, spellProto.particleSpeed, 
				dir, spellProto.hitsPerParticle, delivery);
			
			//particle.Init(spellProto.particleDuration, delivery.direction, spellProto.particleSpeed);
			
			particle.trans.parent = trans;
			particle.trans.localScale = Vector3.one;
			
			particle.gameObj.layer = gameObj.layer;
			
			activeParticles.Add(particle);
			particle.OnDissolve += OnParticleDissolve;
			
			if (overThisTime > 0)
			{
				yield return new WaitForSeconds(overThisTime / thisManyTimes);
			}
		}
	}
	
	float DetermineDirection(int shotNum)
	{
		switch (rotType) {
			case RotationType.ARC:
				return 0;
		
			default:
				return 0;
		}
	}
	
	Vector3 DetermineOffset()
	{
		return Vector3.zero;
	}
	
	void OnParticleDissolve(AOC2Particle particle)
	{
		activeParticles.Remove(particle);
		particle.OnDissolve -= OnParticleDissolve;
		if (activeParticles.size == 0)
		{
			if (OnGenerationComplete != null)
			{
				OnGenerationComplete();
			}
		}
	}
}
