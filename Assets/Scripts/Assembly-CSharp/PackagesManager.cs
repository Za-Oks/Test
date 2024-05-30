using UnityEngine;

public class PackagesManager : MonoBehaviour
{
	[Header("Appstore")]
	public App_Stores whichStore;

	[Header("Company Names")]
	public string AmazonCompany;

	public string GoogleCompany;

	public string IOSCompany;

	[Header("Bundles")]
	public string AmazonBundle;

	public string GoogleBundle;

	public string IOSBundle;

	[HideInInspector]
	public string AmazonUrlRate = "amzn://apps/android?p=";

	[HideInInspector]
	public string GoogleUrlRate = "market://details?id=";

	[HideInInspector]
	public string IOSUrlRate = "itms-apps://itunes.apple.com/app/id";

	[Header("Share")]
	public string AmazonShareSuffix;

	public string AppleShareSuffix;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		SetRateUrls();
	}

	private void SetRateUrls()
	{
		AmazonUrlRate += AmazonBundle;
		GoogleUrlRate += GoogleBundle;
		IOSUrlRate += AppleShareSuffix;
	}

	public string GetShareURL()
	{
		if (whichStore == App_Stores.Google)
		{
			return "https://play.google.com/store/apps/details?id=" + GoogleBundle;
		}
		if (whichStore == App_Stores.Amazon)
		{
			return "http://www.amazon.com/" + AmazonShareSuffix;
		}
		if (whichStore == App_Stores.IOS)
		{
			return "https://itunes.apple.com/app/id" + AppleShareSuffix;
		}
		return string.Empty;
	}
}
