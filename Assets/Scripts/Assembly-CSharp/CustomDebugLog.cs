using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CustomDebugLog : MonoBehaviour
{
	private static Text debugTxt;

	private static StringBuilder appentText;

	private static int countMessages;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		debugTxt = GameObject.FindGameObjectWithTag("DebugLog").GetComponent<Text>();
		appentText = new StringBuilder();
		countMessages = 0;
	}

	public static void AppendBuilder(string Advertiser, string value)
	{
		if (!(debugTxt == null))
		{
			countMessages++;
			appentText.AppendLine(" " + countMessages + ") " + Advertiser + "  " + value);
			debugTxt.text = appentText.ToString();
		}
	}
}
