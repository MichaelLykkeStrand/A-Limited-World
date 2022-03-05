using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour, ITaskCallback
{
    [SerializeField] Button useButton;
    AbstractTask closestTask;
    PlayerMovement playerMovement;
    [SerializeField] Canvas hud;
    [SerializeField] CanvasGroup taskCompletedText;
    [SerializeField] GameObject pointerPrefab;
    private List<AbstractTask> activeTasksInRange;
    Dictionary<AbstractTask, TaskPointer> taskPointers;
    private bool taskOpen = false;
    private Fader fader;

    // Timer for failing task if its an emergency

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        activeTasksInRange = new List<AbstractTask>();
        taskPointers = new Dictionary<AbstractTask, TaskPointer>();
        useButton.onClick.AddListener(OpenClosestTask);
        HideTaskCompletedTextOnAwake();
        InitializeFader();
    }

    private void InitializeFader()
    {
        fader = FindObjectOfType<Fader>();
        if (fader == null) { Debug.LogError("Task controller missing Fader. Add the fader prefab to this scene."); }
    }

    private void HideTaskCompletedTextOnAwake()
    {
        taskCompletedText.alpha = 0;
        taskCompletedText.interactable = false;
        taskCompletedText.blocksRaycasts = false;
    }

    void Update()
    {
        useButton.interactable = activeTasksInRange.Count > 0;
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
        foreach (AbstractTask task in activeTasksInRange)
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
        activeTasksInRange.Add(task);
    }

    public void OnExitRange(AbstractTask task)
    {
        activeTasksInRange.Remove(task);
    }

    public void OnClosedTask(AbstractTask task)
    {
        playerMovement.canMove = true;
        taskOpen = false;
    }

    public void OnCompleteTask(AbstractTask task)
    {
        activeTasksInRange.Remove(task);
        taskPointers.TryGetValue(task, out TaskPointer pointer);
        Debug.Log("pointer " + pointer);
        Destroy(pointer.gameObject);
        taskPointers.Remove(task);
        task.Close();
        task.Reset();

        StartCoroutine(fader.FadeInOut(taskCompletedText, 1f));
    }

    public void OnActiveTask(AbstractTask task)
    {
        Debug.Log("Task activated");
        GameObject pointer = Instantiate(pointerPrefab, hud.transform);
        pointer.GetComponent<TaskPointer>().SetTarget(task.transform.position);
        taskPointers.Add(task, pointer.GetComponent<TaskPointer>());
    }
}