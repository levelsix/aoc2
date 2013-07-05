using UnityEngine;
using System.Collections;

public class AOC2FollowCamera : MonoBehaviour {
	
	public Transform _trans;
	
	public AOC2Unit unit;
	
	void Awake()
	{
		_trans = transform;
	}
	
	void OnEnable()
	{
		if (unit != null)
		{
			LockCameraToUnit(unit);
		}
		AOC2EventManager.Combat.OnSpawnPlayer += LockCameraToUnit;
		AOC2EventManager.Combat.OnPlayerDeath += Unlock;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Combat.OnSpawnPlayer -= LockCameraToUnit;
		AOC2EventManager.Combat.OnPlayerDeath -= Unlock;
	}
	
	void Unlock(AOC2Unit unit){ Unlock(); }
	
	void Unlock()
	{
		_trans.parent = null;
	}
	
	protected void LockCameraToUnit(AOC2Unit unit)
	{
		//First, line up with the unit
		
		Vector3 gPos = AOC2ManagerReferences.gridManager.ScreenToGround(new Vector3(Screen.width/2, Screen.height/2));
		_trans.position += (unit.aPos.position - gPos);
		
		/*
		//Hit the camera's XZ plane by casting a ray from the
		//unit using the camera's rotation 
		Plane camXZ = new Plane(Vector3.down, _trans.position);
		Debug.Log(_trans.rotation.eulerAngles.normalized);
		Vector3 temp = new Vector3(-1, _trans.rotation.eulerAngles.x / _trans.rotation.eulerAngles.y, -1).normalized;
		Debug.Log(temp.x + ", " + temp.y);
		Ray ray = new Ray(unit.aPos.position, temp);
		float dist;
		if (camXZ.Raycast(ray, out dist))
		{
			Debug.Log("cast worked");
			_trans.position = ray.GetPoint(dist);
		}
		*/
		
		//Parent transforms, so that unit & camera
		//move together
		_trans.parent = unit.transform;
	}
	
}
