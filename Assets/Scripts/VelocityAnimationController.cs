using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer),typeof(Animator))]
public class VelocityAnimationController : MonoBehaviour
{
    Vector3 prevPos, velocity;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] float xDeadzone = 0.1f;
    private void Awake()
    {
        prevPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        velocity = (transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;
        if (velocity.x < -xDeadzone)
        {
            spriteRenderer.flipX = true;
        }
        else if(velocity.x > xDeadzone)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("Velocity", velocity.magnitude);
    }
}
