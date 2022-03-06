using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public event Action onSunrise;
    public event Action onSunset;
    [SerializeField] private int dayLength = 15;
    [SerializeField] private int nightLength = 15;
    public bool doDayNightCycle = true;
    [SerializeField] private float dayLight = 1f;
    [SerializeField] private float nightLight = 0.2f;
    private Light2D sun;

    private int time = 0;
    private const int CTIME = 60;
    private int fullDayLength;
    private int dayEndTime;

    void Awake(){
        sun = GetComponent<Light2D>();
    }
    void FixedUpdate()
    {

        if(doDayNightCycle == true)
        {
            dayEndTime = dayLength * CTIME;
            int nightEndTime = dayEndTime + (nightLength * CTIME);
            fullDayLength = nightEndTime;
            time = time + 1;
            if (time == dayEndTime)
            {
                //Handle Sunset
                StartCoroutine(Sunset());
            }
            else if (time == nightEndTime)
            {
                //Handle Sunrise
                StartCoroutine(Sunrise());
            }
            else if (time > nightEndTime) 
            {
                time = 0;
            }
        }
    }

    IEnumerator Sunset()
    {
        onSunset?.Invoke();
        StopCoroutine(Sunrise());
        while(sun.intensity > nightLight)
        {
            sun.intensity -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(Sunset());
    }

    IEnumerator Sunrise()
    {
        onSunrise?.Invoke();
        StopCoroutine(Sunset());
        while (sun.intensity < dayLight)
        {
            sun.intensity += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(Sunrise());
    }

    public void SetTime(int time)
    {
        if(time >= 0 && time <= fullDayLength)
        {
            this.time = time;
        }
    }

    public float GetTime()
    {
        return time;
    }

    public bool IsDay()
    {
        if(time < dayEndTime)
        {
            return true;
        }
        return false;
    }
}