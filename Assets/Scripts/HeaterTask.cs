using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaterTask : AbstractTask 
{
    [SerializeField] Image heater;
    [SerializeField] Sprite coldHeater;
    [SerializeField] Sprite hotHeater;
    [SerializeField] Dragger dragger;
    private bool placed = false;
    
    public void OnFusePlaced()
    {
        if (placed) { return; }
        StartCoroutine(DisplayHotHeaterForTime());
        placed = true;
    }

    private IEnumerator DisplayHotHeaterForTime()
    {
        heater.sprite = hotHeater;
        yield return new WaitForSeconds(1f);
        Complete();
    }

    public override void Reset()
    {
        heater.sprite = coldHeater;
        dragger.ResetPosition();
        placed = false;
    }
}
