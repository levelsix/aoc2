using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Abstract class for state machine logic
/// </summary>
public abstract class AOC2LogicState {
	
	/// <summary>
	/// The pointer to the logic for this state
	/// </summary>
	public IEnumerator logic;
	
	/// <summary>
	/// The complete flag.
	/// Reset in Start()
	/// </summary>
	protected bool _complete;
	
	virtual public bool Complete
	{
		get
		{
			return _complete;
		}
	}
	
	/// <summary>
	/// The list of exit conditions for this state
	/// </summary>
	List<AOC2ExitLogicState> exits;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicState"/> class.
	/// Sets up the logic pointer and exit list.
	/// </summary>
	public AOC2LogicState()
	{
		exits = new List<AOC2ExitLogicState>();
		logic = Logic();
	}
	
	/// <summary>
	/// Start this logic state.
	/// Resets the complete flag
	/// and sets the logic pointer back to the start 
	/// of the Logic function
	/// </summary>
	virtual public void Init(){
		_complete = false;
		logic = Logic();
	}
	
	/// <summary>
	/// Any clean-up that needs to be done when we leave this state
	/// </summary>
	virtual public void OnExitState() {}
	
	/// <summary>
	/// Logic to follow while in this state
	/// </summary>
	virtual public IEnumerator Logic()
	{
		yield return null;
	}
	
	/// <summary>
	/// Checks all exit states, returns the
	/// new state if there's a change.
	/// </summary>
	/// <returns>
	/// The exit state, or null if we're to stay
	/// in this state
	/// </returns>
	public AOC2LogicState GetExit()
	{
		for (int i = 0; i < exits.Count; i++) 
		{
			if (exits[i].Test())
			{
				return exits[i].state;
			}
		}
		
		return null;
	}
	
	/// <summary>
	/// Adds an exit condition to this state.
	/// </summary>
	/// <param name='exit'>
	/// Exit condition
	/// </param>
	public void AddExit(AOC2ExitLogicState exit)
	{
		exits.Add(exit);
	}
	
}


