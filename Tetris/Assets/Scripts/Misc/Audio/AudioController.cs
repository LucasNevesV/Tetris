using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    AudioSource audioPlayer;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayerMove()
    {
        var audio = Resources.Load<AudioClip>(AudioClips.Move);

        PlayAudio(audio);
    }

    public void PlayerRotate()
    {
        var audio = Resources.Load<AudioClip>(AudioClips.Rotate);
        
        PlayAudio(audio);
    }

    public void RowCleared()
    {
        var audio = Resources.Load<AudioClip>(AudioClips.RowCleared);

        PlayAudio(audio);
    }

    void PlayAudio(AudioClip audio)
    {
        audioPlayer.clip = audio;
        audioPlayer.Play();
    }
}
