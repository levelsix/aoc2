using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// For when your hit-box needs to be bigger for touch
/// </summary>
public class AOC2ClickBox : MonoBehaviour {

	public AOC2Unit parent;
	
	void Start()
	{
		if (parent == null)
		{
			parent = transform.parent.GetComponent<AOC2Unit>();
		}
	}
	
}
