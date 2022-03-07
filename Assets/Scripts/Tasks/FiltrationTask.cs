using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FiltrationTask : AbstractTask
{
    [SerializeField] HoldButton holdButton;
    [SerializeField] Image filterImage;
    [SerializeField] Sprite filterOff;
    [SerializeField] Sprite filterOn;
    [SerializeField] Image dirtyWater;
    [SerializeField] Image cleanWater;
    [SerializeField] float serverOfflineMinIdleTime = 5f;
    [SerializeField] float serverOfflineMaxIdleTime = 10f;
    bool filtrationComplete = false;
    private WaterFountain waterFountain;
    private ServerUpdateTask server;
    public static Action waterSupplied;


    protected override void Awake()
    {
        base.Awake();
        server = FindObjectOfType<ServerUpdateTask>();
        waterFountain = FindObjectOfType<WaterFountain>();
        dirtyWater.fillAmount = 1;
        cleanWater.fillAmount = 0;
    }
    protected override void Update()
    {
        base.Update();
        minIdleTime = server.serverOnline ? minIdleTime : serverOfflineMinIdleTime;
        maxIdleTime = server.serverOnline ? maxIdleTime : serverOfflineMaxIdleTime;
        if (!TaskActive) { return; }
        if (holdButton.pointerDown)
        {
            FilterWater();
        }
        else
        {
            AddDirtyWater();
        }

        CompleteTaskOnFullCleanWater();
    }

    private void CompleteTaskOnFullCleanWater()
    {
        if (cleanWater.fillAmount >= 1 && !filtrationComplete)
        {
            filtrationComplete = true;
            waterSupplied?.Invoke();
            Complete();
        }
    }

    protected override void ToggleTaskActive()
    {
        timeSinceTaskCompleted += Time.deltaTime;
        if (timeSinceTaskCompleted >= randomIdleTime && !TaskActive && waterFountain.usedSinceFilter)
        {
            TaskActive = true;
            audioSource.PlayOneShot(taskActive);
            taskCallback.OnActiveTask(this);
            waterFountain.usedSinceFilter = false;
        }
    }

    private void AddDirtyWater()
    {
        filterImage.sprite = filterOff;
        if (dirtyWater.fillAmount < 1)
        {
            dirtyWater.fillAmount += 0.4f * Time.deltaTime;
        }
        if (cleanWater.fillAmount > 0)
        {
            cleanWater.fillAmount -= 0.4f * Time.deltaTime;
        }
    }

    private void FilterWater()
    {
        filterImage.sprite = filterOn;
        if (dirtyWater.fillAmount > 0)
        {
            dirtyWater.fillAmount -= 0.4f * Time.deltaTime;
        }
        if (cleanWater.fillAmount < 1)
        {
            cleanWater.fillAmount += 0.4f * Time.deltaTime;
        }
    }

    public override void Reset()
    {
        dirtyWater.fillAmount = 1;
        cleanWater.fillAmount = 0;
        filterImage.sprite = filterOff;
        filtrationComplete = false;
    }
}
