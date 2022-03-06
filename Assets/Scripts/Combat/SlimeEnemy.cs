using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WeaponController), typeof(FollowTarget), typeof(MeleeWeapon))]
public class SlimeEnemy : Enemy
{
    // Start is called before the first frame update
    private Transform target;
    private WeaponController weaponController;
    private MeleeWeapon weapon;
    void Start()
    {
        target = GetComponent<FollowTarget>().GetTarget();
        weaponController = GetComponent<WeaponController>();
        weapon = GetComponent<MeleeWeapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, target.position) <= weapon.GetRange()+0.5f){
            weaponController.Attack(target.position);
        }
    }
}
