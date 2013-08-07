using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Hierarchal state machine for following a path from the Pathfinder.
/// Finds the path to the next grid coordinates, then grabs the next
/// grid coordinates.
/// </summary>
public class AOC2LogicFollowPath : AOC2LogicState {
	
	Stack<AOC2GridNode> path;
	
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
				_complete = true;
				return _user.targetPos.position;
			}
		}
	}
	
	public AOC2LogicFollowPath(AOC2Unit thisUnit)
		: base(thisUnit)
	{
		followLogic = new AOC2LogicMoveTowardTarget(_user);
		newNodeLogic = new AOC2ExitTargetInRange(null, _user, AOC2ManagerReferences.gridManager.spaceSize / 2);
	}
	
	public override void Init ()
	{
		base.Init();
		
		_user.currentLogicState = "Path";
		
		//Store target position for when we exit
		destination = _user.targetPos;
		
		path = AOC2Pathfind.aStar(
		 	new AOC2GridNode(AOC2ManagerReferences.gridManager.PointToGridCoords(_user.aPos.position)),
			new AOC2GridNode(AOC2ManagerReferences.gridManager.PointToGridCoords(_user.targetPos.position)));
		
		followLogic.Init();
		_user.targetPos = new AOC2Position(nextNodePos); //Need a new pos to make sure we aren't messing with an old transform
	}
	
	public override void OnExitState ()
	{
		base.OnExitState();
		
		//_user.targetPos = destination; 
	}
	
	public override IEnumerator Logic ()
	{
		while(true)
		{
			if (newNodeLogic.Test())
			{
				_user.targetPos.position = nextNodePos;
				followLogic.Init();
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
