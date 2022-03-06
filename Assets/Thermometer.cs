using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer : MonoBehaviour
{
    Image thermometerImage;
    [SerializeField] Text burningText;
    [SerializeField] Text freezingText;
    [SerializeField] List<Sprite> thermometerSprites;
    Dictionary<int, Sprite> thermometerDictionary;
    TemperatureContainer temperatureContainer;


    private void Awake()
    {
        temperatureContainer = FindObjectOfType<TemperatureContainer>();
        temperatureContainer.OnChange += UpdateThermometer;
        thermometerImage = GetComponent<Image>();
        thermometerDictionary = new Dictionary<int, Sprite>();
        for (int i = 0; i < thermometerSprites.Count; i++)
        {
            thermometerDictionary.Add(i, thermometerSprites[i]);
        }
        freezingText.enabled = false;
        burningText.enabled = false;
        Debug.Log("Dictionary count: " + thermometerDictionary.Count);
    }

    public void UpdateThermometer()
    {
        thermometerImage.sprite = thermometerDictionary[temperatureContainer.GetValue()];
        UpdateTemperatureStatus();
    }

    public void UpdateTemperatureStatus()
    {
        if (temperatureContainer.freezing)
        {
            EnableFreezingText();
        }
        else if (temperatureContainer.burning)
        {
            EnableBurningText();
        }
        else
        {
            DisableTexts();
        }
    }
    public void EnableFreezingText() => freezingText.enabled = true;
    public void EnableBurningText() => burningText.enabled = true;

    public void DisableTexts()
    {
        freezingText.enabled = false;
        burningText.enabled = false;
    }

    private void OnDisable()
    {
        temperatureContainer.OnChange -= UpdateThermometer;
    }
}
