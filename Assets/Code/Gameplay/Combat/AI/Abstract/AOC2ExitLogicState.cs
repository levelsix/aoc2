using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for tests to change state
/// in a state machine
/// </summary>
public abstract class AOC2ExitLogicState {
	
	/// <summary>
	/// Logic state to change to when this exit is
	/// triggered
	/// </summary>
	public AOC2LogicState state;
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	abstract public bool Test();
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitLogicState"/> class.
	/// </summary>
	/// <param name='_state'>
	/// State to change to when this exit is triggered
	/// </param>
	public AOC2ExitLogicState(AOC2LogicState _state)
	{
		state = _state;
	}
	
}