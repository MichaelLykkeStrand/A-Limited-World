using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainer : Container
{
    private bool isDead = false;

    private void Update()
    {
        if (isDead || GameManager.Instance.State != GameState.Core) { return; }


        if (value == 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Defeat);
            isDead = true;
        }
    }
}
