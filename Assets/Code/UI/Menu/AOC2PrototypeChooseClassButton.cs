using UnityEngine;
using System.Collections;
using proto;

public class AOC2PrototypeChooseClassButton : MonoBehaviour {
	
	[SerializeField]
	ClassType classType;
	
	void OnClick()
	{
		AOC2Whiteboard.playerClass = classType;
		
		AOC2Values.Scene.ChangeScene(AOC2Values.Scene.Scenes.DUNGEON_TEST_SCENE);
	}
}
