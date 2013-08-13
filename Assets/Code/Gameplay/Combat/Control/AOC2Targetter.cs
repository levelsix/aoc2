using UnityEngine;
using System.Collections;

public class AOC2Targetter : MonoBehaviour {

	public Transform trans;
	
	public Collider coll;
	
	public AOC2LocalPlayerController localPlayerController;
	
	bool targetMode;
	
	enum TargetMode 
	{
		UNDETERMINED,
		GROUND,
		ENEMY
	}
	
	Camera cam;
	Transform camTrans;
	
	void Awake()
	{
		trans = transform;
		coll = collider;
	}
	
	void Start()
	{
		cam = Camera.main;
		camTrans = cam.transform;
	}
	
	void OnEnable()
	{
		AOC2EventManager.Controls.OnKeepDrag[0] += OnKeepDrag;
		AOC2EventManager.Controls.OnStartDrag[0] += OnStartDrag;
		AOC2EventManager.Controls.OnReleaseDrag[0] += OnReleaseDrag;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Controls.OnKeepDrag[0] -= OnKeepDrag;
		AOC2EventManager.Controls.OnStartDrag[0] -= OnStartDrag;
		AOC2EventManager.Controls.OnReleaseDrag[0] -= OnReleaseDrag;
	}
	
	void OnKeepDrag(AOC2TouchData data)
	{
		trans.position = cam.ScreenToWorldPoint(data.pos);
		trans.up = cam.ScreenPointToRay(data.pos).direction;
		
		if (!targetMode)
		{
			localPlayerController.SetGroundTarget(data.pos);
		}
	}
	
	void OnStartDrag(AOC2TouchData data)
	{
		coll.enabled = true;
		targetMode = false;
	}
	
	void OnReleaseDrag(AOC2TouchData data)
	{
		coll.enabled = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		AOC2Unit target = other.GetComponent<AOC2Unit>();
		if (target != null)
		{
			targetMode = true;
			localPlayerController.EnqueueTarget(target);
		}
	}
}
