using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RandomAudioPlayer))]
public abstract class Weapon : MonoBehaviour
{
    public event Action onUse;
    protected RandomAudioPlayer randomAudioPlayer;
    protected void Awake(){
        randomAudioPlayer = GetComponent<RandomAudioPlayer>();
    }

    public virtual void Use(){
        randomAudioPlayer.Play("WeaponUse");
        onUse?.Invoke();
    }
}
