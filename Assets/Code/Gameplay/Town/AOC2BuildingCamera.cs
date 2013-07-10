using UnityEngine;
using System.Collections;

/// <summary>
/// Camera script for the town section.
/// No logic for locking to target.
/// Move Relative is controlled by AOC2BuildingManager, in order to delineate when
/// a drag should affect buildings rather than the camera.
/// </summary>
public class AOC2BuildingCamera : MonoBehaviour, AOC2Placeable
{
	/// <summary>
	/// The smallest orthographic size of the camera.
	/// Once this value is decided, we can make it a constant
	/// </summary>
	public float MIN_SIZE = 3f;
	
	/// <summary>
	/// The largest orthographic size of the camera.
	/// Once this value is decided, we can make it a constant
	/// </summary>
	public float MAX_SIZE = 10f;
	
	/// <summary>
	/// The coefficient of the drag, in order to make touch drag
	/// smooth
	/// </summary>
	public float DRAG_COEFF = 2f;
	
	/// <summary>
	/// Small coefficient applied to X movement to adjust
	/// to the fact that the screen is wider than it is tall
	/// </summary>
	public float X_DRAG_FUDGE = 1.1f;
	
	/// <summary>
	/// Constant coefficient to slow down camera scaling on zoom
	/// </summary>
	const float CAMERA_ZOOM_SCALE = .2f;
	
    /// <summary>
    /// This camera's transform, 
    /// </summary>
    protected Transform _trans;
	
	/// <summary>
	/// The camera that this script controls
	/// </summary>
	protected Camera _cam;
	
	/// <summary>
	/// Awake this instance.
	/// Get the local components that this camera will reference
	/// </summary>
	virtual public void Awake()
	{
		_trans = transform;
		_cam = camera;
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Set up delegtes
	/// </summary>
	virtual protected void OnEnable()
	{
		AOC2EventManager.Controls.OnPinch += Zoom;
	}
	
	/// <summary>
	/// Raises the diable event.
	/// Set up delegates
	/// </summary>
	virtual protected void OnDisable()
	{
		AOC2EventManager.Controls.OnPinch -= Zoom;
	}
	
	/// <summary>
	/// Moves the camera according to touch movement
	/// </summary>
	/// <param name='touch'>
	/// Touch.
	/// </param>
	virtual public void MoveRelative(AOC2TouchData touch)
	{
		Vector3 movement = touch.delta;
		
        //Turn the mouse difference in screen coordinates to world coordinates
        movement.y *= DRAG_COEFF * (Camera.main.orthographicSize / Screen.height);
        movement.x *= DRAG_COEFF * (Camera.main.orthographicSize / Screen.width) * X_DRAG_FUDGE;

        //Turn the 2D coordinates into our tilted isometric coordinates
        movement.z = movement.y - movement.x;
        movement.x = movement.x + movement.y;
        movement.y = 0;
		
		movement *= -1;

        //Add the difference to the original position, since we only hold original mouse pos
        _trans.position += movement;
	}
	
	/// <summary>
	/// Zoom the camera by the specified amount.
	/// </summary>
	/// <param name='amount'>
	/// Amount to zoome the camera in or out by.
	/// A positive number zooms in, while a negative number
	/// zooms out.
	/// </param>
	public void Zoom(float amount)
	{
		_cam.orthographicSize += amount * Time.deltaTime * CAMERA_ZOOM_SCALE;
		if (_cam.orthographicSize > MAX_SIZE)
		{
			_cam.orthographicSize = MAX_SIZE;
		}
		else if (_cam.orthographicSize < MIN_SIZE)
		{
			_cam.orthographicSize = MIN_SIZE;
		}
	}
	
#if UNITY_EDITOR
	/// <summary>
	/// Update this instance.
	/// Cheats to zoom the camera in/out when on computer
	/// </summary>
	void Update()
	{
		if (Input.GetKey(KeyCode.K))
		{
			Zoom(2);
		}
		if (Input.GetKey(KeyCode.I))
		{
			Zoom (-2);
		}
	}
#endif
	
}