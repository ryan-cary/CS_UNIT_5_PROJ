using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdHandler : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private float timeToAddOnContinue = 30;
    
    private string _adUnitId;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AdManager.Instance.RewardId == null)
        {
            _adUnitId = AdvertisementIDs.REWARD_ANDROID;
            Debug.Log("Rewarded Ad: No placement specified; using android placement ID as default");
        }
        else
            _adUnitId = AdManager.Instance.RewardId;
    }

    public void LoadAd()
    {
        //Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(_adUnitId))
        {
            Debug.Log("Ad Loaded: " + placementId);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    { }

    public void OnUnityAdsShowClick(string placementId)
    { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            ScoreManager.Instance.AddTime(timeToAddOnContinue);
        }
    }
}
