using UnityEngine;
using System.Collections;

public class AOC2TriggerPopupButton : MonoBehaviour {

	public UIPanel popup;
	
	void OnClick()
	{
		AOC2EventManager.Popup.OnPopup(popup);
	}
	
}
