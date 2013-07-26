using UnityEngine;
using System.Collections;

public class AOC2Particle : MonoBehaviour, AOC2Poolable {

	private AOC2Particle _prefab;
	
	public float lifeTime = float.NaN;
	
	public ParticleEmitter[] emitters;
	
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
	
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2Particle parti = Instantiate(this, origin, Quaternion.identity) as AOC2Particle;
		parti.prefab = this;
		return parti;
	}
	
	virtual public void Init()
	{
		foreach (ParticleEmitter emitter in emitters) 
		{
			emitter.Emit();
		}
		if (float.IsNaN(lifeTime))
		{
			Debug.LogError("Set lifetime of AOC2Particle");
		}
		else
		{
			StartCoroutine(PoolAfterLifetime(lifeTime));
		}
	}
	
	public void Awake()
	{
		emitters = GetComponentsInChildren<ParticleEmitter>();
	}
	
	public void Init(float life)
	{
		foreach (ParticleEmitter emitter in emitters) 
		{
			emitter.Emit();
		}
		StartCoroutine(PoolAfterLifetime(life));
	}
	
	public IEnumerator PoolAfterLifetime(float life)
	{
		yield return new WaitForSeconds(life);
		Pool();
	}
	
	public void Pool()
	{
		AOC2ManagerReferences.poolManager.Pool(this);
	}
	
	
}
