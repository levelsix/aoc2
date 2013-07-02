using UnityEngine;
using System.Collections;

/// <summary>
/// Component for a solid object which we don't want
/// other objects to be able to move through
/// </summary>
[RequireComponent (typeof (BoxCollider))]
public class AOC2Solid : MonoBehaviour {
	
	/// <summary>
	/// The box around this Solid.
	/// </summary>
	private BoxCollider _box;
	
	/// <summary>
	/// The transform of this Solid.
	/// </summary>
	private Transform _trans;
	
	/// <summary>
	/// Extra space to push things out to
	/// ensure that the collision is not still happening
	/// </summary>
	private const float COLL_PUSH = .1f;
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
		_box = GetComponent<BoxCollider>();
		_trans = transform;
	}
	
	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
	/// <param name='other'>
	/// Other.
	/// </param>
	void OnTriggerStay(Collider other)
	{
		if (other is BoxCollider)
		{
			other.transform.position = PushOther(other as BoxCollider);
		}
	}
	
	/// <summary>
	/// Pushs the other out of this
	/// </summary>
	/// <returns>
	/// The position to push other to
	/// </returns>
	/// <param name='other'>
	/// Whatever has colided with this
	/// </param>
	private Vector3 PushOther(BoxCollider other)
	{
		Vector3 otherPos = other.transform.position;
		if (Mathf.Abs(_trans.position.x - otherPos.x) > Mathf.Abs(_trans.position.z - otherPos.z))
		{
			if (_trans.position.x < otherPos.x)
			{
				otherPos = new Vector3(_trans.position.x + other.size.x/2 + _box.size.x/2 + COLL_PUSH, 
					otherPos.y, otherPos.z);
			}
			else
			{
				otherPos = new Vector3(_trans.position.x - other.size.x/2 - _box.size.x/2 - COLL_PUSH, 
					otherPos.y, otherPos.z);
			}
		}
		else
		{
			if (_trans.position.z < otherPos.z)
			{
				otherPos = new Vector3(otherPos.x, otherPos.y, 
					_trans.position.z + other.size.z/2 + _box.size.z/2 + COLL_PUSH);
			}
			else
			{
				otherPos = new Vector3(otherPos.x, otherPos.y, 
					_trans.position.z - other.size.z/2 - _box.size.z/2 + COLL_PUSH);
			}
		}
		return otherPos;
	}
	
}
