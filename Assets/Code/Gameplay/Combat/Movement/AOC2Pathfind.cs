using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AOC2Pathfind {
	
	static bool DEBUG = true;
	
	public static Stack<AOC2GridNode> aStar(AOC2GridNode start, AOC2GridNode end)
	{
		List<AOC2GridNode> open = new List<AOC2GridNode>();
		Dictionary<Vector2,AOC2GridNode> closed = new Dictionary<Vector2, AOC2GridNode>();
		
		open.Add (start);
		
		AOC2GridNode current;
		while(open.Count > 0)
		{
			current = open[0];
			open.RemoveAt(0);
			
			//if (DEBUG) Debug.Log("Considering " + current.pos);
			
			if (current.pos == end.pos)
			{
				return BuildPath(current);
			}
			
			closed[current.pos] = current;
			
			foreach (AOC2GridNode item in current.GetNeighbors())
			{
				//Debug.Log("Checking neighbor: " + item.pos);
				item.SetHeur(end);
				if (closed.ContainsKey(item.pos) && closed[item.pos].cost <= item.cost)
				{
					continue;
				}
				
				open.Add(item);
				closed[item.pos] = item;
			}
			
			open.Sort();
		}
		return null;
	}
	
	private static Stack<AOC2GridNode> BuildPath(AOC2GridNode end)
	{
		Stack<AOC2GridNode> path = new Stack<AOC2GridNode>();
		AOC2GridNode current = end;
		while(current != null)
		{
			path.Push(current);
			current = current.parent;
		}
		
		return path;
	}
}
