using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Component that allows an object to follow a finite
/// state machine constructed out of logic states
/// </summary>
public abstract class AOC2UnitLogic : MonoBehaviour {
	
	/// <summary>
	/// The current logic state
	/// </summary>
	protected AOC2LogicState _current;
	
	/// <summary>
	/// The base logic state that this unit
	/// will always start out in
	/// </summary>
	protected AOC2LogicState _baseState;
	
	/// <summary>
	/// Use to set up state machine graph and
	/// assign the base state
	/// </summary>
	abstract protected void Start ();
	
	/// <summary>
	/// Sets the base logic state and starts
	/// the state machine
	/// </summary>
	public void Init()
	{
		_current = _baseState;
		//_current.Start();
		
		StartCoroutine(RunLogic());
	}
	
	/// <summary>
	/// Runs the logic.
	/// </summary>
	/// <returns>
	/// The logic.
	/// </returns>
	protected IEnumerator RunLogic () 
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
					_current.Start();
					
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
	
}
