using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowTarget : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 3;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!Physics2D.Linecast(transform.position,target.position)) return;
        Vector2 moveDir = target.position - transform.position;
        rb.MovePosition(rb.position + (moveSpeed * Time.fixedDeltaTime * moveDir.normalized));
    }

    public Transform GetTarget(){
        return this.target;
    }

    public void SetTarget(Transform newTarget){
        this.target = newTarget;
    }
}
