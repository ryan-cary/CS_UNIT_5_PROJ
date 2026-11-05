using System;
using System.Collections;
using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class HapticsController : Singleton<HapticsController>
{
    private void Start()
    {
        InitializeNotificationsChannel();
    }

    #region Vibration

    private int GetAndroidVersion()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
#else
        return 0;
#endif
    }
    
    // Unity Vibration
    public void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR        
        Handheld.Vibrate();
#endif
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

            if (sdk_version >= 26)
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

    #endregion

    #region Notifications

    private const string ChannelId = "game_notifications";

    private void InitializeNotificationsChannel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Id = ChannelId,
            Name = "Game Notifications",
            Importance = Importance.Default,
            Description = "Notifications for the game."
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        if (GetAndroidVersion() > 13)
        {
            StartCoroutine(RequestPermission());
        }
#endif
    }

    public void RequestNotificationPermissions()
    {
        StartCoroutine(RequestPermission());
    }

    private IEnumerator RequestPermission()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        PermissionRequest request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
        {
            yield return null;
        }

        Debug.Log("Notification Permission: " + request.Status);
#else
        return null;
#endif
    }

    public void SendToast(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject activity = AndroidApplication.currentActivity;
        
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                using (AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast"))
                using (AndroidJavaObject toast =
                       toastClass.CallStatic<AndroidJavaObject>("makeText", activity, message, 0))
                {
                    toast.Call("show");
                }
            }
            ));
#endif
    }

    public void SendNotification()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Debug Test",
            Text = "This is a test! Do not be alarmed.",
            FireTime = DateTime.Now.AddSeconds(5)
        };
#endif
    }

    public void SendNotification(string title, string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidNotification notif = new AndroidNotification()
        {
            Title = title,
            Text = message,
            FireTime = DateTime.Now
        };
#endif
    }

    public void SendHighScoreNotification(int score)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidNotification highScoreNotif = new AndroidNotification()
        {
            Title = "High Score!",
            Text = $"You've achieved a high score of {score}! Way to go!",
            FireTime = DateTime.Now
        };
#endif
    }

    #endregion
}
