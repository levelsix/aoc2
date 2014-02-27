using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Class for managing protos, and reporting when protos have been
/// downloaded from the server.
/// External use: Call Load(int) on a list to get the object of that type and ID
/// If it has already been retrieved, it will be reported immediately.
/// </summary>
[Serializable]
public class AOC2DataList<T> {
 
	Type listType;
	
	//Dictionary of all data loaded thus far.
	//TODO: Save this data, so that we don't need to do big bulk server requests
	//every startup, just every update
    private Dictionary<int, T> dict = new Dictionary<int, T>();
    
	//Fire this function whenever the list loads something new
	//That will cause every instance waiting for server response to get
	//its Data.
	//Other classes should attach to this action right before calling Load, in case
	//OnLoad returns immediately. Once OnLoad is called with the proper int ID, the
	//class should detach itself promptly.
	public Action<int, T> OnLoad;
	
	public IEnumerator Load(int i)
	{
		if (dict.ContainsKey(i))
		{
			if (OnLoad != null)
			{
				OnLoad(i, dict[i]);
			}
		}
		else
		{
			//DEBUG
			yield return null;
			
			//Check if this request is already pending (Keep a dictionary of them?)
			
			//Server Request
			
			//Wait for response
			
			//Set dictionary reference
			
			//Fire OnLoad response
		}
	}
	
}
