using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public abstract class WeaponController : MonoBehaviour
{
    public abstract void Attack(Vector2 target);
}
