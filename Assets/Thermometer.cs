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


    private void Awake()
    {
        thermometerImage = GetComponent<Image>();
        thermometerDictionary = new Dictionary<int, Sprite>();
        for (int i = 0; i < thermometerSprites.Count; i++)
        {
            thermometerDictionary.Add(i, thermometerSprites[i]);
        }
        freezingText.enabled = false;
        burningText.enabled = false;
    }

    public void UpdateThermometer(int temperature)
    {
        thermometerImage.sprite = thermometerDictionary[temperature];
    }

    public void EnableFreezingText() => freezingText.enabled = true;
    public void EnableBurningText() => burningText.enabled = true;

    public void DisableTexts()
    {
        freezingText.enabled = false;
        burningText.enabled = false;
    }
}
