using UnityEngine;
using System.Collections;

public class AOC2Delivery : MonoBehaviour, AOC2Poolable {
	
	/// <summary>
	/// The damage.
	/// </summary>
	[HideInInspector]
	public float damage;
	
	/// <summary>
	/// The speed.
	/// </summary>
	[HideInInspector]
	public float speed;
	
	/// <summary>
	/// The direction.
	/// </summary>
	[HideInInspector]
	public Vector3 direction;
	
	/// <summary>
	/// The size of the delivery
	/// Must be hand-set.
	/// </summary>
	public float size;
	
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
		direction = dir;
		
		StartCoroutine(DieAfterLifetime(life));
	}
	
	/// <summary>
	/// Dies the after lifetime.
	/// Uses instances to make sure that we don't
	/// accidentally kill a delivery because a previous cycle
	/// of the gameobject died
	/// </summary>
	IEnumerator DieAfterLifetime(float life)
	{
		yield return new WaitForSeconds(life);
		Pool();
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
			unit.TakeDamage(this);
			Pool();
		}
	}
	
	public void Pool()
	{
		AOC2ManagerReferences.poolManager.Pool(this);
	}
}
