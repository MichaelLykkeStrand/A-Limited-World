using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractTask : MonoBehaviour
{
    [SerializeField] GameObject task;
    [SerializeField] float interactableRadius = 2f;
    public bool MovementRequired { get; protected set; } = false;
    private ITaskCallback taskCallback;
    private Transform player;
    public Action OnFail;

    private bool playerAlreadyInRange = false;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        taskCallback = GameObject.FindGameObjectWithTag("Player").GetComponent<ITaskCallback>();
        task.SetActive(false);
    }
    private void Update()
    {
        if (InRangeOfPlayer() && !playerAlreadyInRange)
        {
            taskCallback.OnEnterRange(this);
            playerAlreadyInRange = true;
        }
        if (!InRangeOfPlayer() && playerAlreadyInRange)
        {
            taskCallback.OnExitRange(this);
            playerAlreadyInRange = false;
        }
    }

    public void Close()
    {
        taskCallback.OnClosedTask(this);
        task.SetActive(false);
    }

    public void Open()
    {
        taskCallback.OnOpenTask(this);
        task.SetActive(true);
    }

    protected void CompleteTask()
    {
        taskCallback.OnCompleteTask(this);        
    }

    private bool InRangeOfPlayer() => Vector2.Distance(transform.position, player.position) <= interactableRadius;
}
