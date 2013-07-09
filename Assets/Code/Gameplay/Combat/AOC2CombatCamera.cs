using UnityEngine;
using System.Collections;

public class AOC2CombatCamera : AOC2BuildingCamera {
	
	/// <summary>
	/// The unit that the camera is locked to.
	/// </summary>
	public AOC2Unit lockUnit;
	
	override protected void OnEnable()
	{
		if (lockUnit != null)
		{
			LockCameraToUnit(lockUnit);
		}
		AOC2EventManager.Combat.OnSpawnPlayer += LockCameraToUnit;
		AOC2EventManager.Combat.OnPlayerDeath += Unlock;
		AOC2EventManager.Controls.OnKeepDrag += MoveRelative;
		base.OnEnable();
	}
	
	override protected void OnDisable()
	{
		AOC2EventManager.Combat.OnSpawnPlayer -= LockCameraToUnit;
		AOC2EventManager.Combat.OnPlayerDeath -= Unlock;
		AOC2EventManager.Controls.OnKeepDrag -= MoveRelative;
		base.OnDisable();
	}
	
	void Unlock(AOC2Unit unit)
	{ 
		Unlock(); 
	}
	
	void Unlock()
	{
		_trans.parent = null;
		lockUnit = null;
	}
	
	public override void MoveRelative (AOC2TouchData touch)
	{
		if (lockUnit == null)
		{
			base.MoveRelative (touch);
		}
	}
	
	protected void LockCameraToUnit(AOC2Unit unit)
	{
		lockUnit = unit;
		
		//First, line up with the unit
		Vector3 gPos = AOC2ManagerReferences.gridManager.ScreenToGround(new Vector3(Screen.width/2, Screen.height/2));
		_trans.position += (unit.aPos.position - gPos);
		
		//Parent transforms, so that unit & camera
		//move together
		_trans.parent = unit.transform;
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
	}
	
}
