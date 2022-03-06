using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    [SerializeField] CanvasGroup victoryCanvas;
    [SerializeField] CanvasGroup defeatCanvas;
    [SerializeField] CanvasGroup darknessCanvas;

    private Fader fader;

    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        fader = FindObjectOfType<Fader>();
        darknessCanvas.alpha = 1;

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu") 
        { 
            UpdateGameState(GameState.Menu); 
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            UpdateGameState(GameState.Core);
            HideRunTimeCanvases();
        }
        StartCoroutine(fader.FadeOutElement(darknessCanvas, 1));
    }

    private void HideRunTimeCanvases()
    {
        victoryCanvas.alpha = 0;
        victoryCanvas.blocksRaycasts = false;
        victoryCanvas.interactable = false;
        defeatCanvas.alpha = 0;
        defeatCanvas.blocksRaycasts = false;
        defeatCanvas.interactable = false;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Core:
                break;
            case GameState.Menu:
                break;
            case GameState.Victory:
                OnVictory();
                break;
            case GameState.Defeat:
                OnDefeat();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void Play()
    {
        StartCoroutine(LoadMainScene());
    }

    public IEnumerator LoadMenu()
    {
        yield return StartCoroutine(fader.FadeInElement(darknessCanvas, 1));
        SceneManager.LoadScene(0);
    }
    
    private void OnVictory()
    {
        StartCoroutine(fader.FadeInElement(victoryCanvas, 1));
        victoryCanvas.blocksRaycasts = true;
        victoryCanvas.interactable = true;
    }

    private void OnDefeat()
    {
        StartCoroutine(fader.FadeInElement(defeatCanvas, 1));
        defeatCanvas.blocksRaycasts = true;
        defeatCanvas.interactable = true;
    }

    public IEnumerator LoadMainScene()
    {
        yield return StartCoroutine(fader.FadeInElement(darknessCanvas, 1));
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public enum GameState
{
    Menu,
    Core,
    Victory,
    Defeat
}
