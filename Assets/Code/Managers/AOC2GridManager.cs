using UnityEngine;
using System.Collections;

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
    public const int GRID_SIZE = 36;

    /// <summary>
    /// Size of the placeable area of the mesh
    /// </summary>
    public const float WORLD_SIZE = 26.5f;
	
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
	private static float _space = float.NaN;
	
    /// <summary>
    /// The size of one side of a square on the grid
    /// Stored the first time we get it, so we're not doing
    /// this calculation more than necessary
    /// </summary>
	public static float SPACE_SIZE{
		get
		{
			if (float.IsNaN(_space))
			{
				_space = WORLD_SIZE / GRID_SIZE;	
			}
			return _space;
		}
	}
	
	/// <summary>
	/// The hypotenuse through the center of a square.
	/// Store it here, so we only have to calculate it once
	/// </summary>
	private static float _hypoton = float.NaN;
	
	/// <summary>
	/// Gets the hypotenuse of a space.
	/// If it hasn't been set, calculates it once.
	/// </summary>
	public static float SPACE_HYPOTENUSE{
		get
		{
			if (float.IsNaN(_hypoton))
			{
				_hypoton = Mathf.Sqrt(SPACE_SIZE * SPACE_SIZE * 2);
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
	    _grid = new AOC2Building[GRID_SIZE,GRID_SIZE];
	}
	    
	/// <summary>
    /// Checks if there is room on the ground for the given building prefab to be inserted
    /// at the specified coordinates
    /// </summary>
    /// <param name="building">Building to use for checking for space</param>
    /// <param name="x">X position to check the grid at</param>
    /// <param name="y">Y position to check the grid at</param>
    /// <returns>Whether the grid has space for this building at the given space</returns>
    public bool HasSpaceForBuilding(AOC2Building building, int x, int y)
    {
        for (int i = 0; i < building.width; i++)
        {
            for (int j = 0; j < building.length; j++)
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
    /// <param name="building">Building to use for checking for space</param>
    /// <param name="coords">Position to check the grid at</param>
    /// <returns>Whether the grid has space for this building at the given space</returns>
	public bool HasSpaceForBuilding(AOC2Building building, Vector2 coords)
	{
		return HasSpaceForBuilding (building, (int)coords.x, (int)coords.y);	
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
		return (x >= 0 && x < GRID_SIZE && y >= 0 && y < GRID_SIZE);
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
            return SPACE_SIZE * GRID_OFFSET * size;
        }
        if (dimension > SPACE_SIZE * GRID_SIZE - GRID_OFFSET * size)
        {
            return SPACE_SIZE * GRID_SIZE - SPACE_SIZE * GRID_OFFSET * size;
        }
        return Mathf.Round(dimension / SPACE_SIZE - SPACE_SIZE * GRID_OFFSET * size) * SPACE_SIZE + SPACE_SIZE * GRID_OFFSET * size;
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
		return new Vector2((int)(point.x / SPACE_SIZE), (int)(point.z / SPACE_SIZE));
	}    
	
	/// <summary>
    /// Adds a building to this ground's grid
    /// </summary>
    /// <param name="building">The building to be added</param>
    /// <param name="x">The left-most x position in grid coordinates</param>
    /// <param name="y">The lowest y positing in grid coordinates</param>
    public void AddBuilding(AOC2Building building, int x, int y)
    {
        _grid[x, y] = building;
        building.groundPos = new Vector2(x, y);

        for (int i = 0; i < building.width; i++)
        {
            for (int j = 0; j < building.length; j++)
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
	public Vector3 ScreenToGround(Vector3 screenPos)
	{
		float dist;
		Ray fromPoint = Camera.main.ScreenPointToRay(screenPos);
		if (GROUND_PLANE.Raycast(fromPoint, out dist))
		{
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
	public Vector2 ScreenToPoint(Vector3 screenPos)
	{
		return PointToGridCoords(ScreenToGround(screenPos));
	}
	
	/// <summary>
	/// Prints the debug grid.
	/// </summary>
	private void DebugPrintGrid()
	{
		string s = "";
		for (int i = 0; i < GRID_SIZE; i++) 
		{
			for (int j = 0; j < GRID_SIZE; j++) 
			{
				AOC2Building building = _grid[j, GRID_SIZE - 1 - i];
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
		Vector3 frontRight = transform.position + new Vector3(WORLD_SIZE,0,0);
		Vector3 backLeft = transform.position + new Vector3(0,0,WORLD_SIZE);
		Vector3 xOff = Vector3.zero;
		Vector3 zOff = Vector3.zero;
		for (int i = 0; i < GRID_SIZE; i++) 
		{
			xOff.x = SPACE_SIZE * i;
			zOff.z = SPACE_SIZE * i;
			Gizmos.DrawLine(transform.position + xOff, backLeft + xOff);
			Gizmos.DrawLine(transform.position + zOff, frontRight + zOff);
		}	
	}
}
