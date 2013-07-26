using UnityEngine;
using System.Collections;

public class AOC2CombatCamera : AOC2BuildingCamera {
	
	/// <summary>
	/// The unit that the camera is locked to.
	/// </summary>
	public AOC2Unit lockUnit;
    
    public bool locked = false;
    
    public float maxRotate = 90;
    
    public float minRotate = 20;
	
	override protected void OnEnable()
	{
		if (lockUnit != null)
		{
			LockCameraToUnit(lockUnit);
		}
		AOC2EventManager.Combat.OnSpawnPlayer += LockCameraToUnit;
		AOC2EventManager.Combat.OnPlayerDeath += Unlock;
		AOC2EventManager.Controls.OnKeepDrag[0] += MoveRelative;
        AOC2EventManager.UI.OnCameraLockButton += OnLockCameraButton;
        AOC2EventManager.UI.OnCameraSnapButton += OnSnapCameraButton;
        AOC2EventManager.Controls.OnKeepDrag[2] += OnMultiDrag3;
		base.OnEnable();
	}
	
	override protected void OnDisable()
	{
		AOC2EventManager.Combat.OnSpawnPlayer -= LockCameraToUnit;
		AOC2EventManager.Combat.OnPlayerDeath -= Unlock;
		AOC2EventManager.Controls.OnKeepDrag[0] -= MoveRelative;
        AOC2EventManager.UI.OnCameraLockButton -= OnLockCameraButton;
        AOC2EventManager.UI.OnCameraSnapButton -= OnSnapCameraButton;
        AOC2EventManager.Controls.OnKeepDrag[2] -= OnMultiDrag3;
		base.OnDisable();
	}
	
	void Unlock(AOC2Unit unit)
	{ 
		Unlock(); 
	}
	
	void Unlock()
	{
		_trans.parent = null;
        locked = false;
	}
	
    /*
	public override void MoveRelative (AOC2TouchData touch)
	{
		if (!locked)
		{
			base.MoveRelative (touch);
		}
	}
    */
    
    public void OnMultiDrag3(AOC2TouchData touch)
    {
        ChangeAngle(touch.delta.y);
    }
    
    public void ChangeAngle(float degrees)
    {
        _trans.Rotate(degrees, 0, 0);
    }
	
	protected void LockCameraToUnit(AOC2Unit unit)
	{
		lockUnit = unit;
		
        locked = true;
        
		//First, line up with the unit
		SnapToUnit(unit);
        
		//Parent transforms, so that unit & camera
		//move together
		_trans.parent = unit.transform;
	}
    
    void SnapToUnit(AOC2Unit unit)
    {
        Vector3 gPos = AOC2ManagerReferences.gridManager.ScreenToGround(new Vector3(Screen.width/2, Screen.height/2));
        _trans.position += (unit.aPos.position - gPos);
    }
    
    void OnLockCameraButton()
    {
        if (locked)
        {
            Unlock();   
        }
        else if (lockUnit != null)
        {
            LockCameraToUnit(lockUnit);
        }
    }
    
    void OnSnapCameraButton()
    {
        if (lockUnit != null)
        {
            SnapToUnit(lockUnit);    
        }
    }
	
	/// <summary>
	/// Cheats.
	/// </summary>
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Unlock();
		}
		if (Input.GetKey(KeyCode.K))
		{
			Zoom(-5f);
		}
        if (Input.GetKey(KeyCode.A))
        {
            ChangeAngle(1f);    
        }
	}
	
}
