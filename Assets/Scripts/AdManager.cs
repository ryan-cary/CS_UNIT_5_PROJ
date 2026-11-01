using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_RIGHT;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #if UNITY_IOS
        _adUnityId = _iOSAdUnitId;
        #elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
