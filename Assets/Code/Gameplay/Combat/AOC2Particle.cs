using UnityEngine;
using System.Collections;
using System;

public class AOC2Particle : MonoBehaviour, AOC2Poolable {

	private AOC2Particle _prefab;
	
	public Transform trans;
	
	public GameObject gameObj;
	
	public float lifeTime = float.NaN;
	
	public Vector3 direction = Vector3.zero;
	
	public float speed = 0f;
	
	public ParticleEmitter[] emitters;
	
	public Action<AOC2Particle> OnDissolve;
	
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
	
	virtual public void Awake()
	{
		emitters = GetComponentsInChildren<ParticleEmitter>();
		trans = transform;
		gameObj = gameObject;
	}
	
	virtual public void Update()
	{
		trans.Translate(direction * speed * Time.deltaTime);
	}
	
	public void Init(float life)
	{
		Init (life, direction, speed);
	}
	
	public void Init(float life, Vector3 direct, float moveSpeed)
	{
		direction = direct;
		
		speed = moveSpeed;
		
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
		if (OnDissolve != null)
		{
			OnDissolve(this);
		}
		AOC2ManagerReferences.poolManager.Pool(this);
	}
}
