using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer : MonoBehaviour
{
    Image thermometerImage;
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
    }

    public void UpdateThermometer(int temperature)
    {
        thermometerImage.sprite = thermometerDictionary[temperature];
    }

}
