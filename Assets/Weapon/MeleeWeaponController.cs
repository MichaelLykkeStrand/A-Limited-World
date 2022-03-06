using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class MeleeWeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOnCooldown = false;
    private MeleeWeapon meleWeapon;
    [SerializeField] private GameObject attackAnimationPrefab;
    [SerializeField] private float attackRadius = 0.5f;
    void Start()
    {
        meleWeapon = GetComponent<MeleeWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOnCooldown)
        {
            //Get the position of this object in screen-coordinates
            Vector3 posInScreen = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector3 dirToMouse = Input.mousePosition - posInScreen;
            dirToMouse.Normalize();
            Vector3 spawnPos = transform.position+(dirToMouse * meleWeapon.GetRange());
            //Spawn attack object - Animation object
            GameObject attackObject = Instantiate(attackAnimationPrefab);
            attackObject.transform.position = spawnPos;
            attackObject.transform.right = (Vector2)spawnPos - (Vector2)transform.position;
            attackObject.transform.parent = this.transform;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRadius,dirToMouse,meleWeapon.GetRange());
            foreach(var hit in hits){
                if(hit.transform == this) continue;
                HealthContainer health = hit.transform.GetComponent<HealthContainer>();
                if(health != null){
                    health.Subtract(meleWeapon.GetDamage());
                } 
            }
            meleWeapon.Attack();
            StartCoroutine(CooldownCoroutine());
        }
    }

    IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(meleWeapon.GetCooldownTime());
        isOnCooldown = false;
    }
}
