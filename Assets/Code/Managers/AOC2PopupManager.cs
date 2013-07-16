using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2PopupManager : MonoBehaviour {
	
	/// <summary>
	/// The popup prefab.
	/// </summary>
	[SerializeField]
	AOC2Popup popupPrefab;
	
	[SerializeField]
	UIPanel popupPanel;
	
	/// <summary>
	/// The stack of current popup menus.
	/// </summary>
	Stack<GameObject> _currPops;
	
	/// <summary>
	/// Awake this instance.
	/// Set up the stack for popups
	/// </summary>
	void Awake()
	{
		_currPops = new Stack<GameObject>();
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
		AOC2EventManager.Popup.CreatePopup += CreatePopup;
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
		AOC2EventManager.Popup.CreatePopup -= CreatePopup;
	}
	
	void CreatePopup(string text)
	{
		AOC2Popup pop = Instantiate(popupPrefab) as AOC2Popup;
		
		pop.Init(text);
		
		Transform popT = pop.transform;
		
		popT.parent = popupPanel.transform;
		popT.localScale = Vector3.one;
		popT.localPosition = Vector3.zero;
		
		OnPopup(pop.gameObject);
	}
	
	/// <summary>
	/// Raises the popup event.
	/// Adds a popup to the popup stack.
	/// </summary>
	/// <param name='popup'>
	/// Popup.
	/// </param>
	void OnPopup(GameObject popup)
	{
		popup.SetActive(true);
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
			_currPops.Pop().SetActive(false);
		}
	}
	
}
