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
	/// The possible locations for this camera in the scene.
	/// All locations should add themselves to this list on enable
	/// and remove them on disable.
	/// DEBUG: If we go with this camera style, a lot of this logic
	/// should be moved outside of these components and into a gloabl
	/// camera manager
	/// </summary>
	public static List<AOC2PointCameraLocation> locs = new List<AOC2PointCameraLocation>();
	
	/// <summary>
	/// The target that this camera will be following.
	/// Needs to be set up in the editor before the scene starts.
	/// </summary>
	[SerializeField]
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
	const float LERP_STEP = .03f;
	
	void Awake()
	{
		_tran = transform;
	}
	
	/// <summary>
	/// Start this instance by fining the first closest location to the target
	/// </summary>
	void Start () {
		_loc = GetClosestLoc(target.position);
	}
	
	/// <summary>
	/// Update this instance by moving to the current camera location
	/// and centering the camera on the target
	/// </summary>
	void Update () {
		
		//Check if we need to change location
		if (!_loc.InRange(target.position))
		{
			_loc = GetClosestLoc(target.position);
		}
		
		//Lerp camera to current location
		_tran.position = Vector3.Lerp(_tran.position, _loc.trans.position, LERP_STEP);
		
		//Point camera at target
		_tran.forward = (target.position - _tran.position).normalized;
	}
	
	/// <summary>
	/// Gets the closest camera location to the target
	/// </summary>
	/// <returns>
	/// The closest location, which the camera should set as its current
	/// location and move towards
	/// </returns>
	/// <param name='toPoint'>
	/// The position of the camera target to check against location positions
	/// </param>
	AOC2PointCameraLocation GetClosestLoc(Vector3 toPoint)
	{
		AOC2PointCameraLocation closest = locs[0];
		float closeDist = closest.GroundDistSqr(toPoint);
		float dist;
		foreach (AOC2PointCameraLocation item in locs) {
			dist = item.GroundDistSqr(toPoint);
			if (dist < closeDist)
			{
				closeDist = dist;
				closest = item;
			}
		}
		return closest;
	}
}
