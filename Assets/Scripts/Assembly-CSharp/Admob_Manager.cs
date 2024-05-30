//using System;
//using GoogleMobileAds.Api;
//using UnityEngine;

//public class Admob_Manager : MonoBehaviour
//{
//	public delegate void RewardedEvent_Rewarded();

//	public delegate void RewardedEvent_Interstitial();

//	private BannerView bannerView;

//	private InterstitialAd interstitial;

//	private RewardBasedVideoAd rewardBasedVideo;

//	public static event RewardedEvent_Rewarded eventRewarded;

//	public static event RewardedEvent_Interstitial eventInterstitial;

//	private void Awake()
//	{
//		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
//	}

//	private void Start()
//	{
//		InitializeRewardBasedVideo();
//	}

//	public void RequestBanner()
//	{
//		string adUnitId = "ca-app-pub-2279608033555978/5764795641";
//		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
//		bannerView.OnAdLoaded += HandleAdLoaded;
//		bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
//		bannerView.OnAdOpening += HandleAdOpened;
//		bannerView.OnAdClosed += HandleAdClosed;
//		bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
//		bannerView.LoadAd(new AdRequest.Builder().Build());
//	}

//	public void ShowBanner()
//	{
//		bannerView.Show();
//	}

//	public void HideBanner()
//	{
//		bannerView.Hide();
//	}

//	public void RequestInterstitial()
//	{
//		string adUnitId = "ca-app-pub-2279608033555978/8718262045";
//		interstitial = new InterstitialAd(adUnitId);
//		interstitial.OnAdLoaded += HandleInterstitialLoaded;
//		interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
//		interstitial.OnAdOpening += HandleInterstitialOpened;
//		interstitial.OnAdClosed += HandleInterstitialClosed;
//		interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
//		interstitial.LoadAd(new AdRequest.Builder().Build());
//	}

//	public void ShowInterstitial()
//	{
//		if (interstitial != null && interstitial.IsLoaded())
//		{
//			interstitial.Show();
//		}
//	}

//	public bool HasInterstitial()
//	{
//		if (interstitial == null)
//		{
//			return false;
//		}
//		return interstitial.IsLoaded();
//	}

//	public void InitializeRewardBasedVideo()
//	{
//		string appId = "ca-app-pub-2279608033555978/3333754592";
//		MobileAds.Initialize(appId);
//		rewardBasedVideo = RewardBasedVideoAd.Instance;
//		rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
//		rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
//		rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
//		rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
//		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
//		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
//		rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
//	}

//	public void RequestRewardBasedVideo()
//	{
//		string adUnitId = "ca-app-pub-2279608033555978/3333754592";
//		AdRequest request = new AdRequest.Builder().Build();
//		rewardBasedVideo.LoadAd(request, adUnitId);
//	}

//	public void ShowRewardBasedVideo()
//	{
//		if (rewardBasedVideo.IsLoaded())
//		{
//			rewardBasedVideo.Show();
//		}
//	}

//	public bool HasRewardBasedVideo()
//	{
//		return rewardBasedVideo.IsLoaded();
//	}

//	private AdRequest createTargetAdRequest()
//	{
//		return new AdRequest.Builder().AddTestDevice("SIMULATOR").AddTestDevice("0123456789ABCDEF0123456789ABCDEF").AddKeyword("game")
//			.SetGender(Gender.Unknown)
//			.SetBirthday(new DateTime(1985, 1, 1))
//			.TagForChildDirectedTreatment(false)
//			.AddExtra("color_bg", "9B30FF")
//			.Build();
//	}

//	public void HandleAdLoaded(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdLoaded event received.");
//	}

//	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
//	}

//	public void HandleAdOpened(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdOpened event received");
//	}

//	private void HandleAdClosing(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdClosing event received");
//	}

//	public void HandleAdClosed(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdClosed event received");
//	}

//	public void HandleAdLeftApplication(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdLeftApplication event received");
//	}

//	public void HandleInterstitialLoaded(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialLoaded event received.");
//	}

//	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
//	}

//	public void HandleInterstitialOpened(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialOpened event received");
//	}

//	private void HandleInterstitialClosing(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialClosing event received");
//	}

//	public void HandleInterstitialClosed(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialClosed event received");
//		if (Admob_Manager.eventInterstitial != null)
//		{
//			Admob_Manager.eventInterstitial();
//		}
//	}

//	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
//	}

//	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
//	}

//	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
//	}

//	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
//	}

//	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
//	}

//	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
//	{
//	}

//	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
//	{
//		string type = args.Type;
//		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + args.Amount + " " + type);
//		if (Admob_Manager.eventRewarded != null)
//		{
//			Admob_Manager.eventRewarded();
//		}
//	}

//	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
//	}
//}
