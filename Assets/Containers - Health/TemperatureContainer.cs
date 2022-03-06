using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureContainer : Container
{
    [SerializeField] Thermometer thermometer;
    [SerializeField] float nightHeatLossRate = 3f;
    [SerializeField] float dayHeatLossRate = 6f;
    private float currentHeatLossRate;
    private DayNightCycle dayNightCycle;
    public bool beingHeated = false;

    protected override void Awake()
    {
        base.Awake();
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        if (dayNightCycle == null) { Debug.LogError("Temperature needs a DayNightCycle in scene."); }
        dayNightCycle.onSunrise += SwitchToDayHeatLoss;
        dayNightCycle.onSunset += SwitchToNightHeatLoss;
    }
    private void Start()
    {
        value = 6;
        currentHeatLossRate = dayHeatLossRate;
        thermometer.UpdateThermometer();
        StartCoroutine(LoseHeat());
    }

    private void SwitchToDayHeatLoss() => currentHeatLossRate = dayHeatLossRate;
    private void SwitchToNightHeatLoss() => currentHeatLossRate = nightHeatLossRate;

    private IEnumerator LoseHeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentHeatLossRate);
            if (!beingHeated)
            {
                Subtract(1);
                Debug.Log("Temperature decreased to " + value);
            }
        }
    }


    private void OnDisable()
    {
        dayNightCycle.onSunrise -= SwitchToDayHeatLoss;
        dayNightCycle.onSunset -= SwitchToNightHeatLoss;
    }
}
