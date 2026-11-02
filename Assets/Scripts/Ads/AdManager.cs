using UnityEngine;
using UnityEngine.Advertisements;

public struct AdvertisementIDs
{
    public const string GAME_ID = "800420153";
    public const string BANNER_ANDROID = "Banner_Android";
    public const string BANNER_IOS = "Banner_iOS";
    public const string REWARD_ANDROID = "BP_Rewarded_Android";
    public const string REWARD_IOS = "Rewarded_iOS";
}

public class AdManager : Singleton<AdManager>, IUnityAdsInitializationListener
{
    private string _bannerAdUnitId = null; // This will remain null for unsupported platforms.
    private string _rewardAdUnitId = null;

    public string BannerId => _bannerAdUnitId;
    public string RewardId => _rewardAdUnitId;

    private BannerAdHandler _bannerAdHandler;
    private RewardedAdHandler _rewardedAdHandler;

    new void Awake()
    {
        base.Awake();
        #if UNITY_IOS
        _bannerAdUnitId = AdvertisementIDs.BANNER_IOS;
        _rewardAdUnitId = AdvertisementIDs.REWARD_IOS;
        #elif UNITY_ANDROID || UNITY_EDITOR
        _bannerAdUnitId = AdvertisementIDs.BANNER_ANDROID;
        _rewardAdUnitId = AdvertisementIDs.REWARD_ANDROID;
        #endif
        
        // Initialize ads in test mode for expediency of debugging
        Advertisement.Initialize(AdvertisementIDs.GAME_ID, true, this);
    }

    private void Start()
    {
        _bannerAdHandler = GetComponent<BannerAdHandler>();
        _rewardedAdHandler = GetComponent<RewardedAdHandler>();
        
        ScoreManager.Instance.onGameOver?.AddListener(LoadAds);
    }

    public void LoadAds()
    {
        _bannerAdHandler.LoadBanner();
        _rewardedAdHandler.LoadAd();
    }

    public void PlayRewardedAd()
    {
        _rewardedAdHandler.ShowAd();
    }

    public void DisplayBannerAd()
    {
        _bannerAdHandler.ShowBannerAd();
    }

    public void DEBUG_ShowAds()
    {
        PlayRewardedAd();
        DisplayBannerAd();
    }

    public void OnInitializationComplete()
    { }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Ad initialization failed: {message}");
    }
}
