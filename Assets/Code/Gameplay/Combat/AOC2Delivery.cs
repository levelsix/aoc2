using UnityEngine;
using System.Collections;

public class AOC2Delivery : MonoBehaviour, AOC2Poolable {
	
	[HideInInspector]
	public float damage;
	
	[HideInInspector]
	public float speed;
	
	[HideInInspector]
	public float lifetime;
	
	[HideInInspector]
	public Vector3 direction;
	
	/// <summary>
	/// The pointer to this delivery's own prefab.
	/// </summary>
	private AOC2Delivery _prefab;
	
	/// <summary>
	/// A public getter and setter, so that we can
	/// make this part of the Poolable interface
	/// </summary>
	public AOC2Poolable prefab{
		get
		{
			return _prefab;
		}
		set
		{
			_prefab = value as AOC2Delivery;
		}
	}
	
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2Delivery deliv = Instantiate(this, origin, Quaternion.identity) as AOC2Delivery;
		deliv.prefab = this;
		return deliv;
	}
	
	// Use this for initialization
	public void Init (float dam, float spd, float life, Vector3 dir) 
	{
		gameObject.SetActive(true);
		
		damage = dam;
		speed = spd;
		lifetime = life;
		direction = dir;
		
	}
	
	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}
	
	void OnTriggerEnter (Collider other)
	{
		AOC2Unit unit = other.GetComponent<AOC2Unit>();
		if (unit != null)
		{
			Debug.Log("Hit!");
			Pool();
		}
	}
	
	public void Pool()
	{
		AOC2ManagerReferences.poolManager.Pool(this);
	}
}
