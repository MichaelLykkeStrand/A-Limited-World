using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureContainer : Container
{
    [SerializeField] Thermometer thermometer;
    [SerializeField] float nightHeatLossRate = 3f;
    [SerializeField] float dayHeatLossRate = 6f;
    [SerializeField] float temperatureHealthLossRate = 2f;
    private float timeSinceTemperatureHealthLoss = Mathf.Infinity;
    private float currentHeatLossRate;
    private DayNightCycle dayNightCycle;
    private HealthContainer health;
    public bool beingHeated = false;
    public bool freezing { get; private set; } = false;
    public bool burning { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        if (dayNightCycle == null) { Debug.LogError("Temperature needs a DayNightCycle in scene."); }
        dayNightCycle.onSunrise += SwitchToDayHeatLoss;
        dayNightCycle.onSunset += SwitchToNightHeatLoss;
        health = GetComponent<HealthContainer>();
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
                health.Subtract(1);
            }
        }
    }

    private void Update()
    {
        if (burning || freezing)
        {
            timeSinceTemperatureHealthLoss += Time.deltaTime;
            if (timeSinceTemperatureHealthLoss >= temperatureHealthLossRate)
            {
                Subtract(1);
                timeSinceTemperatureHealthLoss = 0;
            }
        }
    }
    public override void Add(int addValue)
    {
        base.Add(addValue);
        UpdateTemperatureStatus();
    }

    public override void Subtract(int subValue)
    {
        base.Subtract(subValue);
        UpdateTemperatureStatus();
    }

    private void UpdateTemperatureStatus()
    {
        if (value == 0)
        {
            freezing = true;
        }
        else if (value >= maxValue)
        {
            burning = true;
        }
        else
        {
            freezing = false;
            burning = false;
        }
    }

    private void OnDisable()
    {
        dayNightCycle.onSunrise -= SwitchToDayHeatLoss;
        dayNightCycle.onSunset -= SwitchToNightHeatLoss;
    }
}
