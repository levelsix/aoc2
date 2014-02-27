using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Exit state that checks if the user has a current target
/// </summary>
public class AOC2ExitPlayerHasTarget : AOC2ExitLogicState {
	
	/// <summary>
	/// The user
	/// </summary>
    AOC2Unit _user;
    
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitPlayerHasTarget"/> class.
	/// </summary>
	/// <param name='user'>
	/// User.
	/// </param>
	/// <param name='state'>
	/// State to change to when testing true
	/// </param>
    public AOC2ExitPlayerHasTarget(AOC2Unit user, AOC2LogicState state)
        : base(state)
    {
        _user = user;
    }
    
	/// <summary>
	/// Test this instance by checking if the user's target unit reference is null
	/// </summary>
    public override bool Test ()
    {
        if (_user.targetUnit != null)
        {
            _user.targetPos = _user.targetUnit.aPos;
            return true;
        }
        return false;
    }
    
}
