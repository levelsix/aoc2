using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Rob Giusti
/// Manager component for dealing with setting up synchronized multiplayer.
/// </summary>
public class AOC2SynchMultiManager : MonoBehaviour {
	
	Dictionary<uint, AOC2Unit> players;
	
	void Awake() 
	{
		players = new Dictionary<uint, AOC2Unit>();
	}
	
	void OnEnable()
	{
		
	}
	
	void OnDisable()
	{
		
	}
	
	void Start()
	{
		GameCenterBinding.authenticateLocalPlayer();
		
		//Test: Try joining any game
		GameCenterMultiplayerBinding.findMatchProgrammaticallyWithMinMaxPlayers (2, 4);
	}
	
	
}
