using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainer : Container
{
    [SerializeField] float temperatureHealthLossRate = 2f;
    private float timeSinceTemperatureHealthLoss = Mathf.Infinity;
    private TemperatureContainer temperatureContainer;
    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        temperatureContainer = FindObjectOfType<TemperatureContainer>();
    }

    private void Update()
    {
        if (isDead || GameManager.Instance.State != GameState.Core) { return; }
        if (temperatureContainer.burning || temperatureContainer.freezing)
        {
            timeSinceTemperatureHealthLoss += Time.deltaTime;
            if (timeSinceTemperatureHealthLoss >= temperatureHealthLossRate)
            {
                Subtract(1);
                timeSinceTemperatureHealthLoss = 0;
            }
        }

        if (value == 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Defeat);
            isDead = true;
        }
    }
}
