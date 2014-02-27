using UnityEngine;
using System.Collections;
using proto;

public class AOC2PrototypeChooseClassButton : MonoBehaviour {
	
	[SerializeField]
	ClassType classType;
	
	void OnClick()
	{
		AOC2Whiteboard.playerClass = classType;
		
		AOC2ManagerReferences.dataManager.LoadLevel(AOC2MonsterList.roomA);
	}
}
