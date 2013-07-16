using UnityEngine;
using System.Collections;

/// <summary>
/// Exits when the referrenced logic state is completed
/// </summary>
public class AOC2ExitWhenComplete : AOC2ExitLogicState {

	/// <summary>
	/// The unit running this logic.
	/// Set at constructor.
	/// </summary>
	private readonly AOC2LogicState checkState;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicState"/> class.
	/// </summary>
	/// <param name='runningState'>
	/// State to check if complete
	/// </para>
	/// <param name='exitState'>
	/// State to change to when exit conditions are met
	/// </param>
	public AOC2ExitWhenComplete(AOC2LogicState runningState, AOC2LogicState exitState) 
		: base(exitState)
	{
		checkState = runningState;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		return checkState.complete;
	}
	
	
}
