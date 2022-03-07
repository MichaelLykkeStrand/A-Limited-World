using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] CanvasGroup menuCanvas;
    [SerializeField] CanvasGroup tutorialCanvas;

    private void Start()
    {
        OpenMenu();
    }
    public void OpenTutorial()
    {
        menuCanvas.alpha = 0;
        menuCanvas.interactable = false;
        menuCanvas.blocksRaycasts = false;
        tutorialCanvas.alpha = 1;
        tutorialCanvas.interactable = true;
        tutorialCanvas.blocksRaycasts = true;
    }

    public void OpenMenu()
    {
        tutorialCanvas.alpha = 0;
        tutorialCanvas.interactable = false;
        tutorialCanvas.blocksRaycasts = false;
        menuCanvas.alpha = 1;
        menuCanvas.interactable = true;
        menuCanvas.blocksRaycasts = true;
    }
}
