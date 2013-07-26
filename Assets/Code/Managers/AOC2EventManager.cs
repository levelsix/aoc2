using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Event Manager
/// All events that need to trigger actions on 
/// </summary>
public static class AOC2EventManager 
{
	
	/// <summary>
	/// All Control events. 
	/// These will only be called by AOC2ControlManager.
	/// Other managers and systems which tie into controls should
	/// read from these events.
	/// </summary>
	public static class Controls
	{
		//All touch events are indexed using how many touches were involved in their action
		//This way, listening to multi-touch events is done by listening to the index of touches-1
		//Example, OnTap[0] fires on a 1-finger tap; OnDoubleTap[2] fires on a three-fingered double-tap
		public static Action<AOC2TouchData>[] OnTap = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnStartHold = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnKeepHold = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnReleaseHold = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnStartDrag = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnKeepDrag = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnReleaseDrag = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnFlick = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		public static Action<AOC2TouchData>[] OnDoubleTap = new Action<AOC2TouchData>[AOC2ControlManager.MAX_TOUCHES];
		
		/// <summary>
		/// The on pinch event. The float passed along with it
		/// reflects the change in pinch distance in this frame.
		/// </summary>
		public static Action<float> OnPinch;
	}
	
	public static class Town
	{
		/// <summary>
		/// The on building select event.
		/// Notifies UI that we've selected a new building
		/// </summary>
		public static Action<AOC2Building> OnBuildingSelect;
		
		/// <summary>
		/// The place building event, which gives the signal to the
		/// selected building to place itself on the grid.
		/// </summary>
		public static Action PlaceBuilding;
	}
	
	public static class Combat
	{
		public static Action<AOC2Unit> OnSpawnPlayer;
		public static Action<AOC2Unit> OnSpawnEnemy;
		public static Action<AOC2Unit> OnEnemyDeath;
		public static Action<AOC2Unit> OnPlayerDeath;
		
		public static Action<AOC2Unit> OnPlayerHealthChange;
		
		public static Action OnEnemiesClear;
		public static Action OnPlayerVictory;
		
		public static Action<int> SetPlayerAttack;
		
		/// <summary>
		/// The on ability cooldown.
		/// First parameter is the index of the ability
		/// Second is 
		/// </summary>
		public static Action<int, float> OnAbilityCooldown;
	}
	
	public static class Popup
	{
		public static Action<GameObject> OnPopup;
		public static Action<int> ClosePopupLayer;
		public static Action CloseAllPopups;
		public static Action<string> CreatePopup;
	}
	
	public static class UI
	{
		public static Action<Camera> OnCameraResize;
		public static Action<int>[] OnChangeResource = new Action<int>[3];
        public static Action OnCameraLockButton;
        public static Action OnCameraSnapButton;
	}
	
	/// <summary>
	/// All of the events that need to be shared with the other players in PvP
	/// in order to communicate
	/// </summary>
	public static class NetCombat
	{
		public static Action<int, int> OnLocalPlayerSetMoveTarget; //Use xz grid coords
		public static Action<int> OnLocalPlayerSetUnitTarget;
		public static Action<int> OnLocalPlayerUseAbility;
		public static Action<AOC2Unit> OnLocalPlayerRoutineUpdate;
		public static Action<AOC2Delivery> OnLocalPlayerCreateDelivery;
		public static Action<AOC2Unit, int> OnLocalPlayerTakeDamage;
	}
}
