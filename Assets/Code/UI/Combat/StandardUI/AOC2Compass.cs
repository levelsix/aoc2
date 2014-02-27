using UnityEngine;
using System.Collections;

/// <summary>
/// UI element that points towards the next Objective
/// </summary>
public class AOC2Compass : MonoBehaviour {
	
	/// <summary>
	/// The angle on the last frame.
	/// Used to lerp between targets.
	/// </summary>
	float lastAngle;
	
	/// <summary>
	/// The transform
	/// </summary>
	Transform trans;
	
	/// <summary>
	/// SET IN INSPECTOR
	/// TODO: Set Programatically
	/// The transform of the player
	/// </summary>
	public Transform player;
	
	/// <summary>
	/// Pointer to the main camera's transform
	/// </summary>
	Transform cam;
	
	/// <summary>
	/// The maximum angles that the compass can turn in one frame
	/// </summary>
	const float MAX_RAD_STEP = .1f;
	
	/// <summary>
	/// Awake this instance.
	/// Set up component references
	/// </summary>
	void Awake () 
	{
		trans = transform;
		cam = Camera.main.transform;
		
		lastAngle = 0;
	}
	
	/// <summary>
	/// Reorient the arrow to point towards the current objective
	/// </summary>
	void Update () 
	{
		if (AOC2ManagerReferences.combatManager.currObjective != null)
		{
			float angle = Mathf.Atan2(player.position.z - AOC2ManagerReferences.combatManager.currObjective.trans.position.z,
				player.position.x - AOC2ManagerReferences.combatManager.currObjective.trans.position.x);
			
			angle += cam.rotation.y;
			
			if (Mathf.Abs(angle - lastAngle) > 180 * Mathf.Deg2Rad)
			{
				if (angle > lastAngle)
				{
					lastAngle += 360 * Mathf.Deg2Rad;
				}
				else
				{
					lastAngle -= 360 * Mathf.Deg2Rad;
				}
			}
			
			if (Mathf.Abs(angle - lastAngle) > MAX_RAD_STEP)
			{
				if (angle > lastAngle)
				{
					angle = lastAngle + MAX_RAD_STEP;
				}
				else
				{
					angle = lastAngle - MAX_RAD_STEP;
				}
			}
			
			trans.up = new Vector3(Mathf.Cos (angle) * Mathf.Rad2Deg, Mathf.Sin(angle) * Mathf.Rad2Deg, 0);
			
			trans.Rotate(cam.rotation.x, 0, 0);
			
			lastAngle = angle;
		}
	}
}
