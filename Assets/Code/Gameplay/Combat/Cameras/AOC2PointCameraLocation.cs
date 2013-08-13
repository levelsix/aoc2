using UnityEngine;
using System.Collections;

public class AOC2PointCameraLocation : MonoBehaviour {
	
	float range;
	
	/// <summary>
	/// The transform at which we want the camera to move towards when this
	/// location is triggered
	/// </summary>
	public Transform cameraPoint;
	
	/// <summary>
	/// The transform which we want the camera to point at when at this camera point
	/// </summary>
	[SerializeField]
	public Transform hitArea;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<AOC2LocalPlayerController>() != null && AOC2EventManager.Cam.OnPlayerEnterCameraArea != null)
		{
			AOC2EventManager.Cam.OnPlayerEnterCameraArea(this);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<AOC2LocalPlayerController>() != null && AOC2EventManager.Cam.OnPlayerExitCameraArea != null)
		{
			AOC2EventManager.Cam.OnPlayerExitCameraArea(this);
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.gray;
		Gizmos.DrawSphere(cameraPoint.position, 1f);
	}
	
	public float GroundDistSqr(Vector3 point)
	{
		return AOC2Math.GroundDistanceSqr(cameraPoint.position, point);
	}
	
	public bool InRange(Vector3 point)
	{
		return AOC2Math.GroundDistanceSqr(cameraPoint.position, point) < (range * range);
	}
}
