using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour, ITaskCallback
{
    [SerializeField] Button useButton;
    AbstractTask closestTask;
    PlayerMovement playerMovement;
    private List<AbstractTask> tasksInRange;
    private bool taskOpen = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        tasksInRange = new List<AbstractTask>();
        useButton.onClick.AddListener(OpenClosestTask);
    }

    void Update()
    {
        useButton.interactable = tasksInRange.Count > 0;
    }

    private void OpenClosestTask()
    {
        if (taskOpen) { return; }
        SetClosestTask();
        closestTask.Open();
        taskOpen = true;
    }

    private void SetClosestTask()
    {
        float closest = Mathf.Infinity;
        foreach (AbstractTask task in tasksInRange)
        {
            if (Vector2.Distance(transform.position, task.transform.position) < closest)
            {
                closestTask = task;
                closest = Vector2.Distance(transform.position, task.transform.position);
            }
        }
    }

    public void OnOpenTask(AbstractTask task)
    {
        playerMovement.canMove = task.MovementRequired;
    }

    public void OnEnterRange(AbstractTask task)
    {
        tasksInRange.Add(task);
    }

    public void OnExitRange(AbstractTask task)
    {
        tasksInRange.Remove(task);
    }

    public void OnClosedTask(AbstractTask task)
    {
        playerMovement.canMove = true;
        taskOpen = false;
    }

    public void OnCompleteTask(AbstractTask task)
    {
        task.Close();
    }
}