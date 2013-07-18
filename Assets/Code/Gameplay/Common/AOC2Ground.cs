using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// 
/// Ground component, which has a sprite.
/// Also, used as a check for whether a tap/click raycasts to the ground
/// 
/// TODO: Make a background system that hooks together multiple background
/// images
/// </summary>
[RequireComponent (typeof(AOC2Sprite))]
public class AOC2Ground : MonoBehaviour {
	
	/// <summary>
	/// The sprite component reference
	/// </summary>
	AOC2Sprite _sprite;
	
	/// <summary>
	/// Awake this instance.
	/// Get internal component references
	/// </summary>
	void Awake()
	{
		_sprite = GetComponent<AOC2Sprite>();
	}
	
	/// <summary>
	/// Start this instance.
	/// Makes the background mesh
	/// </summary>
	void Start ()
	{
		_sprite.MakeBackgroundMesh();
	}
}
