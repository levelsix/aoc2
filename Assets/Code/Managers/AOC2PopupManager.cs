using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOC2PopupManager : MonoBehaviour {

	List<UIPanel> _currPops;
	
	void Awake()
	{
		_currPops = new List<UIPanel>();
	}
	
	void OnEnable()
	{
		AOC2EventManager.Popup.OnPopup += OnPopup;
		AOC2EventManager.Popup.CloseAllPopups += CloseAllPopups;
		AOC2EventManager.Popup.ClosePopupLayer += ClosePopupLayer;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Popup.OnPopup -= OnPopup;
		AOC2EventManager.Popup.CloseAllPopups -= CloseAllPopups;
		AOC2EventManager.Popup.ClosePopupLayer -= ClosePopupLayer;
	}
	
	void OnPopup(UIPanel popup)
	{
		popup.gameObject.SetActive(true);
		_currPops.Add(popup);
	}
	
	void CloseAllPopups()
	{
		ClosePopupLayer(0);
	}
	
	void ClosePopupLayer(int stackLayer)
	{
		while(_currPops.Count >= stackLayer)
		{
			_currPops[_currPops.Count-1].gameObject.SetActive(false);
			_currPops.RemoveAt(_currPops.Count-1);
		}
	}
	
}
