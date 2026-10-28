using UnityEngine;

using Firebase;
using Firebase.Analytics;
using Firebase.Database;

using System;

public class FirebaseManager : Singleton<FirebaseManager>
{
    // - Firebase Elements
    private bool firebaseReady = false;
    private DatabaseReference database;

    private async void Start()
    {
        var status = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (status == DependencyStatus.Available)
        {
            // - Initialize the firebase app
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // - Begin collecting analytics from out game on Firebase
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            // - Initialize / Connect to Database
            FirebaseDatabase db = FirebaseDatabase.GetInstance(app, "https://unit5firebasesample-c891e-default-rtdb.firebaseio.com/");
            database = db.RootReference;

            firebaseReady = true;
            Debug.Log("Firebase Analytics Initialized");
        }

        else
        {
            Debug.Log($"Firebase initialization failed: {status}");
        }
    }


    #region Analytics Events

    public void FirebaseEvent_LoginEvent()
    {
        if (!firebaseReady)
            return;

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
        Debug.Log("Login Event Complete");
    }

    public void FirebaseEvent_Float()
    {
        if (!firebaseReady)
            return;

        FirebaseAnalytics.LogEvent("experience", "percentage", 0.4f);
        Debug.Log("Float Event Complete!");
    }

    public void FirebaseEvent_Int()
    {
        if (!firebaseReady)
            return;

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPostScore, FirebaseAnalytics.ParameterScore, 42);
        Debug.Log("Float Event Complete!");
    }

    public void FirebaseEvent_String()
    {
        if (!firebaseReady)
            return;

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventJoinGroup, FirebaseAnalytics.ParameterGroupID, "spoon_welders");
        Debug.Log("Float Event Complete!");
    }

    public void FirebaseEvent_MultiParam()
    {
        if (!firebaseReady)
            return;

        Parameter[] my_params =
        {
            new Parameter(FirebaseAnalytics.ParameterLevel, 5),
            new Parameter(FirebaseAnalytics.ParameterCharacter, "mrspoon"),
            new Parameter("experience_earned", 100)
        };

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, my_params);
        Debug.Log("Float Event Complete!");
    }

    #endregion

    #region Realtime Database

    public async void SavePlayer()
    {
        if (!firebaseReady)
            return;

        try
        {
            await database.Child("players").Child("player001").Child("name").SetValueAsync("Cody");
            await database.Child("players").Child("player001").Child("score").SetValueAsync(10);

            Debug.Log("Successfully saved player");
        }

        catch (Exception e)
        {
            Debug.LogError("Save Failed: " + e.Message);
        }
    }

    public async void LoadPlayer()
    {
        if (!firebaseReady)
            return;

        try
        {
            DataSnapshot snapshot = await database.Child("players").Child("player001").GetValueAsync();

            if (snapshot.Exists)
            {
                Debug.Log("Player Data: " + snapshot.GetRawJsonValue());
            }
            else
            {
                Debug.Log("Failed to find player");
            }
        }

        catch (Exception e)
        {
            Debug.LogError("Load Failed: " + e.Message);
        }
    }

    public async void AddScore()
    {
        if (!firebaseReady)
            return;

        try
        {
            DatabaseReference scoreRef = database.Child("players").Child("player001").Child("score");

            await scoreRef.RunTransaction(mutableData =>
            {
                long currentScore = 0;

                if (mutableData.Value != null)
                {
                    currentScore = Convert.ToInt64(mutableData.Value);
                }

                mutableData.Value = currentScore + 10;

                return TransactionResult.Success(mutableData);
            });

            Debug.Log("Added 10 points, yay.");
        }

        catch (Exception e)
        {
            Debug.LogError("Score Change Failed: " + e.Message);
        }
    }

    public async void DeletePlayer()
    {
        if (!firebaseReady)
            return;

        try
        {
            await database.Child("players").Child("player001").RemoveValueAsync();

            Debug.Log("Successfully deleted player data");
        }

        catch (Exception e)
        {
            Debug.LogError("Delete Player Failed: " + e.Message);
        }
    }

    #endregion

}
