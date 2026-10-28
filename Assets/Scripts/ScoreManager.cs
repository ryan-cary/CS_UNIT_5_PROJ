using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float _timeElapsed;
    private int _fruitSliced;
    private int _fruitMissed;

    void Update()
    {
        _timeElapsed = Time.timeSinceLevelLoad;
    }
}
