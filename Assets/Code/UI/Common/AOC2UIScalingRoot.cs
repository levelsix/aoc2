using UnityEngine;
using System.Collections;

public class AOC2UIScalingRoot : MonoBehaviour {
	
	/// <summary>
	/// The NGUI root.
	/// </summary>
	UIRoot nguiRoot;
	
	[SerializeField]
	float baseHeight;
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		nguiRoot = GetComponent<UIRoot>();
	}
	
	void OnEnable()
	{
		AOC2EventManager.UI.OnCameraResize += OnCameraSizeChange;
	}
	
	void OnDisable()
	{
		AOC2EventManager.UI.OnCameraResize -= OnCameraSizeChange;
	}
	
	/// <summary>
	/// Whenever the camera changes size, manually resize the size of the ngui root
	/// In order to keep everything displaying at scale
	/// </summary>
	/// <param name='cam'>
	/// Main camera
	/// </param>
	void OnCameraSizeChange(Camera cam)
	{
		nguiRoot.manualHeight = (baseHeight / cam.orthographicSize);
	}
	
}
