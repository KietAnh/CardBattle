using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.Events;

public class WindowManager : SingletonTemplate<WindowManager>
{
    public Camera mainCamera;

    private Transform _panelHolder;
    private float _canvasScaleMatchValue;

    private Dictionary<string, UIBase> windows = new Dictionary<string, UIBase>();
    private Dictionary<string, GameObject> windowObjCache = new Dictionary<string, GameObject>();

    public void Awake()
    {
        _panelHolder = GameObject.Find("UISystem").transform;
        Debug.Log("width: " + Screen.width + "; height: " + Screen.height);
        _canvasScaleMatchValue = UIUtils.FindValidCanvasScalerMatchValue1(Screen.width, Screen.height, 1080, 1920);
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }
    public void Init()
    {
        PreloadWindows();

        AddEventListener();
    }

    private void AddEventListener()
    {
        GED.ResED.addListener(EventID.ResBuyPack, ShowPopupReward);
    }
    private void RemoveEventListener()
    {
        GED.ResED.removeListener(EventID.ResBuyPack, ShowPopupReward);
    }

    public void PreloadWindows()   // refactor, define trước list preload windows trong config
    {
        LoadWindow("ShopWindow");
        LoadWindow("MainWindow");
        LoadWindow("SettingWindow");
        LoadWindow("SettingInMainWindow");
        LoadWindow("HubWindow");
        LoadWindow("LoseGameWindow");
        LoadWindow("WinGameWindow");
        LoadWindow("LoadingWindow");
        LoadWindow("RewardWindow");
    }

    public UIBase GetWindow<T>() where T : UIBase
    {
        string windowName = typeof(T).Name;
        if (windows.ContainsKey(windowName))
        {
            return windows[windowName];
        }
        return null;
    }

    public UIBase OpenWindow<T>(UILayer layer = null, WinInfo info = null, float matchScale = -1) where T : UIBase, new()
    {
        string windowName = typeof(T).Name;
        if (windows.ContainsKey(windowName))
        {
            windows[windowName].trans.gameObject.SetActive(true);
            windows[windowName].OnShow();
            return windows[windowName];
        }
        else
        {
            return CreateWindow(windowName, layer, info, matchScale);
        }

    }
    public UIBase CreateWindow(string windowName, UILayer layer = null, WinInfo info = null, float matchScale = -1)
    {
        GameObject panelPrefab = LoadWindow(windowName);  // refactor

        var panel = GameObject.Instantiate(panelPrefab, _panelHolder);  // refactor
        panel.name = windowName;

        Type panelType = Assembly.GetExecutingAssembly().GetType(windowName);
        UIBase ui = Activator.CreateInstance(panelType) as UIBase;
        Type componentType = Assembly.GetExecutingAssembly().GetType(UIComponent.UI_COMPONENT_PREFIX + windowName);
        UIComponent component = Activator.CreateInstance(componentType) as UIComponent;
        component.trans = panel.transform;
        component.Init();

        Canvas canvas = panel.GetComponent<Canvas>();
        canvas.worldCamera = mainCamera;
        if (matchScale < 0 || matchScale > 1)
        {
            panel.GetComponent<CanvasScaler>().matchWidthOrHeight = _canvasScaleMatchValue;
        }
        else
        {
            panel.GetComponent<CanvasScaler>().matchWidthOrHeight = matchScale;
        }

        ui.Create(panel.transform, windowName, component, info, layer);
        windows.Add(windowName, ui);

        ui.OnOpen();
        ui.OnShow();

        return ui;
    }


    public GameObject LoadWindow(string winName, bool isCache = true)
    {
        if (windowObjCache.ContainsKey(winName))
            return windowObjCache[winName];
        string abName = string.Format("wd_{0}", winName.ToLower());
        GameObject uiWindow = AssetLoadManager.LoadAsset<GameObject>(abName, winName);
        if (uiWindow == null)
        {
            uiWindow = Resources.Load<GameObject>(winName);
        }
        if (isCache)
        {
            windowObjCache.Add(winName, uiWindow);
        }
        return uiWindow;
    }

    public void CloseWindow<T>(Action callback = null)
    {
        string windowName = typeof(T).Name;
        if (windows.ContainsKey(windowName))
        {
            windows[windowName].OnHide(() =>
            {
                windows[windowName].OnClose();
                windows.Remove(windowName);
                if (callback != null)
                {
                    callback();
                }
            });
        }
    }
    public void HideWindow<T>()
    {
        string windowName = typeof(T).Name;
        if (windows.ContainsKey(windowName))
        {
            windows[windowName].trans.gameObject.SetActive(false);
            windows[windowName].OnHide(null);
        }
    }

    #region Event Handler

    private void ShowPopupReward(GameEvent gameEvent)
    {
        //var rewardMap = gameEvent.Data as Dictionary<int, int>;
        //if (rewardMap != null)
        //{
        //    OpenWindow<RewardWindow>(null, new WinInfo(rewardMap));
        //}
    }

    #endregion
}

public class WinInfo
{
    public object param;
    public List<UnityAction> callbacks;

    public WinInfo(object param, List<UnityAction> callbacks = null)
    {
        this.param = param;
        this.callbacks = callbacks;
    }
}
public class UILayer
{

}