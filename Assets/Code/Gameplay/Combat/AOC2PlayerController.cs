using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AOC2Unit))]
public class AOC2PlayerController : MonoBehaviour {
	
	public float MIN_MOVE_DIST_SQR = .2f;
	
	public AOC2Attack baseAttack;
	
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
	
	/// <summary>
	/// Raises the tap event.
	/// Attempts to attack an enemy or move
	/// </summary>
	/// <param name='data'>
	/// Touch data.
	/// </param>
	void OnTap(AOC2TouchData data)
	{
		AOC2Unit enemyTarget = TryTargetEnemy(data.pos);
		if (enemyTarget != null)
		{
			AOC2Delivery deliv = baseAttack.Use(_trans.position, (enemyTarget.transform.position - _trans.position).normalized);
			deliv.gameObject.layer = AOC2Values.Layers.TARGET_ENEMY;
		}
		else
		{
			target = new AOC2Position(AOC2ManagerReferences.gridManager.ScreenToGround(data.pos));
		}
	}
	
	AOC2Unit TryTargetEnemy(Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit hit;
		LayerMask mask = (int)Mathf.Pow(2,AOC2Values.Layers.ENEMY);
		//Debug.Log(LayerMask.LayerToName(AOC2Values.Layers.ENEMY));
        if (Physics.Raycast(ray, out hit, mask))
		{
			return hit.collider.GetComponent<AOC2Unit>();
		}
		return null;
	}
}
