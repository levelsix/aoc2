using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Control Manager, which handles mouse and touch input.
/// For touch, maintains a dictionary of active touches which
/// it checks up on every step.
/// </summary>
public class AOC2ControlManager : MonoBehaviour 
{
    /// <summary>
    /// Constant that Unity uses for left-click
    /// </summary>
    private const int LEFT_MOUSE = 0;
	
	/// <summary>
	/// A dictionary of current touches, indexed by Touch.fingerId
	/// </summary>
	private Dictionary<int, AOC2TouchData> touches;
	
	/// <summary>
	/// The touch pile.
	/// A collection of touch datas that are no longer being used and can
	/// be recycled, to avoid allocating new data every touch.
	/// </summary>
	private List<AOC2TouchData> touchPile;
	
	/// <summary>
	/// The touch data for a mouse.
	/// </summary>
	private AOC2TouchData mouseData;
	
	/// <summary>
	/// The touch data for following a gesture involving multiple touches
	/// </summary>
	private AOC2TouchData multiData;
	
	/// <summary>
	/// The last mouse position.
	/// For calculating DeltaMouse
	/// </summary>
	private Vector3 _lastMouse;
	
	/// <summary>
	/// Gets the delta mouse.
	/// </summary>
	/// <value>
	/// The change in mouse position since the previous frame
	/// </value>
	public Vector3 DeltaMouse{
		get
		{
			return Input.mousePosition - _lastMouse;
		}
	}
	
	private AOC2TouchData recentTap;
	
	const float MULTITOUCH_LIFT_TIME = .2f;
	
	const float DOUBLE_TAP_TIME = .3f;
	
	const float DOULBE_TAP_DIST_SQR = 25f;
	
	/// <summary>
	/// Awake this instance.
	/// Set up the touch dictionary
	/// </summary>
	void Awake()
	{
		touches = new Dictionary<int, AOC2TouchData>();
		mouseData = new AOC2TouchData(Vector2.zero);
		
		AOC2ManagerReferences.controlManager = this;
	}
	
	/// <summary>
    /// Update is called once per frame
	/// </summary>
	void Update () 
	{
		ProcessMouse();
		//ProcessTouches();
	}
	
	/// <summary>
	/// Processes all current touches and .
	/// </summary>  
	private void ProcessTouches()
	{
		foreach (Touch touch in Input.touches) 
		{
			//Add new touches to the dictionary
			if (touch.phase == TouchPhase.Began)
			{
				touches[touch.fingerId] = new AOC2TouchData(touch.position);
			}
			
			//Remove all ended touches in the dictionary
			if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && touches.ContainsKey(touch.fingerId))
			{
				//Process tap/flick
				ProcessRelease(touches[touch.fingerId]);
				touches.Remove(touch.fingerId);
			}
			//If it's not an ending touch, update it and possibly process it as a hold
			else
			{
				touches[touch.fingerId].pos = touch.position;
				touches[touch.fingerId].delta = touch.deltaPosition;
				UpdateTouch(touches[touch.fingerId]);
			}
		}
		if (touches.Count > 0)
		{
			UpdateMultiTouch();
		}
	}

    /// <summary>
    /// Process mouse click for selection and movement
    /// </summary>
    private void ProcessMouse()
    {
        //Checks left-click
        if (Input.GetMouseButtonDown(LEFT_MOUSE))
        {
			mouseData.init(Input.mousePosition);
        }
        else if (Input.GetMouseButton(LEFT_MOUSE))
        {
			mouseData.delta = DeltaMouse;
            mouseData.pos = Input.mousePosition;
			UpdateTouch(mouseData);
        }
        else if (Input.GetMouseButtonUp(LEFT_MOUSE))
        {
            ProcessRelease(mouseData);
        }
		_lastMouse = Input.mousePosition;
    }
	
	/// <summary>
	/// Processes a single hold.
	/// </summary>
	/// <param name='touch'>
	/// The touch data of the hold
	/// </param>
	private void ProcessHold(AOC2TouchData touch)
	{
		if (!touch.stationary)
		{
			if (AOC2EventManager.Controls.OnKeepDrag != null){
				AOC2EventManager.Controls.OnKeepDrag(touch);
			}
		}
		else
		{
			if (AOC2EventManager.Controls.OnKeepHold != null){
				AOC2EventManager.Controls.OnKeepHold(touch);
			}
		}
	}
	
	/// <summary>
	/// Processes the release of a touch. If it's a tap of flick, this
	/// is when the event is called
	/// </summary>
	/// <param name='touch'>
	/// Touch being released.
	/// </param>
	private void ProcessRelease(AOC2TouchData touch)
	{
		if (touch.phase == AOC2TouchData.Phase.TAP)
		{
			if (!touch.stationary)
			{
				if (AOC2EventManager.Controls.OnFlick != null){
					AOC2EventManager.Controls.OnFlick(touch);
				}
			}
			else
			{
				//Try to double-tap
				if (CheckDoubleTap(touch) && AOC2EventManager.Controls.OnDoubleTap != null)
				{
					AOC2EventManager.Controls.OnDoubleTap(touch);
				}
				//Tap
				else if (AOC2EventManager.Controls.OnTap != null)
				{
					AOC2EventManager.Controls.OnTap(touch);
				}
				StartCoroutine(HoldTap(touch));
			}
		}
		else
		{
			if (!touch.stationary)
			{
				if (AOC2EventManager.Controls.OnReleaseDrag != null){
					AOC2EventManager.Controls.OnReleaseDrag(touch);
				}
			}
			else
			{
				if (AOC2EventManager.Controls.OnReleaseHold != null){
					AOC2EventManager.Controls.OnReleaseHold(touch);
				}
			}
		}		
	}
	
	/// <summary>
	/// Updates a touch.
	/// </summary>
	/// <param name='touch'>
	/// Touch to be updated
	/// </param>
	private void UpdateTouch(AOC2TouchData touch)
	{
		if (touch.phase == AOC2TouchData.Phase.TAP)
		{
			touch.Update(Time.deltaTime);
			if (touch.phase == AOC2TouchData.Phase.HOLD)
			{
				if (touch.stationary)
				{
					//Separate 'if' for clarity
					if (AOC2EventManager.Controls.OnStartHold != null)
					{
						AOC2EventManager.Controls.OnStartHold(touch);
					}
				}
				ProcessHold(touch);
			}
		}
		else
		{
			touch.Update(Time.deltaTime);
			ProcessHold(touch);	
		}
	}
	
	IEnumerator HoldTap(AOC2TouchData data)
	{
		recentTap = data;
		yield return new WaitForSeconds(DOUBLE_TAP_TIME);
		if (recentTap == data)
		{
			recentTap = null;
		}
	}
	
	private bool CheckDoubleTap(AOC2TouchData data)
	{
		return recentTap != null && (data.pos - recentTap.pos).sqrMagnitude < DOULBE_TAP_DIST_SQR;
	}
	
	/// <summary>
	/// If we have multiple touches on the screen, process
	/// them differently
	/// </summary>
	private void UpdateMultiTouch()
	{
		if (multiData == null)
		{
			
		}
		else
		{
			
		}
	}
}
