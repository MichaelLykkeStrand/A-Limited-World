using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    private DayNightCycle dayNightCycle;
    private int daysPassed = 0;
    private readonly int daysToWin = 7;
    private bool victorious = false;

    private void Awake()
    {
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        dayNightCycle.onSunrise += CountDay;
    }

    private void CountDay()
    {
        daysPassed++;
        if (daysPassed >= daysToWin && !victorious)
        {
            GameManager.Instance.UpdateGameState(GameState.Victory);
            victorious = true;
        }
    }

    private void OnDisable()
    {
        dayNightCycle.onSunrise -= CountDay;
    }


}
