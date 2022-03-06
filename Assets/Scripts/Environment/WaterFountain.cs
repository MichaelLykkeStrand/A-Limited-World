using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterFountain : MonoBehaviour
{
    [SerializeField] GameObject drinkButtonObj;
    [SerializeField] GameObject useButtonObj;
    [SerializeField] float interactableRadius = 2f;
    [SerializeField] HealthContainer health;
    [SerializeField] Text outOfWaterText;
    [SerializeField] float cooldown = 10f;
    private FiltrationTask[] pumps;
    Button drinkButton;
    protected Transform player;
    private int healthToGain;
    private bool outOfWater = false;
    private bool canUse = true;

    private void Awake()
    {
        pumps = FindObjectsOfType<FiltrationTask>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        drinkButton = drinkButtonObj.GetComponent<Button>();
        drinkButton.onClick.AddListener(Drink);
        drinkButtonObj.SetActive(false);
        outOfWaterText.enabled = false;
        SetStatus();
    }

    private void Update()
    {
        drinkButtonObj.SetActive(InRangeOfPlayer());
        drinkButton.interactable = !outOfWater && canUse;
        useButtonObj.SetActive(!InRangeOfPlayer());
        SetStatus();
    }

    public void Drink()
    {
        if (outOfWater || !canUse) { return; }
        SetStatus();
        health.Add(healthToGain);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldown);
        canUse = true;
    }

    private void SetStatus()
    {
        int count = 0;
        foreach (FiltrationTask pump in pumps)
        {
            if (!pump.TaskActive) { count++; }
        }
        healthToGain = count * 5;
        outOfWaterText.enabled = count == 0 || !canUse;
        outOfWater = count == 0;
    }
    private bool InRangeOfPlayer() => Vector2.Distance(transform.position, player.position) <= interactableRadius;

}
