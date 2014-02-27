using UnityEngine;
using System.Collections;
using System;
using proto;

/// <summary>
/// @author Rob Giusti
/// Based on PrefabGenerator.js from the Dynamic Elements Effects package
/// Rewritten in C# using coroutines for efficiency and for ability to use
/// alongside our other gameplay code without having to make cross-language
/// calls. Derives its data from an ability, and makes particles that connect
/// to the generator's Delivery component
/// </summary>
[RequireComponent (typeof (AOC2Delivery))]
public class AOC2DeliveryParticleGenerator : MonoBehaviour {
	
	/// <summary>
	/// The spell proto.
	/// </summary>
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
	/// The number of particles generated so far
	/// </summary>
	public int particlesGenerated = 0;
	
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
	
	/// <summary>
	/// The List of active particles.
	/// </summary>
	private BetterList<AOC2Particle> activeParticles = new BetterList<AOC2Particle>();
	
	/// <summary>
	/// The transform component
	/// </summary>
	public Transform trans;
	
	/// <summary>
	/// The delivery component of this object
	/// </summary>
	AOC2Delivery delivery;
	
	/// <summary>
	/// Reference to the gameObject component, so that we don't have to call GetComponent
	/// when changing layers
	/// </summary>
	GameObject gameObj;
	
	/// <summary>
	/// The on generation begin event.
	/// If there is a delay, this is fired afterwards.
	/// </summary>
	public Action OnGenerationBegin;
	
	/// <summary>
	/// The on generation complete event.
	/// </summary>
	public Action OnGenerationComplete;
	
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
	
	/// <summary>
	/// Initialize the generator from the given proto
	/// </summary>
	/// <param name='proto'>
	/// Proto to derive values from
	/// </param>
	public void Init(SpellProto proto)
	{
		spellProto = proto;
		
		thisManyTimes = spellProto.numberParticles;
		
		overThisTime = spellProto.deliveryDuration;
		
		area = spellProto.area;
		
		Init ();
	}
	
	/// <summary>
	/// Initialize this instance, starting the generator function
	/// </summary>
	public void Init()
	{
		activeParticles.Clear();
		
		particlesGenerated = 0;
		
		StartCoroutine(RunGenerator());
	}
	
	/// <summary>
	/// Runs the generator, creating particles periodically
	/// </summary>
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
			
			DetermineDirection (ref dir, ref angle, minAngle, i);
			
			particle = AOC2ManagerReferences.poolManager.Get(prefab, new Vector3(x_dev, trans.position.y, z_dev)) as AOC2Particle;
			particle.GetComponent<AOC2DeliveryParticle>().Init(spellProto.particleDuration, spellProto.particleSpeed, 
				dir, spellProto.hitsPerParticle, delivery);
			
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

	/// <summary>
	/// Determines the Direction.
	/// </summary>
	/// <param name='dir'>
	/// Dir.
	/// </param>
	/// <param name='angle'>
	/// Angle.
	/// </param>
	/// <param name='minAngle'>
	/// Minimum angle.
	/// </param>
	/// <param name='i'>
	/// Particle number
	/// </param>
	void DetermineDirection (ref Vector3 dir, ref float angle, float minAngle, int i)
	{
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
	}
	
	/// <summary>
	/// When a particle dissolves, removes it from the list.
	/// Fires the GenerationComplete event when all particles have been removed.
	/// Note that we use particles generated in case the lifetime of a particle
	/// happens to be shorter than the time it takes to generate another, which
	/// would lead to generation complete being fired before all of the particles
	/// have been generated
	/// </summary>
	/// <param name='particle'>
	/// Particle which has dissolved
	/// </param>
	void OnParticleDissolve(AOC2Particle particle)
	{
		activeParticles.Remove(particle);
		particle.OnDissolve -= OnParticleDissolve;
		if (++particlesGenerated == thisManyTimes)
		{
			if (OnGenerationComplete != null)
			{
				OnGenerationComplete();
			}
		}
	}
}
