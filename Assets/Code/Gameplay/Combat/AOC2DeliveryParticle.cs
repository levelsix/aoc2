using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// A particle that connects to a delivery and acts as the collider for an ability
/// </summary>
[RequireComponent (typeof (AOC2Particle))]
public class AOC2DeliveryParticle : MonoBehaviour {

	/// <summary>
	/// The particle component
	/// </summary>
	AOC2Particle particle;
	
	/// <summary>
	/// The delivery that created this particle
	/// </summary>
	AOC2Delivery delivery;
	
	/// <summary>
	/// The max targets this particle can hit before timing out
	/// </summary>
	int maxHits;
	
	/// <summary>
	/// The current number of hits.
	/// </summary>
	int currHits;
	
	/// <summary>
	/// Awake this instance.
	/// Set up internal component references.
	/// </summary>
	void Awake()
	{
		particle = GetComponent<AOC2Particle>();
	}
	
	/// <summary>
	/// Init the particle.
	/// </summary>
	/// <param name='life'>
	/// Life.
	/// </param>
	/// <param name='speed'>
	/// Speed.
	/// </param>
	/// <param name='dir'>
	/// Direction.
	/// </param>
	/// <param name='hits'>
	/// Hits.
	/// </param>
	/// <param name='deliv'>
	/// Delivery.
	/// </param>
	public void Init(float life, float speed, Vector3 dir, int hits, AOC2Delivery deliv)
	{
		particle.Init (life, dir, speed);
		
		maxHits = hits;
		
		currHits = 0;
		
		delivery = deliv;
	}
	
	/// <summary>
	/// Tries to hit a potential target. If successful, increments hit count and possibly
	/// pools itself.
	/// </summary>
	/// <param name='unit'>
	/// Unit that this particle has made contact with.
	/// </param>
	void TryToHit(AOC2Unit unit)
	{
		if (delivery.CanHit(unit))
		{
			delivery.Hit(unit);
			if (++currHits == maxHits)
			{
				particle.Pool();
			}
		}
	}
	
	/// <summary>
	/// Attempts to contact another collider when colliding
	/// </summary>
	/// <param name='other'>
	/// Collider being hit by this particle
	/// </param>
	void OnTriggerEnter(Collider other)
	{
		TryToHit(other.GetComponent<AOC2Unit>());
	}
	
	/// <summary>
	/// Attempts to contact another collider when colliding
	/// </summary>
	/// <param name='other'>
	/// Collider being hit by this particle
	/// </param>
	void OnTriggerStay(Collider other)
	{
		TryToHit(other.GetComponent<AOC2Unit>());
	}
	
}
