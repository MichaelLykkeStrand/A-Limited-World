using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEmptyContainer : MonoBehaviour
{
    [SerializeField] private Container container;
    [SerializeField] private GameObject animationObject;
    // Start is called before the first frame update
    void Start()
    {
        container.OnEmpty += handleEmptyContainer;
    }
    
    private void handleEmptyContainer(){
        try
        {
            GameObject clone = Instantiate(animationObject);
            animationObject.AddComponent<Despawn>();
            clone.transform.position = this.transform.position;
        }
        catch (System.Exception)
        {

        }
        Destroy(this.gameObject);
    }
}
