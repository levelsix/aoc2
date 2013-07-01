using UnityEngine;
using System.Collections;

/// <summary>
/// Component for a single building in Building Test
/// </summary>
public class BTBuilding : MonoBehaviour
{

    #region Public Members

    /// <summary>
    /// The material for this building to use when it's not selected
    /// </summary>
    public Material _unselectedMaterial;

    /// <summary>
    /// The material for this building to use when it is the currently selected building
    /// </summary>
    public Material _selectedMaterial;

    /// <summary>
    /// The material to show if the building is placed in an invalid location
    /// </summary>
    public Material _invalidMaterial;

    /// <summary>
    /// The ground that this building is located on
    /// Used for snapping to position
    /// </summary>
    public BTGround ground;

    /// <summary>
    /// The position within ground that this building is located
    /// </summary>
    public Vector2 groundPos;

    /// <summary>
    /// The size, in grid spaces, of this building
    /// </summary>
    public int size = 1;

    #endregion

    #region Private Members

    /// <summary>
    /// This building's mesh renderer, where we need to change materials upon selection
    /// </summary>
    private MeshRenderer _meshRenderer;

    /// <summary>
    /// This building's transform, 
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// If the building is being moved, this holds its position prior
    /// to the move
    /// </summary>
    private Vector3 _originalPos;

    /// <summary>
    /// If the building is being moved, the current position that it is being
    /// dragged over on the map
    /// </summary>
    private Vector2 currPos;

    #endregion

    #region Constants

    /// <summary>
    /// How much to offset the center of the building by for each size increase
    /// </summary>
    private static readonly Vector3 SIZE_OFFSET = new Vector3(.5f, 0, .5f);

    #endregion

    /// <summary>
    /// On awake, get a pointer to all necessary components
    /// </summary>
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        //NOTE: When Unity uses it's built-in properties like transform, it does
        //a GetComponent call every time. Since we're using this repeatedly, it's
        //much more efficient to store a direct pointer
        _transform = transform;
    }

    /// <summary>
    /// Set up scale, position, and material on start
    /// </summary>
    void Start()
    {
        _transform.localScale = new Vector3(size, 1, size);
        _transform.position += SIZE_OFFSET * size + new Vector3(0, .5f, 0);
        _meshRenderer.material = _unselectedMaterial;
    }

    /// <summary>
    /// Moves this building relative to its original position
    /// </summary>
    /// <param name="mouseDifference"></param>
    public void MoveRelative(Vector3 mouseDifference)
    {
        //Turn the mouse difference in screen coordinates to world coordinates
        mouseDifference.y *= 2 * (Camera.main.orthographicSize / Screen.height);
        mouseDifference.x *= 2 * (Camera.main.orthographicSize / Screen.width);

        //Turn the 2D coordinates into our tilted isometric coordinates
        mouseDifference.z = mouseDifference.y - mouseDifference.x;
        mouseDifference.x = mouseDifference.x + mouseDifference.y;
        mouseDifference.y = 0;

        //Add the difference to the original position, since we only hold original mouse pos
        _transform.position = _originalPos + mouseDifference;

        _transform.position = ground.SnapPointToGrid(transform.position, size);

        currPos = new Vector2(transform.position.x - SIZE_OFFSET.x * size,
                                    transform.position.z - SIZE_OFFSET.z * size);

        //Set the proper material for if the current position is valid
        if (!ground.HasSpace(this, (int)currPos.x, (int)currPos.y))
        {
            _meshRenderer.material = _invalidMaterial;
        }
        else
        {
            _meshRenderer.material = _selectedMaterial;
        }
    }

    /// <summary>
    /// Move the building to the curent position
    /// If the current position is invalid, move it back to its original position
    /// </summary>
    public void Place()
    {
        if (ground.HasSpace(this, (int)currPos.x, (int)currPos.y))
        {
            ground.AddBuilding(this, (int)currPos.x, (int)currPos.y);
        }
        else
        {
            ground.AddBuilding(this, (int)groundPos.x, (int)groundPos.y);
            _transform.position = _originalPos;
        }
    }

    /// <summary>
    /// When this building is selected
    /// </summary>
    public void OnSelect()
    {
        _meshRenderer.material = _selectedMaterial;
        _originalPos = transform.position;
        ground.RemoveBuilding(this);
    }

    public void OnDeselect()
    {
        _meshRenderer.material = _unselectedMaterial;
    }
}
