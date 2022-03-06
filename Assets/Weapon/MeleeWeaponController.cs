using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : WeaponController
{
    // Start is called before the first frame update
    private bool isOnCooldown = false;
    private MeleeWeapon meleeWeapon;
    [SerializeField] private GameObject attackAnimationPrefab;

    void Start()
    {
        meleeWeapon = GetComponent<MeleeWeapon>();
    }

    // Update is called once per frame
    public override void Attack(Vector2 target)
    {
        if (!isOnCooldown)
        {
            Vector2 dirToTarget = target - (Vector2)transform.position;
            dirToTarget.Normalize();
            Vector2 spawnPos = (Vector2)transform.position+(dirToTarget * meleeWeapon.GetRange());
            //Spawn attack object - Animation object
            GameObject attackObject = Instantiate(attackAnimationPrefab);
            attackObject.transform.position = spawnPos;
            attackObject.transform.right = (Vector2)spawnPos - (Vector2)transform.position;
            attackObject.transform.parent = this.transform;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, meleeWeapon.GetAttackRadius(),dirToTarget,meleeWeapon.GetRange());
            foreach(var hit in hits){
                if(hit.transform == this.transform) continue;
                HealthContainer health = hit.transform.GetComponent<HealthContainer>();
                if(health != null){
                    health.Subtract(meleeWeapon.GetDamage());
                } 
            }
            meleeWeapon.Use();
            StartCoroutine(CooldownCoroutine());
        }
    }

    IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(meleeWeapon.GetCooldownTime());
        isOnCooldown = false;
    }
}
