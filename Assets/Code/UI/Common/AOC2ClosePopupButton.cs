using UnityEngine;
using System.Collections;

public class AOC2ClosePopupButton : MonoBehaviour {

	void OnClick()
	{
		AOC2EventManager.Popup.CloseAllPopups();
	}
	
}
