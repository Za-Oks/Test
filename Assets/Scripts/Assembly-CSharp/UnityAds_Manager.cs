//using UnityEngine;
//using UnityEngine.Advertisements;

//public class UnityAds_Manager : MonoBehaviour
//{
//	public delegate void RewardedEvent_Rewarded();

//	public delegate void RewardedEvent_Interstitial();

//	public static event RewardedEvent_Rewarded eventRewarded;

//	public static event RewardedEvent_Interstitial eventInterstitial;

//	private void Awake()
//	{
//		Object.DontDestroyOnLoad(base.gameObject);
//	}

//	public void ShowInterstitial()
//	{
//		ShowOptions showOptions = new ShowOptions();
//		showOptions.resultCallback = HandleShowResult_Interstitial;
//		ShowOptions showOptions2 = showOptions;
//		Advertisement.Show("video", showOptions2);
//	}

//	private void HandleShowResult_Interstitial(ShowResult result)
//	{
//		switch (result)
//		{
//		}
//		if (UnityAds_Manager.eventInterstitial != null)
//		{
//			UnityAds_Manager.eventInterstitial();
//		}
//	}

//	public bool HasInterstitial()
//	{
//		if (Advertisement.IsReady("video"))
//		{
//			return true;
//		}
//		return false;
//	}

//	public void ShowRewardedVideo()
//	{
//		ShowOptions showOptions = new ShowOptions();
//		showOptions.resultCallback = HandleShowResult_Rewarded;
//		ShowOptions showOptions2 = showOptions;
//		Advertisement.Show("rewardedVideo", showOptions2);
//	}

//	private void HandleShowResult_Rewarded(ShowResult result)
//	{
//		switch (result)
//		{
//		case ShowResult.Finished:
//			if (UnityAds_Manager.eventRewarded != null)
//			{
//				UnityAds_Manager.eventRewarded();
//			}
//			break;
//		}
//	}

//	public bool HasRewardedVideo()
//	{
//		if (Advertisement.IsReady("rewardedVideo"))
//		{
//			return true;
//		}
//		return false;
//	}
//}
