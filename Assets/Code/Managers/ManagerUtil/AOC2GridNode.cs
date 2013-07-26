using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AOC2GridNode : IComparable {
	
	/// <summary>
	/// The parent of this node.
	/// For retracing a path once we've found
	/// the solution.
	/// </summary>
	public AOC2GridNode parent;
	
	/// <summary>
	/// Grid coordinates
	/// </summary>
	public int x, z;
	
	/// <summary>
	/// The heuristic value of this node
	/// </summary>
	public float heur = 0;
	
	/// <summary>
	/// The distance from the root
	/// </summary>
	public float dist = 0;
	
	private static readonly Vector2[] moveDirs =
	{
		new Vector2(0,1),
		new Vector2(1,1),
		new Vector2(1,0),
		new Vector2(0,-1),
		new Vector2(-1,-1),
		new Vector2(-1,0),
		new Vector2(-1,1),
		new Vector2(1,-1)
	};
		
	
	/// <summary>
	/// Gets the cost of this node, for sorting
	/// </summary>
	/// <value>
	/// The cost of this node
	/// </value>
	public float cost{
		get
		{
			return dist + heur;
		}
	}
	
	public Vector2 pos{
		get
		{
			return new Vector2(x, z);
		}
	}
	
	public Vector2 worldPos{
		get
		{
			return new Vector2(x * AOC2ManagerReferences.gridManager.spaceSize,
				z * AOC2ManagerReferences.gridManager.spaceSize);
		}
	}
	
	public AOC2GridNode()
	{
		x = 0;
		z = 0;
	}
	
	public AOC2GridNode(int _x, int _y)
	{
		x = _x;
		z = _y;
	}
	
	public AOC2GridNode(Vector2 _pos)
	{
		x = (int)_pos.x;
		z = (int)_pos.y;
	}
	
	public static AOC2GridNode operator +(AOC2GridNode n1, AOC2GridNode n2)
	{
		return new AOC2GridNode(n1.pos + n2.pos);
	}
	
	public static implicit operator Vector2(AOC2GridNode value)
	{
		return value.pos;
	}
	
	public static implicit operator AOC2GridNode(Vector2 value)
	{
		return new AOC2GridNode(value);
	}
	
	/// <summary>
	/// Sets the heuristic using manhattan distance
	/// </summary>
	/// <param name='destination'>
	/// Destination.
	/// </param>
	public void SetHeur(AOC2GridNode destination)
	{
		heur = Mathf.Abs(destination.x - x) + Mathf.Abs (destination.z - z);
	}
	
	/// <summary>
	/// Gets all neighbors.
	/// </summary>
	/// <returns>
	/// The neighbors.
	/// </returns>
	public List<AOC2GridNode> GetNeighbors()
	{
		List<AOC2GridNode> neighs = new List<AOC2GridNode>();
		
		
		for (int i = 0; i < moveDirs.Length; i++) {
			if (AOC2ManagerReferences.gridManager.CanMoveInDir(this, moveDirs[i])) //This is never working!!!
			{
				AOC2GridNode node = new AOC2GridNode(pos + moveDirs[i]);
				node.dist = dist + 1;
				node.parent = this;
				neighs.Add(node);
			}
		}
		
		return neighs;
	}
	
	/// <summary>
	/// Compares costs
	/// </summary>
	/// <returns>
	/// Comparison turnery int
	/// </returns>
	/// <param name='obj'>
	/// Other grid node
	/// </param>
	public int CompareTo(object obj)
	{
		if (!(obj is AOC2GridNode))
		{
			Debug.LogError("Cannot compare grid nodes to other types");
			return -1;
		}
		
		return cost.CompareTo((obj as AOC2GridNode).cost);
	}
}
