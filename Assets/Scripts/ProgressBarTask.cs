using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarTask : AbstractTask
{
    [SerializeField] Image progressBar;
    [SerializeField] Button updateButton;
    [SerializeField] float updateSpeed = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        progressBar.fillAmount = 0;
    }

    public void OnUpdateButtonPress() => StartCoroutine(UpdateServer());

    private IEnumerator UpdateServer()
    {
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += (Time.deltaTime * updateSpeed);
            yield return null;
        }
        Complete();
    }

    private void Reset()
    {
        progressBar.fillAmount = 0;
    }
}
