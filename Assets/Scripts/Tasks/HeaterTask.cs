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
    [SerializeField] float heatRadius = 4f;
    private bool placed = false;
    TemperatureContainer playerTemperature;
    [SerializeField] float heatingRate = 2f;
    ServerUpdateTask server;
    private float timeSincePlayerHeated = Mathf.Infinity;

    protected override void Awake()
    {
        base.Awake();
        server = FindObjectOfType<ServerUpdateTask>();
    }

    private void Start()
    {
        playerTemperature = player.gameObject.GetComponent<TemperatureContainer>();
    }

    public void OnFusePlaced()
    {
        if (placed) { return; }
        StartCoroutine(DisplayHotHeaterForTime());
        placed = true;
    }

    protected override void Update()
    {
        base.Update();
        if (InHeatingRange())
        {
            timeSincePlayerHeated += Time.deltaTime;
            if (timeSincePlayerHeated > heatingRate)
            {
                playerTemperature.Add(1);
                timeSincePlayerHeated = 0;
            }
            playerTemperature.beingHeated = true;
        } else { playerTemperature.beingHeated = false; }
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
    public override bool InRangeOfPlayer() => Vector2.Distance(transform.position, player.position) <= interactableRadius && TaskActive && server.serverOnline;
    private bool InHeatingRange() => Vector2.Distance(transform.position, player.position) <= heatRadius && !TaskActive;
}
 