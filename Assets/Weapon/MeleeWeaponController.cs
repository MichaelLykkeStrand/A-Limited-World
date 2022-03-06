using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class MeleeWeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOnCooldown = true;
    private MeleWeapon meleWeapon;
    private GameObject attackAnimationPrefab;
    [SerializeField] private float attackRadius = 0.5f;
    void Start()
    {
        meleWeapon = GetComponent<MeleWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isOnCooldown)
        {
            //Get the position of this object in screen-coordinates
            Vector3 posInScreen = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector3 dirToMouse = Input.mousePosition - posInScreen;
            float zRotation = Mathf.Atan2(dirToMouse.y, dirToMouse.x)*Mathf.Rad2Deg;
            dirToMouse.Normalize();
            Vector3 spawnPos = transform.position+(dirToMouse * meleWeapon.range);
            //Spawn attack object - Animation object
            GameObject attackObject = Instantiate(attackAnimationPrefab);
            attackObject.transform.position = spawnPos;
            
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRadius,dirToMouse,meleWeapon.range);
            foreach(var hit in hits){
                if(hit.transform == this) continue;
                HealthContainer health = hit.transform.GetComponent<HealthContainer>();
                health.Subtract(meleWeapon.damage);
            }
            meleWeapon.Attack();
            StartCoroutine(CooldownCoroutine());
        }
    }

    IEnumerator CooldownCoroutine()
    {
        isOnCooldown = false;
        yield return new WaitForSeconds(meleWeapon.cooldown);
        isOnCooldown = true;
    }
}
