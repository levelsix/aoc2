using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Component that allows an object to follow a finite
/// state machine constructed out of logic states
/// </summary>
public abstract class AOC2UnitLogic : MonoBehaviour {
	
	/// <summary>
	/// The heirarchical FSM at the top of this unit's logic
	/// </summary>
	public AOC2HFSMLogic logic;
	
	/// <summary>
	/// Sets the base logic state and starts
	/// the state machine
	/// Gets called by the Unit component on Init
	/// </summary>
	virtual public void Init()
	{
		StartCoroutine(logic.Logic());
	}
	
	/// <summary>
	/// Sets the current logic to a different state, initializing that state.
	/// Checks if the current state can be changed to that state.
	/// </summary>
	/// <param name='state'>
	/// State to change current logic to
	/// </param>
	virtual public void SetLogic(AOC2LogicState state)
	{
		if (logic.current == null)
		{
			state.Init();
			logic.current = state;
		}
		else if (logic.current.canBeInterrupt || state.priority > logic.priority)
		{
			logic.current.OnExitState();
			state.Init();
			logic.current = state;
		}
	}

}
