using UnityEngine;
using System.Collections;

public class AOC2TriggerPopupButton : MonoBehaviour {

	public GameObject popup;
	
	void OnClick()
	{
		AOC2EventManager.Popup.OnPopup(popup);
	}
	
}
