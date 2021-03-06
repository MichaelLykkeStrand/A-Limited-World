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
    [SerializeField] AudioClip serverNoise;
    float serverVersion;
    bool serverUpdating = false;
    public bool serverOnline { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        progressBar.fillAmount = 0;
        serverVersion = baseServerVersion;
        serverVersionText.text = "Ver. 1." + serverVersion.ToString();
    }

    protected override void Update()
    {
        base.Update();
        updateButton.interactable = !serverUpdating;
        serverOnline = !TaskActive;
    }

    public void OnUpdateButtonPress()
    {
        StartCoroutine(UpdateServer());
        serverUpdating = true;
    }

    private IEnumerator UpdateServer()
    {
        audioSource.PlayOneShot(serverNoise);
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += (Time.deltaTime * updateSpeed);
            yield return null;
        }
        UpdateServerVersion();
        Complete();
    }

    private void UpdateServerVersion()
    {
        serverVersion += 0.3f;
        serverVersionText.text = "Ver. 1." + serverVersion.ToString();
    }

    public override void Reset()
    {
        progressBar.fillAmount = 0;
        serverUpdating = false;
    }
}
