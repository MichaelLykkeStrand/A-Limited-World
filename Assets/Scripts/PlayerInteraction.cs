using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactableRadius = 2f;
    [SerializeField] Button useButton;
    IWindow closestTask;
    bool listeningForInteract = false;
   
    void Update()
    {
        useButton.interactable = TaskInRange();
        
        if (!TaskInRange()) 
        { 
            useButton.onClick.RemoveAllListeners();
            closestTask?.Close();
            listeningForInteract = false;
        }

        if (!listeningForInteract && TaskInRange())
        {
            useButton.onClick.AddListener(OpenClosestTask);
            listeningForInteract = true;
        }
        
    }

    private void OpenClosestTask()
    {
        closestTask.Open();
    }

    private bool TaskInRange()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, interactableRadius))
        {
            IWindow task = collider.gameObject.GetComponent<IWindow>();
            if (task != null) {
                closestTask = task;
                return true; 
            }
        }
        return false;
    }
}
