using UnityEngine;
using System.Collections;

public class AOC2DebugPointAtPlayer : MonoBehaviour {
	
	[SerializeField]
	Transform player;
	
	Transform _trans;
	
	void Awake()
	{
		_trans = transform;
	}
	
	void Update()
	{
		_trans.forward = (player.position - _trans.position).normalized;
	}
	
}
