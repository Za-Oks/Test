using UnityEngine;

public class Appsflyer_Manager : MonoBehaviour
{
	private void Start()
	{
		AppsFlyer.setAppsFlyerKey("RcrRTEszbptAakuihnNrsB");
		AppsFlyer.setAppID("com.rappidstudios.simulatorbattlephysics");
		AppsFlyer.init("RcrRTEszbptAakuihnNrsB", "AppsFlyerTrackerCallbacks");
	}
}
