using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdHandler : MonoBehaviour
{
    [SerializeField] private BannerPosition anchor = BannerPosition.TOP_LEFT;

    private string _adUnitId;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Advertisement.Banner.SetPosition(anchor);
        if (AdManager.Instance.BannerId == null)
        {
            _adUnitId = AdvertisementIDs.BANNER_ANDROID;
            Debug.Log("Banner Ad: No placement specified; Using android placement ID as default");
        }
        else
            _adUnitId = AdManager.Instance.BannerId;
    }
    
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner ad loaded!");
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }
    
    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
    
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}
