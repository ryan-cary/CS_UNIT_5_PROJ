using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip bombSound;

    private AudioSource _audio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        ScoreManager.Instance.onBombSliced.AddListener(PlayBombSound);
    }

    private void PlayBombSound()
    {
        _audio.PlayOneShot(bombSound);
    }
}
