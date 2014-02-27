using UnityEngine;
using System.Collections;

public class AOC2GameUIManager : MonoBehaviour {
	
	/// <summary>
	/// The transform
	/// </summary>
	Transform trans;
	
	/// <summary>
	/// Awake this instance.
	/// Set up internal component references
	/// </summary>
	void Awake()
	{
		AOC2ManagerReferences.gameUIManager = this;
		trans = transform;
	}
	
	/// <summary>
	/// Grabs a UI of the given prefab from the pool, sets it up at the
	/// given world coordinates, and then passes it back to whoever called
	/// the function
	/// </summary>
	/// <returns>
	/// The poolable component of the object
	/// </returns>
	/// <param name='prefab'>
	/// Prefab for the UI element
	/// </param>
	/// <param name='origin'>
	/// World position at which to create the UI 
	/// </param>
	public AOC2Poolable GrabUIRef(AOC2Poolable prefab, Vector3 origin)
	{
		AOC2Poolable item = AOC2ManagerReferences.poolManager.Get(prefab, origin);
		item.transf.parent = trans;
		return item;
	}
	
}
