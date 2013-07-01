using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AOC2Unit))]
public class AOC2PlayerController : MonoBehaviour {
	
	public float MIN_MOVE_DIST_SQR = .2f;
	
	private AOC2Position target;
	
	private Transform _trans;
	
	AOC2Unit _unit;
	
	// Use this for initialization
	void Awake () {
		_unit = GetComponent<AOC2Unit>();
		_trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		MoveTowardsTarget();
		
		if (Input.GetKey(KeyCode.W))
		{
			_unit.Move(new Vector3(1,0,1));
		}
		else if (Input.GetKey (KeyCode.S))
		{
			_unit.Move(new Vector3(-1,0,-1));
		}
		if (Input.GetKey(KeyCode.A))
		{
			_unit.Move(new Vector3(-1,0,1));
		}
		else if (Input.GetKey(KeyCode.D))
		{
			_unit.Move(new Vector3(1,0,-1));
		}
	}
	
	void OnEnable()
	{
		AOC2EventManager.Controls.OnTap += OnTap;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Controls.OnTap -= OnTap;
	}
	
	void MoveTowardsTarget()
	{
		if (target != null)
		{
			_unit.Move((target.position - _trans.position).normalized);
			if (AOC2Math.GroundDistanceSqr(target.position, _trans.position) < MIN_MOVE_DIST_SQR)
			{
				target = null;
			}
		}
	}
	
	void OnTap(AOC2TouchData data)
	{
		target = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos));
	}
}
