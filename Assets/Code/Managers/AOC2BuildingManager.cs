using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Manager for buildings
/// Keeps track of all buildings in the scene
/// As well as works as the intermediatary for controls interacting with the buildings
/// </summary>
public class AOC2BuildingManager : MonoBehaviour
{
	#region Members
	
    #region Public members

    /// <summary>
    /// Whether or not this ground should create its own set of buildings
    /// </summary>
    public bool createRandomBuildings = true;

    /// <summary>
    /// Prefab for a small building
    /// </summary>
    public AOC2Building smallBuildingPrefab;

    /// <summary>
    /// Prefab for a medium building
    /// </summary>
    public AOC2Building mediumBuildingPrefab;

    /// <summary>
    /// Prefab for a large building
    /// </summary>
    public AOC2Building largeBuildingPrefab;
	
	/// <summary>
	/// The scene's camera, which this needs to pass
	/// along drag events when the camera is the target
	/// </summary>
	public AOC2BuildingCamera cam;

    #endregion

    #region Private
	/// <summary>
	/// The current selected building.
	/// </summary>
	private AOC2Building _selectedBuilding;
	
	/// <summary>
	/// The current target, which will be moved as long
	/// as a drag continues
	/// </summary>
	private AOC2Placeable _target;

    #endregion

    #region Constants

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
	
	#endregion

    #region Functions
	
	#region Initialization/Clean-up
	
    /// <summary>
	/// Initialization, create the grid. If we want to generate buildings, add them in now
	/// </summary>
	void Start () {
        //Make a bunch of buildings to test
        if (createRandomBuildings)
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
	/// Raises the enable event.
	/// Sets up the event delegates
	/// </summary>
	void OnEnable ()
	{
		AOC2EventManager.Controls.OnTap += OnTap;
		AOC2EventManager.Controls.OnKeepDrag += OnDrag;
		AOC2EventManager.Controls.OnStartHold += OnStartHold;
		AOC2EventManager.Controls.OnReleaseDrag += OnReleaseDrag;
		AOC2EventManager.Controls.OnStartDrag += OnStartDrag;
		AOC2EventManager.Town.PlaceBuilding += OnPlace;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Removes all event delegates
	/// </summary>
	void OnDisable ()
	{
		AOC2EventManager.Controls.OnTap -= OnTap;
		AOC2EventManager.Controls.OnKeepDrag -= OnDrag;
		AOC2EventManager.Controls.OnStartHold -= OnStartHold;
		AOC2EventManager.Controls.OnStartDrag -= OnStartDrag;
		AOC2EventManager.Controls.OnReleaseDrag -= OnReleaseDrag;
		AOC2EventManager.Town.PlaceBuilding -= OnPlace;
	}
	
	#endregion
	
	#region Building Generation
	
    /// <summary>
    /// Makes a building, then adds it to the grid in a random open position
    /// </summary>
    /// <param name="buildPrefab">The prefab for the building we want to build</param>
    /// <returns>The building, placed on the grid</returns>
    private AOC2Building MakeBuilding(AOC2Building buildPrefab)
    {
        int x, y;
        do
        {
            x = (int)(Random.value * (AOC2GridManager.GRID_SIZE + 1 - buildPrefab.width));
            y = (int)(Random.value * (AOC2GridManager.GRID_SIZE + 1 - buildPrefab.length));
        } while (!AOC2ManagerReferences.gridManager.HasSpaceForBuilding(buildPrefab, x, y));

        Vector3 position = new Vector3(AOC2GridManager.SPACE_SIZE * x, 0, AOC2GridManager.SPACE_SIZE * y);

        AOC2Building building = Instantiate(buildPrefab, position, transform.rotation) as AOC2Building;
        AOC2ManagerReferences.gridManager.AddBuilding(building, x, y);

        return building;
    }
	
	/// <summary>
	/// Makes a building, adding it to the closest place near the center of the current screen possible
	/// </summary>
	/// <returns>
	/// The created building
	/// </returns>
	/// <param name='buildPrefab'>
	/// The prefab for the building to be made
	/// </param>
	private AOC2Building MakeBuildingCenter(AOC2Building buildPrefab)
	{
		Vector2 coords = AOC2ManagerReferences.gridManager.ScreenToPoint(new Vector3(Screen.width/2, Screen.height/2));
		coords = FindSpaceInRange(buildPrefab, coords, 0);
		
		Vector3 position = new Vector3(AOC2GridManager.SPACE_SIZE * coords.x, 0, AOC2GridManager.SPACE_SIZE * coords.y);
		
		AOC2Building building = Instantiate(buildPrefab, position, transform.rotation) as AOC2Building;
        AOC2ManagerReferences.gridManager.AddBuilding(building, (int)coords.x, (int)coords.y);

        return building;
	}
	
	#endregion
	
	#region Building Placement
	
	/// <summary>
	/// Checks all of the spaces in the given range.
    /// If no appropriate space, recursively inreases the range
	/// </summary>
	/// <returns>
	/// The space where this building can fit
	/// </returns>
	/// <param name='buildingPrefab'>
	/// Building prefab.
	/// </param>
	/// <param name='startPos'>
	/// Start position.
	/// </param>
	/// <param name='range'>
	/// Range.
	/// </param>
	public Vector2 FindSpaceInRange(AOC2Building buildingPrefab, Vector2 startPos, int range)
	{
		if (range > 36)
		{
			throw new System.Exception("Not enough room to place the building. Throw a popup and refund.");
		}
		for (int i = 0; i <= range; i++) 
		{
			Vector2 space;
			space = CheckSpaces(buildingPrefab, startPos, range, i);
			if (space.x >= 0) return space;
			space = CheckSpaces(buildingPrefab, startPos, i, range);
			if (space.x >= 0) return space;
		}
		return FindSpaceInRange(buildingPrefab, startPos, range+1);
	}
	
	/// <summary>
	/// Checks the spaces using the given derivations from the base position
	/// </summary>
	/// <returns>
	/// The spaces.
	/// </returns>
	/// <param name='buildingPrefab'>
	/// Building prefab.
	/// </param>
	/// <param name='basePos'>
	/// Base position.
	/// </param>
	/// <param name='x'>
	/// X derivation
	/// </param>
	/// <param name='y'>
	/// Y derivation
	/// </param>
	public Vector2 CheckSpaces(AOC2Building buildingPrefab, Vector2 basePos, int x, int y)
	{
		if (AOC2ManagerReferences.gridManager.HasSpaceForBuilding(buildingPrefab, basePos + new Vector2(x,y)))
		{
			return basePos+new Vector2(x,y);	
		}
		if (x==0 || y==0) return new Vector2(-1,-1);
		if (AOC2ManagerReferences.gridManager.HasSpaceForBuilding(buildingPrefab, basePos + new Vector2(-x,y)))
		{
			return basePos+new Vector2(-x,y);	
		}
		if (AOC2ManagerReferences.gridManager.HasSpaceForBuilding(buildingPrefab, basePos + new Vector2(x,-y)))
		{
			return basePos+new Vector2(x,-y);	
		}
		if (AOC2ManagerReferences.gridManager.HasSpaceForBuilding(buildingPrefab, basePos + new Vector2(-x,-y)))
		{
			return basePos+new Vector2(-x,-y);	
		}
		
		return new Vector2(-1,-1);
	}
	

	
	#endregion
	
	#region Grid/Building Control (Adding/Removing)
	
	/// <summary>
	/// Selects the building from a touch or click
	/// </summary>
	/// <param name='point'>
	/// Point on screen
	/// </param>
	public AOC2Building SelectBuildingFromScreen(Vector2 point)
	{
        //Cast a ray using the mouse position
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //If our ray hits, select that building
            return hit.collider.GetComponent<AOC2Building>();
        }
        else
        {
            return null;
        }
	}
	
	/// <summary>
	/// Deselect the current building, if one is selected, and place it
	/// </summary>
	private void Deselect()
	{
		if (_selectedBuilding != null)
		{
			//_selectedBuilding.Deselect();
			AOC2EventManager.Town.PlaceBuilding();	
			_selectedBuilding = null;
		}
	}
	
	#endregion
	
	#region Debug
	
#if UNITY_EDITOR
	/// <summary>
	/// Cheats.
	/// Update this instance.
	/// </summary>
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (AOC2EventManager.Town.PlaceBuilding != null)
			{
				AOC2EventManager.Town.PlaceBuilding();
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//AOC2ManagerReferences.gridManager.DebugPrintGrid();	
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			MakeBuildingCenter(mediumBuildingPrefab);	
		}
	}
#endif
	
	#endregion
	
	#region Control Delegates
	
	/// <summary>
	/// Casts a ray from the tap point
 	/// Selects or deselects a building
	/// </summary>
	/// <param name='touch'>
	/// Touch.
	/// </param>
	public void OnTap(AOC2TouchData touch)
	{        
		AOC2Building chosen = SelectBuildingFromScreen(touch.pos);
		if (chosen != null)
		{
			if (chosen != _selectedBuilding)
			{
				Deselect();
				chosen.Select();
			}
			_selectedBuilding = chosen;
		}
		else
		{
			Deselect();	
			_selectedBuilding = null;
		}
	}
	
	/// <summary>
	/// Raises the start hold event.
	/// If we start holding on a building, select it and get it ready to start moving
	/// If we start holding on empty space, get the camera ready to be moved
	/// </summary>
	/// <param name='touch'>
	/// Touch.
	/// </param>
	public void OnStartHold(AOC2TouchData touch)
	{
		//If we're on a building, select it so that if we start dragging, we can move it
		AOC2Building chosen = SelectBuildingFromScreen(touch.pos);
		if (chosen != null)
		{
			if (chosen != _selectedBuilding){
				Deselect();
				chosen.Select();
			}
			_selectedBuilding = chosen;
			_target = _selectedBuilding;
		}
		if (_selectedBuilding == null || _target == null)
		{
			_target = cam;
		}
	}
	
	/// <summary>
	/// Raises the start drag event.
	/// If the drag started on the selected building, target that building
	/// Otherwise, target the camera
	/// </summary>
	/// <param name='touch'>
	/// Touch.
	/// </param>
	public void OnStartDrag(AOC2TouchData touch)
	{
		if (_selectedBuilding != null && SelectBuildingFromScreen(touch._initialPos) == _selectedBuilding)
		{
			_target = _selectedBuilding;
		}
		else
		{
			_target = cam;
		}
	}
	
	/// <summary>
	/// Raises the release drag event.
	/// When a drag is released, if we have a building selected, drop it.
	/// </summary>
	/// <param name='touch'>
	/// Touch.
	/// </param>
	public void OnReleaseDrag(AOC2TouchData touch)
	{
		if (_selectedBuilding != null)
		{
			_selectedBuilding.Drop();
		}
	}
	
	/// <summary>
	/// Raises the drag event.
	/// As long as we're dragging, move the target.
	/// </summary>
	/// <param name='touch'>
	/// Touch.
	/// </param>
	public void OnDrag(AOC2TouchData touch)
	{
		//Move building or camera
		if (_target != null){
			_target.MoveRelative(touch);
		}
	}
	
	/// <summary>
	/// Raises the place event.
	/// If we place a building, null out the selected building pointer
	/// </summary>
	public void OnPlace()
	{
		_selectedBuilding = null;	
	}
	
	#endregion
	
    #endregion
}
