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
	/// </summary>
	virtual public void Start(){}
	
	/// <summary>
	/// Logic to follow while in this state
	/// </summary>
	abstract protected IEnumerator Logic();
	
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


