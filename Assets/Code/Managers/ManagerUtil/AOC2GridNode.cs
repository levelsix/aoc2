using UnityEngine;
using System.Collections;
using System;

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
	public int x, y;
	
	/// <summary>
	/// The heuristic value of this node
	/// </summary>
	public float heur = 0;
	
	/// <summary>
	/// The distance from the root
	/// </summary>
	public float dist = 0;
	
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
			return new Vector2(x, y);
		}
	}
	
	public AOC2GridNode()
	{
		x = 0;
		y = 0;
	}
	
	public AOC2GridNode(int _x, int _y)
	{
		x = _x;
		y = _y;
	}
	
	public AOC2GridNode(Vector2 _pos)
	{
		x = (int)_pos.x;
		y = (int)_pos.y;
	}
	
	public static AOC2GridNode operator +(AOC2GridNode n1, AOC2GridNode n2)
	{
		return new AOC2GridNode(n1.pos + n2.pos);
	}
	
	/// <summary>
	/// Sets the heuristic using manhattan distance
	/// </summary>
	/// <param name='destination'>
	/// Destination.
	/// </param>
	public void SetHeur(AOC2GridNode destination)
	{
		heur = Mathf.Abs(destination.x - x) + Mathf.Abs (destination.y - y);
	}
	
	
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
