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
    [SerializeField] Text statusText;
    private string outOfWaterMessage = "Out of water!";
    private string drinkWaterMessage = "Drink to heal!";
    [SerializeField] float cooldown = 10f;
    private FiltrationTask[] pumps;
    Button drinkButton;
    protected Transform player;
    private int healthToGain;
    private bool outOfWater = false;
    private bool canUse = true;
    public bool usedSinceFilter = false;

    private void Awake()
    {
        pumps = FindObjectsOfType<FiltrationTask>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        drinkButton = drinkButtonObj.GetComponent<Button>();
        drinkButton.onClick.AddListener(Drink);
        drinkButtonObj.SetActive(false);
        SetStatus();
    }

    private void Update()
    {
        drinkButtonObj.SetActive(InRangeOfPlayer());
        drinkButton.interactable = !outOfWater && canUse;
        useButtonObj.SetActive(!InRangeOfPlayer());
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
        {
            if (InRangeOfPlayer())
            {
                Drink();
            }
        }
        SetStatus();
    }

    public void Drink()
    {
        if (outOfWater || !canUse) { return; }
        usedSinceFilter = true;
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
        statusText.text = count == 0 || !canUse ? outOfWaterMessage : drinkWaterMessage;
        statusText.color = count == 0 || !canUse ? Color.red : Color.cyan;
        outOfWater = count == 0;
    }
    private bool InRangeOfPlayer() => Vector2.Distance(transform.position, player.position) <= interactableRadius;

}
