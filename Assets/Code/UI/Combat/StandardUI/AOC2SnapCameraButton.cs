using UnityEngine;
using System.Collections;

public class AOC2SnapCameraButton : MonoBehaviour {

	void OnClick()
    {
        if (AOC2EventManager.UI.OnCameraSnapButton != null)
        {  
            AOC2EventManager.UI.OnCameraSnapButton();
        }
    }
}
