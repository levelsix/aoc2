using UnityEngine;
using System.Collections;

public class AOC2ChangeSceneButton : MonoBehaviour {
 
    public AOC2Values.Scene.Scenes scene;
    
    void OnClick()
    {
        AOC2Values.Scene.ChangeScene(scene);  
    }
    
}
