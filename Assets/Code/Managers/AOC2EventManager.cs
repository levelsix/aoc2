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
		public static Action<AOC2TouchData> OnTap;
		public static Action<AOC2TouchData> OnStartHold;
		public static Action<AOC2TouchData> OnKeepHold;
		public static Action<AOC2TouchData> OnReleaseHold;
		public static Action<AOC2TouchData> OnStartDrag;
		public static Action<AOC2TouchData> OnKeepDrag;
		public static Action<AOC2TouchData> OnReleaseDrag;
		public static Action<AOC2TouchData> OnFlick;
		public static Action<AOC2TouchData> OnDoubleTap;
		
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
		
		public static Action OnEnemiesClear;
		public static Action OnPlayerVictory;
	}
}
