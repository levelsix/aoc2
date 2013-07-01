using UnityEngine;
using System.Collections;

public class BTControlManager : MonoBehaviour {

    /// <summary>
    /// Constant that Unity uses for left-click
    /// </summary>
    private const int LEFT_MOUSE = 0;

    /// <summary>
    /// If the mouse is held down, the point where the 
    /// mouse first clicked down
    /// </summary>
    private Vector3 _clickPoint;

    /// <summary>
    /// Building we currently have selected.
    /// </summary>
    public BTBuilding _selectedBuilding;
	
	/// <summary>
    /// Update is called once per frame
	/// </summary>
	void Update () {
        ProcessMouse();
	}

    /// <summary>
    /// Process mouse click for selection and movement
    /// </summary>
    private void ProcessMouse()
    {
        //Checks left-click
        if (Input.GetMouseButtonDown(LEFT_MOUSE))
        {
            //If we had a selected building, select whatever we just clicked on
            if (_selectedBuilding != null)
            {
                _selectedBuilding.OnDeselect();
            }

            //Cast a ray using the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //If our ray hits, select that building
                _selectedBuilding = hit.collider.GetComponent<BTBuilding>();
                if (_selectedBuilding != null)
                {
                    _selectedBuilding.OnSelect();
                }
            }
            else
            {
                _selectedBuilding = null;
            }
            //Store the point clicked
            _clickPoint = Input.mousePosition;
        }
        else if (Input.GetMouseButton(LEFT_MOUSE))
        {
            //If we have a continuing click going on, move the selected building
            if (_selectedBuilding != null)
            {
                _selectedBuilding.MoveRelative(Input.mousePosition - _clickPoint);
            }
        }
        else if (Input.GetMouseButtonUp(LEFT_MOUSE))
        {
            //When we release a click
            if (_selectedBuilding != null)
            {
                _selectedBuilding.Place();
            }
        }
    }
}
