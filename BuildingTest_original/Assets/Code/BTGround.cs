using UnityEngine;
using System.Collections;

/// <summary>
/// Ground component for Building Test
/// Keeps track of all buildings on top of it
/// </summary>
public class BTGround : MonoBehaviour
{
    #region Public members

    /// <summary>
    /// Whether or not this ground should create its own set of buildings
    /// </summary>
    public bool createBuildings = true;

    /// <summary>
    /// Prefab for a small building
    /// </summary>
    public BTBuilding smallBuildingPrefab;

    /// <summary>
    /// Prefab for a medium building
    /// </summary>
    public BTBuilding mediumBuildingPrefab;

    /// <summary>
    /// Prefab for a large building
    /// </summary>
    public BTBuilding largeBuildingPrefab;

    #endregion

    #region Private members

    /// <summary>
    /// The grid of buildings atop the ground
    /// </summary>
    BTBuilding[,] grid;

    /// <summary>
    /// The size of one side of a square on the grid
    /// </summary>
    private float spaceSize;

    #endregion

    #region Constants

    /// <summary>
    /// Size of the ground grid, in squares
    /// </summary>
    const int GRID_SIZE = 10;

    /// <summary>
    /// By default, this mesh is 10x10 in Unity units
    /// </summary>
    const int UNITY_DEFAULT_SIZE = 10;

    /// <summary>
    /// The offset of the grid in each direction due to spacing things
    /// </summary>
    const float GRID_OFFSET = .5f;

    /// <summary>
    /// Number of small buildings to make
    /// </summary>
    const int NUM_SMALL_BUILDINGS = 5;

    /// <summary>
    /// Number of medium buildings to make
    /// </summary>
    const int NUM_MED_BUILDINGS = 3;

    /// <summary>
    /// Number of large buildings to make
    /// </summary>
    const int NUM_LARGE_BUILDINGS = 2;

    #endregion

    #region Functions

    /// <summary>
	/// Initialization, create the grid. If we want to generate buildings, add them in now
	/// </summary>
	void Start () {
	    grid = new BTBuilding[GRID_SIZE,GRID_SIZE];
        spaceSize = UNITY_DEFAULT_SIZE / GRID_SIZE;

        //Make a bunch of buildings to test
        if (createBuildings)
        {
            for (int i = 0; i < NUM_LARGE_BUILDINGS; i++)
            {
                MakeBuilding(largeBuildingPrefab);
            }
            for (int i = 0; i < NUM_MED_BUILDINGS; i++)
            {
                MakeBuilding(mediumBuildingPrefab);
            }
            for (int i = 0; i < NUM_SMALL_BUILDINGS; i++)
            {
                MakeBuilding(smallBuildingPrefab);
            }
        }
	}

    /// <summary>
    /// Makes a building, then adds it to the grid
    /// </summary>
    /// <param name="buildPrefab">The prefab for the building we want to build</param>
    /// <returns>The building, placed on the grid</returns>
    private BTBuilding MakeBuilding(BTBuilding buildPrefab)
    {
        int x, y;
        do
        {
            x = (int)(Random.value * (GRID_SIZE + 1 - buildPrefab.size));
            y = (int)(Random.value * (GRID_SIZE + 1 - buildPrefab.size));
        } while (!HasSpace(buildPrefab, x, y));

        Vector3 position = new Vector3(spaceSize * x, 0, spaceSize * y);

        BTBuilding building = Instantiate(buildPrefab, position, transform.rotation) as BTBuilding;
        AddBuilding(building, x, y);

        return building;
    }

    /// <summary>
    /// Checks if there is room on the ground for the given building prefab to be inserted
    /// at the specified coordinates
    /// </summary>
    /// <param name="building"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool HasSpace(BTBuilding building, int x, int y)
    {
        for (int i = 0; i < building.size; i++)
        {
            for (int j = 0; j < building.size; j++)
            {
                if (grid[x + i, y + j] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Takes a point and snaps it's X and Z components to the grid
    /// </summary>
    /// <param name="point">The point to be rounded</param>
    /// <param name="size">The size of the building. Used for determining edges
    /// and how the grid should be offset</param>
    /// <returns>The altered coordinates</returns>
    public Vector3 SnapPointToGrid(Vector3 point, int size)
    {
        point.x = SnapDimension(point.x, size);
        point.z = SnapDimension(point.z, size);
        return point;
    }

    /// <summary>
    /// Snaps a single dimension to the grid
    /// </summary>
    /// <param name="dimension">The dimension to be snapped</param>
    /// <param name="size">The size of the building. Used for determining how
    /// the grid should be offset</param>
    /// <returns>The altered dimension</returns>
    float SnapDimension(float dimension, int size)
    {
        if (dimension < GRID_OFFSET * size)
        {
            return GRID_OFFSET * size;
        }
        if (dimension > spaceSize * GRID_SIZE - GRID_OFFSET * size)
        {
            return spaceSize * GRID_SIZE - GRID_OFFSET * size;
        }
        return Mathf.Round(dimension - GRID_OFFSET * size) + GRID_OFFSET * size;
    }

    /// <summary>
    /// Adds a building to this ground's grid
    /// </summary>
    /// <param name="building">The building to be added</param>
    /// <param name="x">The left-most x position in grid coordinates</param>
    /// <param name="y">The lowest y positing in grid coordinates</param>
    public void AddBuilding(BTBuilding building, int x, int y)
    {
        grid[x, y] = building;
        building.ground = this;
        building.groundPos = new Vector2(x, y);

        for (int i = 0; i < building.size; i++)
        {
            for (int j = 0; j < building.size; j++)
            {
                grid[x + i, y + j] = building;
            }
        }
    }

    /// <summary>
    /// Removes a building from the building grid by using the building's
    /// size and base position to null out all references to it in the
    /// ground grid.
    /// </summary>
    /// <param name="building">The building being removed from the grid</param>
    public void RemoveBuilding(BTBuilding building)
    {
        for (int i = 0; i < building.size; i++)
        {
            for (int j = 0; j < building.size; j++)
            {
                grid[(int)building.groundPos.x + i, (int)building.groundPos.y + j] = null;
            }
        }
    }

    #endregion

}
