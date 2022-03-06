using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public IEnumerator FadeInElement(CanvasGroup canvasGroup, float time)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeOutElement(CanvasGroup canvasGroup, float time)
    {
        while (canvasGroup.alpha > Mathf.Epsilon)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }

    }

    public IEnumerator FadeInOut(CanvasGroup canvasGroup, float time)
    {
        yield return StartCoroutine(FadeInElement(canvasGroup, time));
        yield return StartCoroutine(FadeOutElement(canvasGroup, time));
    }
}
