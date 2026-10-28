using System;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    private FirebaseManager _fbManager;

    [Header("Timed Mode - Player")]
    [SerializeField] private TMP_Text mostFruits_PLAYER_TIMED;
    [SerializeField] private TMP_Text mostBombs_PLAYER_TIMED;
    [SerializeField] private TMP_Text totalFruits_PLAYER_TIMED;
    [SerializeField] private TMP_Text totalbombs_PLAYER_TIMED;
    [SerializeField] private TMP_Text perfectGames_PLAYER_TIMED;
    [Header("Timed Mode - Top")]
    [SerializeField] private TMP_Text mostFruits_TOP_TIMED;
    [SerializeField] private TMP_Text mostBombs_TOP_TIMED;
    [SerializeField] private TMP_Text totalFruits_TOP_TIMED;
    [SerializeField] private TMP_Text totalbombs_TOP_TIMED;
    [SerializeField] private TMP_Text perfectGames_TOP_TIMED;
    
    [Header("Endless Mode - Player")] 
    [SerializeField] private TMP_Text mostFruits_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text mostBombs_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text totalFruits_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text totalbombs_PLAYER_ENDLESS;
    [SerializeField] private TMP_Text perfectGames_PLAYER_ENDLESS;
    [Header("Endless Mode - Top")] 
    [SerializeField] private TMP_Text mostFruits_TOP_ENDLESS;
    [SerializeField] private TMP_Text mostBombs_TOP_ENDLESS;
    [SerializeField] private TMP_Text totalFruits_TOP_ENDLESS;
    [SerializeField] private TMP_Text totalbombs_TOP_ENDLESS;
    [SerializeField] private TMP_Text perfectGames_TOP_ENDLESS;

    private void Awake()
    {
        _fbManager = FindFirstObjectByType<FirebaseManager>();
    }

    private void Start()
    {
        UpdateTimedMode();
        UpdateEndlessMode();
    }

    public void UpdateTimedMode()
    {
        
    }

    public void UpdateEndlessMode()
    {
        
    }
}
