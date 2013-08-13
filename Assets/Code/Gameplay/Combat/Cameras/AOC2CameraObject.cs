using UnityEngine;
using System.Collections;

public class AOC2CameraObject : MonoBehaviour {
	
	public Transform trans;
	
	void Awake()
	{
		trans = transform;
	}
	
	void OnEnable()
	{
		if (AOC2EventManager.Cam.OnEnableCameraObject != null)
		{
			AOC2EventManager.Cam.OnEnableCameraObject(this);
		}
	}
	
	void OnDisable()
	{
		if (AOC2EventManager.Cam.OnDisableCameraObject != null)
		{
			AOC2EventManager.Cam.OnDisableCameraObject(this);
		}
	}
}
