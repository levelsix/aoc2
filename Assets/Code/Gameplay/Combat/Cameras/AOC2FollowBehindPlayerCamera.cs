using UnityEngine;
using System.Collections;

public class AOC2FollowBehindPlayerCamera : MonoBehaviour {

	[SerializeField]
	Transform _target;
	
	[SerializeField]
	float _height;
	
	[SerializeField]
	float _followDist;
	
	Transform _trans;
	
	[SerializeField]
	float LERP_STEP = .1f;
	
	void Awake()
	{
		_trans = transform;
	}
	
	void Update()
	{
		if (_target != null)
		{
			Vector3 backDir = _target.forward * -1;
			Vector3 pos = _target.position + backDir * _followDist;
			pos.y = _height;
			_trans.position = Vector3.Slerp(_trans.position, pos, LERP_STEP);
			_trans.forward = (_target.position - _trans.position).normalized;
		}
	}
}
