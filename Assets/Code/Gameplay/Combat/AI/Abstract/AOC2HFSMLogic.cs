using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// A hierarchical finite logic state.
/// Contains a chain of Logic States that form a Finite State Machine.
/// Layer of abstraction between UnitLogic and LogicState. HFSM's can point
/// to other HFSM's in addition to LogicStates
/// </summary>
public class AOC2HFSMLogic : AOC2LogicState {
	
	/// <summary>
	/// The current logic state of this layer of the state machine
	/// </summary>
	public AOC2LogicState current;
	
	/// <summary>
	/// The base state, which this HFSM will start at when initialized and
	/// will attempt to defer to if any internal errors occur within the state machine
	/// </summary>
	protected AOC2LogicState _baseState;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2HFSMLogic"/> class.
	/// </summary>
	/// <param name='baseState'>
	/// Base state.
	/// </param>
	/// <param name='user'>
	/// User.
	/// </param>
	public AOC2HFSMLogic(AOC2LogicState baseState, AOC2Unit user) : base(user)
	{
		current = _baseState = baseState;
	}
	
	/// <summary>
	/// Initialize this instance.
	/// Sets the current state to the base state and initializes that
	/// </summary>
	public override void Init ()
	{
		current = _baseState;
		current.Init();
		base.Init();
	}
	
	/// <summary>
	/// Raises the exit state event.
	/// Exits the current state, and then calls this's own exit code
	/// </summary>
	public override void OnExitState ()
	{
		current.OnExitState();
		base.OnExitState();
	}
	
	/// <summary>
	/// Determine the current state by checking exit conditions
	/// </summary>
	public override IEnumerator Logic ()
	{
		while(true)
		{
			if (current != null)
			{
				AOC2LogicState change = current.GetExit();
				if (change != null)
				{
					//Clean up the old state
					current.OnExitState();
					
					//Set up the returned state for the next frame
					current = change;
					current.Init();
					
					//Keep checking if we need to immediately change
					//state
					//change = current.GetExit();
				}
				
				if (current.logic.MoveNext())
				{
					//Then, do current state logic
					object step = current.logic.Current;
				
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
				current = _baseState;
				yield return null;
			}
		}
	}
	
}
