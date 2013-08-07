using UnityEngine;
using System.Collections;

public class AOC2PointCameraLocation : MonoBehaviour {
	
	[SerializeField]
	float range;
	
	public Transform trans;
	
	void Awake()
	{
		trans = transform;
	}
	
	void OnEnable()
	{
		AOC2PointCamera.locs.Add(this);
	}
	
	void OnDisable()
	{
		AOC2PointCamera.locs.Remove(this);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.gray;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	
	public float GroundDistSqr(Vector3 point)
	{
		return AOC2Math.GroundDistanceSqr(trans.position, point);
	}
	
	public bool InRange(Vector3 point)
	{
		return AOC2Math.GroundDistanceSqr(trans.position, point) < (range * range);
	}
}
