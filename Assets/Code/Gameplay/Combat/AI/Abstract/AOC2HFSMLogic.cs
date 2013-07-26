using UnityEngine;
using System.Collections;

public class AOC2HFSMLogic : AOC2LogicState {
	
	public AOC2LogicState _current;
	
	protected AOC2LogicState _baseState;
	
	public AOC2HFSMLogic() : base() 
	{
	}
	
	public AOC2HFSMLogic(AOC2LogicState baseState) : base()
	{
		_current = _baseState = baseState;
	}
	
	public override void Init ()
	{
		_current = _baseState;
		_current.Init();
		base.Init ();
	}
	
	public override IEnumerator Logic ()
	{
		while(true)
		{
			if (_current != null)
			{
				//First, check for exits
				AOC2LogicState change = _current.GetExit();
				while (change != null)
				{
					//Clean up the old state
					_current.OnExitState();
					
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
	
}
