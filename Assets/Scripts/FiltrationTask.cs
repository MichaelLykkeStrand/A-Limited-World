using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltrationTask : AbstractTask
{
    [SerializeField] HoldButton holdButton;
    [SerializeField] Image filterImage;
    [SerializeField] Sprite filterOff;
    [SerializeField] Sprite filterOn;
    [SerializeField] Image dirtyWater;
    [SerializeField] Image cleanWater;
    bool filtrationComplete = false;

    protected override void Awake()
    {
        base.Awake();
        dirtyWater.fillAmount = 1;
        cleanWater.fillAmount = 0;
    }
    protected override void Update()
    {
        base.Update();
        if (!TaskActive) { return; }
        if (holdButton.pointerDown)
        {
            filterImage.sprite = filterOn;
            if (dirtyWater.fillAmount > 0)
            {
                dirtyWater.fillAmount -= Time.deltaTime;
            }
            if (cleanWater.fillAmount < 1)
            {
                cleanWater.fillAmount += Time.deltaTime;
            }
        }
        else
        {
            filterImage.sprite = filterOff;
            if (dirtyWater.fillAmount < 1)
            {
                dirtyWater.fillAmount += Time.deltaTime;
            }
            if (cleanWater.fillAmount > 0)
            {
                cleanWater.fillAmount -= Time.deltaTime;
            }
        }

        if (cleanWater.fillAmount >= 1 && !filtrationComplete)
        {
            filtrationComplete = true;
            Complete();
        }
    }

    public override void Reset()
    {
        
        dirtyWater.fillAmount = 1;
        cleanWater.fillAmount = 0;
        filterImage.sprite = filterOff;
        filtrationComplete = false;
        Debug.Log("Resetting");
    }
}
