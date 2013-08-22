using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AOC2Particle))]
public class AOC2DeliveryParticle : MonoBehaviour {

	AOC2Particle particle;
	
	AOC2Delivery delivery;
	
	int maxHits;
	
	int currHits;
	
	void Awake()
	{
		particle = GetComponent<AOC2Particle>();
	}
	
	public void Init(float life, float speed, Vector3 dir, int hits, AOC2Delivery deliv)
	{
		particle.Init (life, dir, speed);
		
		maxHits = hits;
		
		currHits = 0;
		
		delivery = deliv;
	}
	
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
	
	void OnTriggerEnter(Collider other)
	{
		TryToHit(other.GetComponent<AOC2Unit>());
	}
	
	void OnTriggerStay(Collider other)
	{
		TryToHit(other.GetComponent<AOC2Unit>());
	}
	
}
