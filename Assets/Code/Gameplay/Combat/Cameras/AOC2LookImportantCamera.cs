using UnityEngine;
using System.Collections;

public class AOC2LookImportantCamera : MonoBehaviour {
	
	Camera cam;
	
	Transform trans;
	
	Vector3 lookPoint;
	
	Vector3 lastLookPoint;
	
	float lastFov;
	
	float fov;
	
	/// <summary>
	/// The amount of time between updates
	/// </summary>
	const float UPDATE_TIME = .5f;
	
	/// <summary>
	/// Extra degrees to add to the view angle to make sure that everythin
	/// is in frame
	/// </summary>
	const float CAM_ANGLE_BUFFER = 10f;
	
	const float MIN_FOV = 20f;
	
	const float FOV_STEP = .3f;
	
	const float MIN_FOV_DIFF = 3f;
	
	const float POINT_LOC_STEP = .3f;
	
	const float MIN_POINT_SQR_DIST = 4f;
	
	void Awake()
	{
		cam = camera;
		trans = transform;
	}
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(PeriodicUpdate());
	}
	
	void Update ()
	{
		if (fov != lastFov)
		{
			StepFOV();
		}
		if (lookPoint != lastLookPoint)
		{
			StepLookPoint();
		}
	}
	
	IEnumerator PeriodicUpdate()
	{
		while(true)
		{
			DetermineView();
			yield return null;
		}
	}
	
	void DetermineView()
	{
		AOC2CameraObject minX = null, minZ = null, maxX = null, maxZ = null;
		foreach (AOC2CameraObject item in AOC2ManagerReferences.cameraManager.camObjects) 
		{
			if (maxZ == null)
			{
				minX = minZ = maxX = maxZ = item;
			}
			else
			{
				if (item.trans.position.x < minX.trans.position.x)
				{
					minX = item;
				}
				else if (item.trans.position.x > maxX.trans.position.x)
				{
					maxX = item;
				}
				if (item.trans.position.z < minZ.trans.position.z)
				{
					minZ = item;
				}
				else if (item.trans.position.z > maxZ.trans.position.z)
				{
					maxZ = item;
				}
			}
		}
		
		float xDist = maxX.trans.position.x - minX.trans.position.x;
		float zDist = maxZ.trans.position.z - minZ.trans.position.z;
		
		float tempFOV;
		Vector3 centerPoint;
		if (xDist > zDist)
		{
			tempFOV = Vector3.Angle((minX.trans.position - trans.position).normalized, (maxX.trans.position - trans.position).normalized);
			centerPoint = (minX.trans.position + maxX.trans.position) / 2;
		}
		else
		{
			tempFOV = Vector3.Angle((minZ.trans.position - trans.position).normalized, (maxZ.trans.position - trans.position).normalized);
			centerPoint = (minZ.trans.position + maxZ.trans.position) / 2;
		}
		
		tempFOV = Mathf.Max(tempFOV + CAM_ANGLE_BUFFER, MIN_FOV);
		
		if (Mathf.Abs(fov - tempFOV) > MIN_FOV_DIFF)
		{
			fov = tempFOV;
		}
		
		if (Vector3.SqrMagnitude(lookPoint - centerPoint) > MIN_POINT_SQR_DIST)
		{
			lookPoint = centerPoint;
		}
		
		//StartCoroutine(LerpFOV(fov, centerPoint));
		
	}
	
	/*
	IEnumerator DoLerpOverTime(float endFOV, Vector3 endPoint)
	{
		float startFOV = cam.fieldOfView;
		float currTime = 0;
		float lerp;
		while (currTime < UPDATE_TIME)
		{
			currTime += Time.deltaTime;
			
			lerp = Mathf.Min(currTime/UPDATE_TIME, 1f);
			
			cam.fieldOfView = Mathf.Lerp(startFOV, endFOV, lerp);
			
			cam.transform.LookAt(Vector3.Lerp(lookPoint, endPoint, lerp));
			
			yield return null;
		}
		lookPoint = endPoint;
	}
	*/
	
	void SetFOV(float newFOV)
	{
		lastFov = fov;
		fov = newFOV;
	}
	
	void SetLookPoint(Vector3 newPoint)
	{
		lastLookPoint = lookPoint;
		lookPoint = newPoint;
	}
	
	void StepFOV()
	{
		if (Mathf.Abs (lastFov - fov) < FOV_STEP)
		{
			lastFov = fov;
		}
		else
		{
			if (fov < lastFov)
			{
				lastFov -= FOV_STEP;
			}
			else
			{
				lastFov += FOV_STEP;
			}
		}
		cam.fieldOfView = lastFov;
	}
	
	void StepLookPoint()
	{
		if (Vector3.SqrMagnitude(lookPoint - lastLookPoint) < POINT_LOC_STEP)
		{
			lastLookPoint = lookPoint;
		}
		else
		{
			Vector3 dir = (lookPoint - lastLookPoint).normalized;
			lastLookPoint += dir * POINT_LOC_STEP;
		}
		trans.LookAt(lastLookPoint);
	}
}
