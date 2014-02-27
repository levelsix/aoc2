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
	
	/// <summary>
	/// The path of grid nodes
	/// </summary>
	Stack<AOC2GridNode> path;
	
	/// <summary>
	/// The logic for moving towards the next target position
	/// </summary>
	AOC2LogicMoveTowardTarget followLogic;
	
	/// <summary>
	/// An exit state set up to check when to grab the next node
	/// </summary>
	AOC2ExitTargetInRange newNodeLogic;
	
	/// <summary>
	/// Gets the next node position, popping the list in the process.
	/// If there isn't another node, sets the state to complete and sets the final
	/// target position
	/// </summary>
	/// <value>
	/// The next node position.
	/// </value>
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
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2LogicFollowPath"/> class.
	/// </summary>
	/// <param name='thisUnit'>
	/// This unit.
	/// </param>
	public AOC2LogicFollowPath(AOC2Unit thisUnit)
		: base(thisUnit)
	{
		followLogic = new AOC2LogicMoveTowardTarget(_user);
		newNodeLogic = new AOC2ExitTargetInRange(null, _user, AOC2ManagerReferences.gridManager.spaceSize / 2);
	}
	
	/// <summary>
	/// Initilzie this instance by calling the Pathfinder function to generate the path
	/// </summary>
	public override void Init ()
	{
		base.Init();
		
		_user.currentLogicState = "Path";
		
		path = AOC2Pathfind.aStar(
		 	new AOC2GridNode(AOC2ManagerReferences.gridManager.PointToGridCoords(_user.aPos.position)),
			new AOC2GridNode(AOC2ManagerReferences.gridManager.PointToGridCoords(_user.targetPos.position)));
		
		followLogic.Init();
		_user.targetPos = new AOC2Position(nextNodePos); //Need a new pos to make sure we aren't messing with an old transform
	}
	
	/// <summary>
	/// Logic this instance.
	/// Moves towards the next node in succession
	/// </summary>
	public override IEnumerator Logic ()
	{
		while(true)
		{
			if (newNodeLogic.Test()) //If we need a new node
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
	}
	
}
