using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureContainer : Container
{
    [SerializeField] Thermometer thermometer;
    [SerializeField] float heatLossRate = 5f;

    private void Start()
    {
        maxValue = 12;
        value = 6;
        thermometer.UpdateThermometer(value);
        StartCoroutine(LoseHeat());
    }

    private IEnumerator LoseHeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(heatLossRate);
            Subtract(1);
            thermometer.UpdateThermometer(value);
        }
    }
}
