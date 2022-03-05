using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerUpdateTask : AbstractTask
{
    [SerializeField] Text serverVersionText;
    [SerializeField] Image progressBar;
    [SerializeField] Button updateButton;
    [SerializeField] float updateSpeed = 0.2f;
    [SerializeField] float baseServerVersion = 1.4f;
    float serverVersion;

    protected override void Awake()
    {
        base.Awake();
        progressBar.fillAmount = 0;
        serverVersion = baseServerVersion;
        serverVersionText.text = "Ver. 1." + serverVersion.ToString();
    }

    public void OnUpdateButtonPress() => StartCoroutine(UpdateServer());

    private IEnumerator UpdateServer()
    {
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += (Time.deltaTime * updateSpeed);
            yield return null;
        }
        serverVersion += 0.3f;
        serverVersionText.text = "Ver. 1." + serverVersion.ToString();
        Complete();
    }

    public override void Reset()
    {
        progressBar.fillAmount = 0;
    }
}
