using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    AnimationDelay animationDelay;
    RandomAudioPlayer audioPlayer;
    void Start()
    {
        audioPlayer = GetComponent<RandomAudioPlayer>();
        animationDelay = GetComponent<AnimationDelay>();
        animationDelay.onAnimationPlay += OnAnimationPlayed;
    }

    private void OnAnimationPlayed()
    {
        audioPlayer.Play("Fish Splash");
    }
}
