using UnityEngine;
using System.Collections;
using proto;

public class AOC2GridManager : MonoBehaviour {
	
	#region Members
	
	#region Public
	
	#endregion
	
	#region Private
	
	/// <summary>
    /// The grid of everything in the scene
    /// </summary>
    private AOC2Building[,] _grid;

	#endregion
	
	#region Constant
	
    /// <summary>
    /// Size of the ground grid, in squares
    /// </summary>
    public int gridSize = 36;

    /// <summary>
    /// Size of the placeable area of the mesh
    /// </summary>
    public float worldSize = 36f;
	
	public const float GRID_OFFSET = .5f;
	
	/// <summary>
	/// The plane that represents the ground; the x- and z-axes at y=0
	/// </summary>
	private static readonly Plane GROUND_PLANE = new Plane(Vector3.up, Vector3.zero);
	
	#endregion
	
	#region Properties
		
	/// <summary>
	/// The size of one space.
	/// Stored here so that we only have to calculate it once.
	/// </summary>
	private float _space = float.NaN;
	
    /// <summary>
    /// The size of one side of a square on the grid
    /// Stored the first time we get it, so we're not doing
    /// this calculation more than necessary
    /// </summary>
	public float spaceSize{
		get
		{
			if (float.IsNaN(_space))
			{
				_space = worldSize / gridSize;	
			}
			return _space;
		}
	}
	
	/// <summary>
	/// The hypotenuse through the center of a square.
	/// Store it here, so we only have to calculate it once
	/// </summary>
	private float _hypoton = float.NaN;
	
	/// <summary>
	/// Gets the hypotenuse of a space.
	/// If it hasn't been set, calculates it once.
	/// </summary>
	public float gridSpaceHypotenuse{
		get
		{
			if (float.IsNaN(_hypoton))
			{
				_hypoton = Mathf.Sqrt(spaceSize * spaceSize * 2);
			}
			return _hypoton;
		}
	}
	
	#endregion
	
	#endregion
	

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () 
	{
		AOC2ManagerReferences.gridManager = this;
	    _grid = new AOC2Building[gridSize,gridSize];
	}
	    
	/// <summary>
    /// Checks if there is room on the ground for the given building prefab to be inserted
    /// at the specified coordinates
    /// </summary>
    /// <param name="proto">Building to use for checking for space</param>
    /// <param name="x">X position to check the grid at</param>
    /// <param name="y">Y position to check the grid at</param>
    /// <returns>Whether the grid has space for this building at the given space</returns>
    public bool HasSpaceForBuilding(StructureProto proto, int x, int y)
    {
        for (int i = 0; i < proto.size.x; i++)
        {
            for (int j = 0; j < proto.size.y; j++)
            {
				if (!OnGrid(x+i, y+j))
				{
					return false;	
				}
                if (_grid[x + i, y + j] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
	
	/// <summary>
    /// Checks if there is room on the ground for the given building prefab to be inserted
    /// at the specified coordinates
    /// </summary>
    /// <param name="proto">Building to use for checking for space</param>
    /// <param name="coords">Position to check the grid at</param>
    /// <returns>Whether the grid has space for this building at the given space</returns>
	public bool HasSpaceForBuilding(StructureProto proto, AOC2GridNode coords)
	{
		return HasSpaceForBuilding (proto, coords.x, coords.z);	
	}
	
	/// <summary>
	/// Checks if a point is on the grid.
	/// </summary>
	/// <returns>
	/// True if this point is within the grid coordinates
	/// </returns>
	/// <param name='x'>
	/// X position
	/// </param>
	/// <param name='y'>
	/// Y position
	/// </param>
	private bool OnGrid(int x, int y)
	{
		return (x >= 0 && x < gridSize && y >= 0 && y < gridSize);
	}
	
    /// <summary>
    /// Takes a point and snaps it's X and Z components to the grid
    /// </summary>
    /// <param name="point">The point to be rounded</param>
    /// <param name="size">The size of the building. Used for determining edges
    /// and how the grid should be offset</param>
    /// <returns>The altered coordinates</returns>
    public Vector3 SnapPointToGrid(Vector3 point, int width, int length)
    {
        point.x = SnapDimension(point.x, width);
        point.z = SnapDimension(point.z, length);
        return point;
    }

    /// <summary>
    /// Snaps a single dimension to the grid.
    /// Clamps it to the grid also
    /// </summary>
    /// <param name="dimension">The dimension to be snapped</param>
    /// <param name="size">The size of the building. Used for determining how
    /// the grid should be offset</param>
    /// <returns>The altered dimension</returns>
    float SnapDimension(float dimension, int size)
    {
        if (dimension < GRID_OFFSET * size)
        {
            return spaceSize * GRID_OFFSET * size;
        }
        if (dimension > spaceSize * gridSize - GRID_OFFSET * size)
        {
            return spaceSize * gridSize - spaceSize * GRID_OFFSET * size;
        }
        return Mathf.Round(dimension / spaceSize - spaceSize * GRID_OFFSET * size) * spaceSize + spaceSize * GRID_OFFSET * size;
    }
	
	/// <summary>
	/// Takes in a point in world coords and turns it into grid coords
	/// </summary>
	/// <returns>
	/// The grid coords
	/// </returns>
	/// <param name='point'>
	/// The world coords
	/// </param>
	public Vector2 PointToGridCoords(Vector3 point)
	{
		return new AOC2GridNode(Mathf.Clamp((int)(point.x / spaceSize), 0, gridSize-1), Mathf.Clamp((int)(point.z / spaceSize), 0, gridSize-1));
	}
	
	/// <summary>
	/// Takes a grid position and turns it into world coordinates
	/// </summary>
	/// <returns>
	/// The world coordinates of the center of the grid space
	/// </returns>
	/// <param name='pos'>
	/// Position on grid
	/// </param>
	public Vector3 GridToWorld(Vector2 pos)
	{
		return new Vector3((pos.x + GRID_OFFSET) * spaceSize, 0, (pos.y + GRID_OFFSET) * spaceSize);
	}
	
	/// <summary>
    /// Adds a building to this ground's grid
    /// </summary>
    /// <param name="building">The building to be added</param>
    /// <param name="x">The left-most x position in grid coordinates</param>
    /// <param name="y">The lowest y positing in grid coordinates</param>
    public void AddBuilding(AOC2Building building, int x, int y, StructureProto proto)
    {
        _grid[x, y] = building;
        building.groundPos = new Vector2(x, y);

        for (int i = 0; i < proto.size.x; i++)
        {
            for (int j = 0; j < proto.size.y; j++)
            {
                _grid[x + i, y + j] = building;
            }
        }
    }
	
	/// <summary>
    /// Removes a building from the building grid by using the building's
    /// size and base position to null out all references to it in the
    /// ground grid.
    /// </summary>
    /// <param name="building">The building being removed from the grid</param>
    public void RemoveBuilding(AOC2Building building)
    {
        for (int i = 0; i < building.width; i++)
        {
            for (int j = 0; j < building.length; j++)
            {
                _grid[(int)building.groundPos.x + i, (int)building.groundPos.y + j] = null;
            }
        }
    }
	
	/// <summary>
	/// Raycasts a screen position from the camera to get a position on the xz-plane
	/// </summary>
	/// <returns>
	/// Point on the xz-plane
	/// </returns>
	/// <param name='screenPos'>
	/// Screen position.
	/// </param>
	public Vector3 ScreenToGround(Vector3 screenPos, bool withinGrid = false)
	{
		float dist;
		Ray fromPoint = Camera.main.ScreenPointToRay(screenPos);
		if (GROUND_PLANE.Raycast(fromPoint, out dist))
		{
			if (withinGrid)
			{
				return SnapPointToGrid(fromPoint.GetPoint(dist),0,0);
			}
			return fromPoint.GetPoint(dist);
		}
		Debug.LogWarning("Raycast from screen to ground failed. Returned (0,0,0).");
		return Vector3.zero;
	}
	
	/// <summary>
	/// Raycasts a screen position to a grid position
	/// </summary>
	/// <returns>
	/// The grid position
	/// </returns>
	/// <param name='screenPos'>
	/// Screen position.
	/// </param>
	public AOC2GridNode ScreenToPoint(Vector3 screenPos)
	{
		return PointToGridCoords(ScreenToGround(screenPos));
	}
	
	/// <summary>
	/// Prints the debug grid.
	/// </summary>
	private void DebugPrintGrid()
	{
		string s = "";
		for (int i = 0; i < gridSize; i++) 
		{
			for (int j = 0; j < gridSize; j++) 
			{
				AOC2Building building = _grid[j, gridSize - 1 - i];
				if (building == null) s += "0 ";
				else s += building.width + building.length + " ";
			}
			s += "\n";
		}
		Debug.Log(s);
	}
	
	/// <summary>
	/// DEBUG
	/// Raises the draw gizmos event.
	/// Draws the ground grid for lining up with background
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Vector3 frontRight = transform.position + new Vector3(worldSize,0,0);
		Vector3 backLeft = transform.position + new Vector3(0,0,worldSize);
		Vector3 xOff = Vector3.zero;
		Vector3 zOff = Vector3.zero;
		for (int i = 0; i < gridSize; i++) 
		{
			if (i % 5 == 0)
			{
				Gizmos.color = Color.magenta;
			}
			else
			{
				Gizmos.color = Color.yellow;
			}
			xOff.x = spaceSize * i;
			zOff.z = spaceSize * i;
			Gizmos.DrawLine(transform.position + xOff, backLeft + xOff);
			Gizmos.DrawLine(transform.position + zOff, frontRight + zOff);
		}	
	}
	
	public bool IsOpen(Vector2 pos)
	{
		return IsOpen((int)pos.x, (int)pos.y);
	}
	
	public bool IsOpen(int x, int y)
	{
		return x >= 0 && y >= 0 && x < gridSize && y < gridSize && _grid[x,y] == null;
	}
	
	public bool CanMoveInDir(AOC2GridNode start, Vector2 dir)
	{
		if (start.x + dir.x > gridSize || start.x + dir.x < 0)
		{
			return false;
		}
		if (start.z + dir.y > gridSize || start.z + dir.y < 0)
		{
			return false;
		}
		
		if (!IsOpen(start.pos + dir))
		{
			return false;
		}
		
		if (dir.x * dir.y != 0) //Diagonal case
		{
			return (IsOpen((int)(start.x + dir.x), (int)(start.z)) && IsOpen((int)start.x, (int)(start.z + dir.y)));
		}
		return true;
	}
	
	public bool CanMoveToNeighbor(AOC2GridNode start, AOC2GridNode end)
	{
		//Get dx and dy
		int dx = end.x - start.x;
		int dy = end.z - start.z;
		
		if (Mathf.Abs (dx) > 1 || Mathf.Abs (dy) > 1)
		{
			Debug.LogError("Bad neighbor check!");
		}
		
		if (!IsOpen (end.x, end.z))
		{
			return false;
		}
		
		//Diagonal needs all three open
		if (dx * dy != 0)
		{
			return (IsOpen(start.x,end.z) && IsOpen(end.x,start.z));
		}
		return true;
	}
}
