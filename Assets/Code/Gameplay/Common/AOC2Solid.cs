using UnityEngine;
using System.Collections;

/// <summary>
/// Component for a solid object which we don't want
/// other objects to be able to move through
/// </summary>
[RequireComponent (typeof (BoxCollider))]
public class AOC2Solid : MonoBehaviour {
	
	/// <summary>
	/// The transform of this Solid.
	/// </summary>
	private Transform _trans;
	
	/// <summary>
	/// Extra space to push things out to
	/// ensure that the collision is not still happening
	/// </summary>
	private const float COLL_PUSH = .3f;
    
    public bool canBePushed;
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
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
		if (canBePushed && other.GetComponent<AOC2Solid>() != null)
		{
			transform.position = PushAwayFrom(other);
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
	private Vector3 PushAwayFrom(Collider other)
	{
        Vector3 otherPos = other.transform.position;
		Vector3 pushPos = transform.position;
		if (Mathf.Abs(_trans.position.x - otherPos.x) > Mathf.Abs(_trans.position.z - otherPos.z))
		{
			if (_trans.position.x < otherPos.x)
			{
				pushPos.x -= COLL_PUSH * Time.deltaTime;
			}
			else
			{
				pushPos.x += COLL_PUSH * Time.deltaTime;
			}
		}
		else
		{
			if (_trans.position.z < otherPos.z)
			{
				pushPos.z -= COLL_PUSH * Time.deltaTime;
			}
			else
			{
				pushPos.z += COLL_PUSH * Time.deltaTime;
			}
		}
		return pushPos;
	}
	
}
