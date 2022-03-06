using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeapon : Weapon
{
    [SerializeField] public float range {get; protected set;}
    [SerializeField] public int damage {get; protected set;}
    [SerializeField] public float cooldown {get; protected set;}
}
