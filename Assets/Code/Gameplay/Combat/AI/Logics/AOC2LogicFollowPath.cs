using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Weird HFSM for following a path from the Pathfinder
/// </summary>
public class AOC2LogicFollowPath : AOC2LogicState {
	
	Stack<AOC2GridNode> path;
	
	AOC2Unit _unit;
	
	AOC2LogicMoveTowardTarget followLogic;
	
	AOC2ExitTargetInRange newNodeLogic;
	
	AOC2Position destination;
	
	Vector3 nextNodePos{
		get
		{
			if (path.Count > 0)
			{
				return AOC2ManagerReferences.gridManager.GridToWorld(path.Pop().pos);
			}
			else
			{
				complete = true;
				return _unit.targetPos.position;
			}
		}
	}
	
	public AOC2LogicFollowPath(AOC2Unit thisUnit)
		: base()
	{
		_unit = thisUnit;
		followLogic = new AOC2LogicMoveTowardTarget(_unit);
	}
	
	public override void Start ()
	{
		base.Start();
		
		
		path = AOC2Pathfind.aStar(
		 	new AOC2GridNode(AOC2ManagerReferences.gridManager.PointToGridCoords(_unit.aPos.position)),
			new AOC2GridNode(AOC2ManagerReferences.gridManager.PointToGridCoords(_unit.targetPos.position)));
		
		followLogic.Start();
		_unit.targetPos = new AOC2Position(nextNodePos); //Need a new pos to make sure we aren't messing with an old transform
	}
	
	protected override IEnumerator Logic ()
	{
		while(true)
		{
			if (followLogic.complete)
			{
				_unit.targetPos.position = nextNodePos;
				followLogic.Start();
			}
			if(followLogic.logic.MoveNext()){
				yield return followLogic.logic.Current;
			}
			else
			{
				yield return null;
			}
		}
		//Debug.Log("Wha?");
	}
	
}
