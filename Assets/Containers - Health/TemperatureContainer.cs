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
        maxValue = 12;
        value = 6;
        currentHeatLossRate = dayHeatLossRate;
        thermometer.UpdateThermometer(value);
        StartCoroutine(LoseHeat());
    }

    private void SwitchToDayHeatLoss() => currentHeatLossRate = dayHeatLossRate;
    private void SwitchToNightHeatLoss() => currentHeatLossRate = nightHeatLossRate;

    private IEnumerator LoseHeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentHeatLossRate);
            Subtract(1);
            thermometer.UpdateThermometer(value);
        }
    }

    private void OnDisable()
    {
        dayNightCycle.onSunrise -= SwitchToDayHeatLoss;
        dayNightCycle.onSunset -= SwitchToNightHeatLoss;
    }
}
