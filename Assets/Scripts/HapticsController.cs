using UnityEngine;

public class HapticsController : MonoBehaviour
{
    private int GetAndroidVersion()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT")
        }
#else
        return 0;
#endif
    }
    
    // Unity Vibration
    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    public void Vibrate(int strength)
    {
        VibrateAndroid(100, strength);
    }
    
    // Android vibration
    public void VibrateAndroid(long duration, int strength)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject vibrator = activity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
        {
            if (vibrator == null || !vibrator.Call<bool>("hasVibrator"))
                return;

            int sdk_version = GetAndroidVersion();

            if (sdkVersion >= 26)
            {
                using (AndroidJavaClass effect = new AndroidJavaClass("android.os.VibrationEffect"))
                using (AndroidJavaObject vibration = effect.CallStatic<AndroidJavaObject>("createOneShot", duration, Mathf.Clamp(strength, 1, 255)))
                {
                    vibrator.Call("vibrate", vibration);
                }
            }
            else 
            {
                vibrator.Call("vibrate", duration);
            }
        }
#endif
    }

    #region Notifications

    

    #endregion
}
