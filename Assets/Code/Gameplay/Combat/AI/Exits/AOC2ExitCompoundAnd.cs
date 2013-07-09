using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Exit state that holds multiple other exit states
/// Exits only when all states are fulfilled
/// </summary>
public class AOC2ExitCompoundAnd : AOC2ExitLogicState {
	
	/// <summary>
	/// The tests.
	/// </summary>
	AOC2ExitLogicState[] _tests;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ExitCompoundAnd"/> class.
	/// </summary>
	/// <param name='state'>
	/// State.
	/// </param>
	/// <param name='tests'>
	/// Tests.
	/// </param>
	public AOC2ExitCompoundAnd(AOC2LogicState state, AOC2ExitLogicState[] tests)
		: base(state)
	{
		_tests = tests;
	}
	
	/// <summary>
	/// Test this instance.
	/// </summary>
	public override bool Test ()
	{
		for (int i = 0; i < _tests.Length; i++) 
		{
			if (!_tests[i].Test())
			{
				return false;
			}
		}
		return true;
	}
}
