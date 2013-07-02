using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AOC2Sprite))]
public class AOC2Ground : MonoBehaviour {
	
	AOC2Sprite _sprite;
	
	void Awake()
	{
		_sprite = GetComponent<AOC2Sprite>();
	}
	
	// Use this for initialization
	void Start ()
	{
		_sprite.MakeBackgroundMesh();
	}
}
