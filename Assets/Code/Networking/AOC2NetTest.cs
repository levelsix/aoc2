using UnityEngine;
using System.Collections;
using proto;

public class AOC2NetTest : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		
		yield return new WaitForSeconds(2);
		
		Debug.Log("Sending Test");
		
		StartupRequestProto startup = new StartupRequestProto();
		startup.mup = new MinimumUserProto();
		startup.mup.name = "Test";
		startup.mup.udid = "9001";
		startup.mup.userID = "12";
		startup.mup.gameCenterId = "teest";
		
		startup.loginType = StartupRequestProto.LoginType.UDID;
		
		
		AOC2ManagerReferences.networkManager.SendRequest(startup, (int)AocTwoEventProtocolRequest.C_STARTUP_EVENT);
	}
}
