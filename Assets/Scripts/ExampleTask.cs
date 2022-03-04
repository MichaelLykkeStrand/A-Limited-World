using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTask : MonoBehaviour, IWindow
{
    [SerializeField] CanvasGroup taskCanvas;

    private void Awake()
    {
        Close();
    }

    public void Open()
    {
        taskCanvas.alpha = 1;
        taskCanvas.interactable = true;
        taskCanvas.blocksRaycasts = true;
    }

    public void Close()
    {
        taskCanvas.alpha = 0;
        taskCanvas.interactable = false;
        taskCanvas.blocksRaycasts = false;
    }

}
