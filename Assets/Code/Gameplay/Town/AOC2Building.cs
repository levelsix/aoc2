using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// Component for a single building
/// </summary>
[RequireComponent (typeof(AOC2Sprite))]
[RequireComponent (typeof(BoxCollider))]
public class AOC2Building : MonoBehaviour, AOC2Placeable
{
	#region Members
	
    #region Public

    /// <summary>
    /// The position within ground that this building is located
    /// </summary>
    public Vector2 groundPos;

    /// <summary>
    /// The width, in grid spaces, of this building
    /// </summary>
    public int width = 1;
	
	/// <summary>
	/// The length, in grid spaces, of this building
	/// </summary>
	public int length = 1;
	
    /// <summary>
    /// This building's transform, 
    /// </summary>
    public Transform trans;
	
	/// <summary>
	/// The color of the building when selected.
	/// </summary>
	public Color selectColor = new Color(.5f, 1, .5f);
	
	/// <summary>
	/// The color of the building when placed in an improper place
	/// </summary>
	public Color badColor = new Color(1f, .5f, .5f);
	
    #endregion

    #region Private

    /// <summary>
    /// If the building is being moved, this holds its position prior
    /// to the move
    /// </summary>
    private Vector3 _originalPos;
	
	/// <summary>
	/// If the building is being moved, this keeps track of its
	/// steps along the way, so that a building can be moved in multiple
	/// steps before being placed
	/// </summary>
	private Vector3 _tempPos;

    /// <summary>
    /// If the building is being moved, the current position that it is being
    /// dragged over on the map
    /// </summary>
    private Vector2 _currPos;
	
	/// <summary>
	/// If the building is currently selected
	/// </summary>
	private bool _selected;
	
	/// <summary>
	/// The sprite for this building.
	/// </summary>
	private AOC2Sprite _sprite;
	
	/// <summary>
	/// The box collider for this building
	/// </summary>
	private BoxCollider _box;
	
	/// <summary>
	/// Current color.
	/// </summary>
	private Color _currColor;
	
	/// <summary>
	/// The direction of the tint ping-pong
	/// </summary>
	private int _ppDir = 1;
	
	/// <summary>
	/// The current tracker for the ping-pong
	/// </summary>
	private float _ppPow = 0;
	
    #endregion

    #region Constants
	
	/// <summary>
	/// The amount of time it takes the tint to ping-pong
	/// when this building is selected
	/// </summary>
	private const float COLOR_SPEED = 1.5f;

    /// <summary>
    /// How much to offset the center of the building by for each size increase
    /// </summary>
    private static Vector3 SIZE_OFFSET{
		get
		{
			return new Vector3(AOC2GridManager.SPACE_SIZE * .5f, 0, AOC2GridManager.SPACE_SIZE * .5f);
		}
	}
	
	/// <summary>
	/// Constant coefficient for the drag amount to get it 
	/// to work just right
	/// </summary>
	private const float X_DRAG_FUDGE = 1.1f;

    #endregion
	
	#endregion
	
	#region Functions
	
	#region Instantiation/Clean-up
	
    /// <summary>
    /// On awake, get a pointer to all necessary components
    /// </summary>
    void Awake()
    {
		_sprite = GetComponent<AOC2Sprite>();
		_box = GetComponent<BoxCollider>();

        //NOTE: When Unity uses it's built-in properties like transform, it does
        //a GetComponent call every time. Since we're using this repeatedly, it's
        //much more efficient to store a direct pointer
        trans = transform;
    }

    /// <summary>
    /// Set up scale, position, and material on start
    /// </summary>
    void Start()
    {
        //_transform.localScale = new Vector3(width * AOC2BuildingManager.SPACE_SIZE, 1, length * AOC2BuildingManager.SPACE_SIZE);
		_box.size = new Vector3(width * AOC2GridManager.SPACE_SIZE, 1, length * AOC2GridManager.SPACE_SIZE);
		
        trans.position += new Vector3(SIZE_OFFSET.x * width, 0, SIZE_OFFSET.z * length);
		_sprite.MakeBuildingMesh(this);
        //_meshRenderer.material = _unselectedMaterial;
    }
	
	/// <summary>
	/// Raises the disable event.
	/// Makes sure that if this building is selected, it removes the event pointer for placement
	/// </summary>
	void OnDisable()
	{
		if (_selected)
		{
			AOC2EventManager.Town.PlaceBuilding -= Place;
		}
	}
	
	#endregion
	
	#region Building Selection & Movement
	
    /// <summary>
    /// Moves this building relative to its original position
    /// </summary>
    /// <param name="movement"></param> 
    public void MoveRelative(AOC2TouchData touch)
    {
		Vector3 movement = touch.Movement;
		
        //Turn the mouse difference in screen coordinates to world coordinates
        movement.y *= 2 * (Camera.main.orthographicSize / Screen.height);
        movement.x *= 2 * (Camera.main.orthographicSize / Screen.width) * X_DRAG_FUDGE;

        //Turn the 2D coordinates into our tilted isometric coordinates
        movement.z = movement.y - movement.x;
        movement.x = movement.x + movement.y;
        movement.y = 0;

        //Add the difference to the original position, since we only hold original mouse pos
        trans.position = _tempPos + movement;

        trans.position = AOC2ManagerReferences.gridManager.SnapPointToGrid(transform.position, width, length);

        _currPos = new Vector2(transform.position.x / AOC2GridManager.SPACE_SIZE - SIZE_OFFSET.x * width,
                                    transform.position.z / AOC2GridManager.SPACE_SIZE - SIZE_OFFSET.z * length);
		
		if (AOC2ManagerReferences.gridManager.HasSpaceForBuilding(this, _currPos))
		{
			_currColor = selectColor;
		}
		else
		{
			_currColor = badColor;
		}
		
    }
	
	/// <summary>
	/// Drop this instance in place, so that it can be moved more with another
	/// drag
	/// </summary>
	public void Drop()
	{
		_tempPos = trans.position;	
	}

    /// <summary>
    /// Move the building to the curent position
    /// If the current position is invalid, move it back to its original position
    /// </summary>
    public void Place()
    {
        if (AOC2ManagerReferences.gridManager.HasSpaceForBuilding(this, (int)_currPos.x, (int)_currPos.y))
        {
            AOC2ManagerReferences.gridManager.AddBuilding(this, (int)_currPos.x, (int)_currPos.y);
			_originalPos = trans.position;
        }
        else
        {
            AOC2ManagerReferences.gridManager.AddBuilding(this, (int)groundPos.x, (int)groundPos.y);
            trans.position = _originalPos;
        }
		AOC2EventManager.Town.PlaceBuilding -= Place;
		Deselect();
    }

    /// <summary>
    /// When this building is selected
    /// </summary>
    public void Select()
    {
		if (!_selected){
			_originalPos = trans.position;
			_tempPos = trans.position;
	        AOC2ManagerReferences.gridManager.RemoveBuilding(this);
			AOC2EventManager.Town.PlaceBuilding += Place;
			_selected = true;
			_currColor = selectColor;
			
			StartCoroutine(ColorPingPong());
		}
    }
	
	/// <summary>
	/// Deselect this instance.
	/// </summary>
    public void Deselect()
    {
		_selected = false;
		//Reset color to untinted
		_sprite.SetColor(Color.white);
    }
	
	#endregion
	
	#region Utility
	
	/// <summary>
	/// Coroutine that lerps the color back and forth between
	/// white and the current color
	/// </summary>
	IEnumerator ColorPingPong()
	{
		_ppDir = 1;
		_ppPow = 0;
		while(_selected)
		{
			_ppPow += _ppDir * Time.deltaTime * COLOR_SPEED;
			
			//See if we need to change direction
			if (_ppPow >= 1)
			{
				_ppPow = 1;
				_ppDir = -1;
			}
			else if (_ppPow <= 0)
			{
				_ppPow = 0;
				_ppDir = 1;
			}
			
			_sprite.SetColor(Color.Lerp(Color.white, _currColor, _ppPow));
			
			yield return new WaitForEndOfFrame();
		}
	}
	
	#endregion
	
	
	
	#endregion
}
