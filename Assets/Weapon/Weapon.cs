using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public event Action onAttack;

    public virtual void Attack(){
        onAttack?.Invoke();
    }
}
