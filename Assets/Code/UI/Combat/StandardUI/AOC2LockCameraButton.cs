using UnityEngine;
using System.Collections;

public class AOC2LockCameraButton : MonoBehaviour {

	void OnClick()
    {
        if (AOC2EventManager.UI.OnCameraLockButton != null)
        {
            AOC2EventManager.UI.OnCameraLockButton();
        }
    }
}
