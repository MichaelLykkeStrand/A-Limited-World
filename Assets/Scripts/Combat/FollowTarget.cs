using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowTarget : MonoBehaviour
{

    private Transform target;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] NavMeshQueryFilter filter;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    protected Transform player;
    public bool ready = false;
    private MeleeWeapon weapon;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weapon = GetComponent<MeleeWeapon>();
        target = player;
        if (agent.enabled && !agent.isOnNavMesh)
        {
            var position = transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(position, out hit, 10.0f, 0);
            position = hit.position; // usually this barely changes, if at all
            agent.Warp(position);
        }
        agent.stoppingDistance = weapon.GetRange();
    }

    private void Update()
    {
        if (ready)
        {
            agent.SetDestination(player.position);
        }
    }

    public Transform GetTarget(){
        return this.target;
    }

    public void SetTarget(Transform newTarget){
        this.target = newTarget;
    }
}
