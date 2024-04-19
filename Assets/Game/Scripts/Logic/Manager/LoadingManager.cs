using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : SingletonBehaviour<LoadingManager>
{
    private Slider _progressBar;
    private GameObject _loadingPanel;
    private RectTransform _progressHead;
    protected override void Awake()
    {
        base.Awake();
        var loadingPanelPrefab = Resources.Load<GameObject>("LoadingPanel");
        _loadingPanel = Instantiate(loadingPanelPrefab);
        _loadingPanel.GetComponent<Canvas>().worldCamera = Camera.main;
        _progressBar = _loadingPanel.GetComponentInChildren<Slider>();
        _progressHead = _progressBar.transform.Find("head").GetComponent<RectTransform>();

        _progressBar.value = 0;
        _progressHead.anchoredPosition = Vector3.zero;
    }
    public void OnProgress(float value)
    {
        if (value >= 0 && value <= 1f)
        {
            _progressBar.value = value;
            _progressHead.anchoredPosition = new Vector2(_progressBar.fillRect.rect.width + 9f, 0);   // refactor, hardcode
        }
        else
        {
            Debug.LogError("KIET LOG >> Progress value is invalid >> " + value);
        }
    }
    public void CloseLoading()
    {
        CoroutineManager.Singleton.delayedCall(0.5f, () =>
        {
            GameObject.Destroy(_loadingPanel);
        });
    }

    public IEnumerator FakeLoad(float duration)
    {
        float value = 0f;
        while (value < 1f)
        {
            float valuePerFrame = 1f / (duration / Time.deltaTime);
            value += valuePerFrame;
            value = Mathf.Clamp(value, 0f, 1f);
            OnProgress(value);
            yield return null;
        }
    }
}
