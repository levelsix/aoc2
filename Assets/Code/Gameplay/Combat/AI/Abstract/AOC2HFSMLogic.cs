using UnityEngine;
using System.Collections;

public class AOC2HFSMLogic : AOC2LogicState {
	
	public AOC2LogicState current;
	
	protected AOC2LogicState _baseState;
	
	public AOC2HFSMLogic(AOC2LogicState baseState, AOC2Unit user) : base(user)
	{
		current = _baseState = baseState;
	}
	
	public override void Init ()
	{
		current = _baseState;
		current.Init();
		base.Init();
	}
	
	public override void OnExitState ()
	{
		current.OnExitState();
		base.OnExitState();
	}
	
	public override IEnumerator Logic ()
	{
		while(true)
		{
			if (current != null)
			{
				AOC2LogicState change = current.GetExit();
				while (change != null)
				{
					//Clean up the old state
					current.OnExitState();
					
					//Set up the returned state for the next frame
					current = change;
					current.Init();
					
					//Keep checking if we need to immediately change
					//state
					change = current.GetExit();
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
