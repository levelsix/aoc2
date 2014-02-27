using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Exit state that returns true if another exit state returns false
/// </summary>
public class AOC2ExitNotOther : AOC2ExitLogicState {
	
	/// <summary>
	/// The exit state to check
	/// </summary>
	AOC2ExitLogicState _other;
    
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitNotOther"/> class.
	/// </summary>
	/// <param name='other'>
	/// Other state to check
	/// </param>
	/// <param name='state'>
	/// State to change to when this exit is true
	/// </param>
    public AOC2ExitNotOther(AOC2ExitLogicState other, AOC2LogicState state)
        :base(state)
    {
        _other = other;
    }
    
	/// <summary>
	/// Test this instance by testing the inverse of the other state
	/// </summary>
    public override bool Test()
    {
        return !_other.Test();
    }
    
}
