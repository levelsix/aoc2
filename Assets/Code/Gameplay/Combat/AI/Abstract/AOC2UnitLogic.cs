using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Component that allows an object to follow a finite
/// state machine constructed out of logic states
/// </summary>
public abstract class AOC2UnitLogic : MonoBehaviour {
	
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
	
	/*
	/// <summary>
	/// Runs the logic.
	/// </summary>
	/// <returns>
	/// The logic.
	/// </returns>
	virtual public IEnumerator Logic() 
	{
		while(true)
		{
			if (_current != null)
			{
				//First, check for exits
				AOC2LogicState change = _current.GetExit();
				while (change != null)
				{
					//Set up the returned state for the next frame
					_current = change;
					_current.Init();
					
					//Keep checking if we need to immediately change
					//state
					change = _current.GetExit();
				}
				
				if (_current.logic.MoveNext())
				{
					//Then, do current state logic
					object step = _current.logic.Current;
				
					//Yield whatever wait the current state had
					yield return step;
				}
				else
				{
					yield return null;
				}
			}
			else
			{
				//Fallback: return to base state if state is lost
				_current = _baseState;
				yield return null;
			}
		}
	}
	*/
}
