using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class PlayerInputController : MonoBehaviour
{
    private WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        weaponController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            try
            {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            weaponController.Attack(mousepos);
            }
            catch (System.Exception)
            {

            }

        }
    }
}
