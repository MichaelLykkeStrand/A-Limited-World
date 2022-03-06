using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] protected float range;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;

    public float GetRange()
    {
        return range;
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetCooldownTime(){
        return cooldown;
    }
}
