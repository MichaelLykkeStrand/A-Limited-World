using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public abstract class AbstractTask : MonoBehaviour
{
    [SerializeField] GameObject task;
    [SerializeField] float interactableRadius = 2f;
    [SerializeField] float minIdleTime = 5f;
    [SerializeField] float maxIdleTime = 10f;
    [SerializeField] AudioClip taskActive;
    [SerializeField] AudioClip taskComplete;
    protected AudioSource audioSource;

    private float timeSinceTaskCompleted = 0;
    private float randomIdleTime;
    public bool TaskActive { get; protected set; } = false;
    public bool MovementRequired { get; protected set; } = false;
    private ITaskCallback taskCallback;
    protected Transform player;

    private bool playerAlreadyInRange = false;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        taskCallback = GameObject.FindGameObjectWithTag("Player").GetComponent<ITaskCallback>();
        task.SetActive(false);
        randomIdleTime = UnityEngine.Random.Range(minIdleTime, maxIdleTime);
    }
    protected virtual void Update()
    {
        HandleEnterRange();
        HandleExitRange();

        timeSinceTaskCompleted += Time.deltaTime;
        if (timeSinceTaskCompleted >= randomIdleTime && !TaskActive)
        {
            TaskActive = true;
            audioSource.PlayOneShot(taskActive);
            taskCallback.OnActiveTask(this);
        }

    }

    private void HandleEnterRange()
    {
        if (InRangeOfPlayer() && !playerAlreadyInRange)
        {
            taskCallback.OnEnterRange(this);
            playerAlreadyInRange = true;
        }
    }

    private void HandleExitRange()
    {
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

    protected void Complete()
    {
        taskCallback.OnCompleteTask(this);
        TaskActive = false;
        timeSinceTaskCompleted = 0;
        audioSource.PlayOneShot(taskComplete);
    }

    public virtual void Reset()
    {
        
    }

    private bool InRangeOfPlayer() => Vector2.Distance(transform.position, player.position) <= interactableRadius && TaskActive;
}
