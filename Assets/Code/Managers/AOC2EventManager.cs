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
		/// In the future, will be sent by a UI element trigger.
		/// Currently, activated by cheats.
		/// </summary>
		public static Action PlaceBuilding;
	}
}
