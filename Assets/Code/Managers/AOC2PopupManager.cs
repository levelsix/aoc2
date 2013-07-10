using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2PopupManager : MonoBehaviour {
	
	Stack<UIPanel> _currPops;
	
	/// <summary>
	/// Awake this instance.
	/// Set up the stack for popups
	/// </summary>
	void Awake()
	{
		_currPops = new Stack<UIPanel>();
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Assigns delegates.
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Popup.OnPopup += OnPopup;
		AOC2EventManager.Popup.CloseAllPopups += CloseAllPopups;
		AOC2EventManager.Popup.ClosePopupLayer += ClosePopupLayer;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Deassigns deletages.
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Popup.OnPopup -= OnPopup;
		AOC2EventManager.Popup.CloseAllPopups -= CloseAllPopups;
		AOC2EventManager.Popup.ClosePopupLayer -= ClosePopupLayer;
	}
	
	/// <summary>
	/// Raises the popup event.
	/// Adds a popup to the popup stack.
	/// </summary>
	/// <param name='popup'>
	/// Popup.
	/// </param>
	void OnPopup(UIPanel popup)
	{
		popup.gameObject.SetActive(true);
		_currPops.Push(popup);
	}
	
	/// <summary>
	/// Closes all popups.
	/// </summary>
	void CloseAllPopups()
	{
		ClosePopupLayer(0);
	}
	
	/// <summary>
	/// Closes the popup layer and all layers above it, but not below it
	/// </summary>
	/// <param name='stackLayer'>
	/// Stack layer.
	/// </param>
	void ClosePopupLayer(int stackLayer)
	{
		while(_currPops.Count > stackLayer)
		{
			_currPops.Pop().gameObject.SetActive(false);
		}
	}
	
}
