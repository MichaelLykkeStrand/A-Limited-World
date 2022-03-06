using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] protected float range;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;

    [SerializeField] protected float attackRadius = 0.5f;

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

    public float GetAttackRadius(){
        return attackRadius;
    }
}
