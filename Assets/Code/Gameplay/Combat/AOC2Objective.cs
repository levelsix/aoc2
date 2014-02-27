using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// @author Rob Giusti
/// A level objective, which the Compass UI element will guide
/// the player towards
/// </summary>
public class AOC2Objective : MonoBehaviour {
	
	/// <summary>
	/// The transform component
	/// </summary>
	public Transform trans;
	
	/// <summary>
	/// Event triggered when this objective is set as the
	/// current objective
	/// </summary>
	public Action OnSetObjective;
	
	/// <summary>
	/// The next objective after this one.
	/// If this is null, it is assumed that this is the final
	/// objective on the level
	/// </summary>
	public AOC2Objective next;
	
	/// <summary>
	/// Awake this instance.
	/// Register internal component references.
	/// </summary>
	void Awake()
	{
		trans = transform;
	}
	
	/// <summary>
	/// Complete this instance, triggering the gloabl Objective Complete event
	/// </summary>
	public void Complete()
	{
		AOC2EventManager.Combat.OnObjectiveComplete(this);
	}
	
}
