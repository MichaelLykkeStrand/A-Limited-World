using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField, Min(0)] private float despawnTime = 1f;

    private void Awake(){
        StartCoroutine(CooldownCoroutine());
    }
    // Update is called once per frame
    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
    }
}
