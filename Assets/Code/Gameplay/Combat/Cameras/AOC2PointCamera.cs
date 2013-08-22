#define DEBUG 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// A camera that jumps between PointCameraLocations and stays pointed
/// at a target.
/// </summary>
public class AOC2PointCamera : MonoBehaviour {
	
	/// <summary>
	/// The target that this camera will be following.
	/// Specific to each camera location
	/// </summary>
	Transform target;
	
	/// <summary>
	/// The transform of this camera, so that we can move and turn it
	/// manually.
	/// </summary>
	Transform _tran;
	
	/// <summary>
	/// The current location point that this camera is either at or moving towards
	/// </summary>
	AOC2PointCameraLocation _loc;
	
	/// <summary>
	/// The amount of the distance between the camera's current position and ending position
	/// that the camera will move per step.
	/// </summary>
	const float LERP_TIME = .7f;
	
	void Awake()
	{
		_tran = transform;
	}
	
	/// <summary>
	/// Start this instance by fining the first closest location to the target
	/// </summary>
	void Start () {
	}
	
	void OnEnable()
	{
		AOC2EventManager.Cam.OnPlayerEnterCameraArea += OnPlayerEnterCameraZone;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Cam.OnPlayerEnterCameraArea -= OnPlayerEnterCameraZone;
	}
	
	/// <summary>
	/// Update this instance by moving to the current camera location
	/// and centering the camera on the target
	/// </summary>
	void Update () {
#if DEBUG
		_tran.position = _loc.cameraPoint.position;	
		//_tran.LookAt(target);
#endif
		
	}
	
	IEnumerator LerpToLoc(AOC2PointCameraLocation loc)
	{
		float currTime = 0f;
		Vector3 startPos = _tran.position;
		_loc = loc;
		target = _loc.hitArea;
		
		while(currTime < LERP_TIME)
		{
			currTime += Time.deltaTime;
			
			float timeDist = Mathf.Min(currTime / LERP_TIME, 1f);
			
			_tran.position = Vector3.Lerp(startPos, _loc.cameraPoint.position, timeDist);
			
			//_tran.LookAt(Vector3.Lerp(startTarg, target.position, timeDist));
			
			yield return null;
		}
		
	}
	
	void OnPlayerEnterCameraZone(AOC2PointCameraLocation loc)
	{
		StartCoroutine(LerpToLoc(loc));
	}
}
