using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class AOC2Unit : MonoBehaviour {
	
	#region Members
	
	#region Public
	
	public float maxHealth = 1;
	
	public float moveSpeed = 1;
	
	public Color tint = Color.white;
	
	#endregion
	
	#region Private
	
	private float _health = 1;
	
	private Vector3 _prevPos;
	
	BoxCollider _box;
	
	Transform _trans;
	
	#endregion
	
	#region Constant
	
	private static readonly Plane GROUND_PLANE = new Plane(Vector3.up, Vector3.zero);
	
	#endregion
	
	#endregion
	
	#region Functions
	
	#region Initialization
	
	void Awake()
	{
		_trans = transform;
		_box = collider as BoxCollider;
	}
	
	void Start()
	{
		Init();
		_prevPos = _trans.position;
		DebugTint(tint);
	}
	
	public void Init()
	{
		
	}
	
	private void DebugTint(Color color)
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Color[] colorArr = new Color[mesh.vertices.Length];
		for (int i = 0; i < mesh.vertices.Length; i++) {
			colorArr[i] = color;
		}
		mesh.colors = colorArr;
	}
	
	#endregion
	
	#region Death Logic
	
	public void TakeDamage(AOC2Delivery deliv)
	{
		_health -= deliv.damage;
	}
	
	#endregion
	
	#region Movement
	
	public void Move(Vector3 direction)
	{
		
		if (direction != direction.normalized)
		{
			//Debug.LogWarning("Normalize vectors before passing them into Move");
			direction.Normalize();
		}
		
		_trans.position += direction * moveSpeed * Time.deltaTime;
	}
	
	#endregion
	
	#region Collision
	

	
	#endregion
	
	#endregion
}
