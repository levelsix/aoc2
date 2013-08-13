using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2CameraManager : MonoBehaviour {

	public List<AOC2CameraObject> camObjects;
	
	void Awake()
	{
		AOC2ManagerReferences.cameraManager = this;
	}
	
	void OnEnable()
	{
		AOC2EventManager.Cam.OnEnableCameraObject += AddCameraObject;
		AOC2EventManager.Cam.OnDisableCameraObject += RemoveCameraObject;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Cam.OnEnableCameraObject -= AddCameraObject;
		AOC2EventManager.Cam.OnDisableCameraObject -= RemoveCameraObject;
	}
	
	void AddCameraObject(AOC2CameraObject obj)
	{
		camObjects.Add(obj);
	}
	
	void RemoveCameraObject(AOC2CameraObject obj)
	{
		camObjects.Remove(obj);
	}
}
