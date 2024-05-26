using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public class GameManager : SingletonBehaviour<GameManager>
{
    public string currentState => _stateMachine.GetCurrentState();
    public string prevState = StateMachine.InvalidState;
    private StateMachine _stateMachine;

    private AppInfo _appInfo;

    private Dictionary<Type, BaseLocalManager> _localManagers = new Dictionary<Type, BaseLocalManager>();

    public void Init(AppInfo appInfo)
    {
        _appInfo = appInfo;

        InitBehaviourManagers();

        InitGameState();

        AddEventListener();

        //bool isFirstPlay = UserDataService.GetData<bool>(PREF_KEY.IsFirstPlay, true);
        //if (isFirstPlay)          // refactor, loading delay?
        //{
        //    var param = new TwoParam<string, int>();
        //    param.value1 = GameState.BOARD;
        //    param.value2 = 0;   // level tutorial
        //    _stateMachine.ChangeState(GameState.LOADING, param);
        //    //StartLevel(0);
        //    UserDataService.SetData(PREF_KEY.IsFirstPlay, false);
        //}
        //else
        //{
        //    var param = new TwoParam<string, int>();
        //    param.value1 = GameState.MAIN;
        //    ChangeState(GameState.LOADING, param);
        //    //var loadingWindow = WindowManager.Singleton.OpenWindow<LoadingWindow>(null, new WinInfo(param.value1), 0.5f) as LoadingWindow;
        //    //loadingWindow.PlayTransition(() =>
        //    //{

        //    //});
        //}

        var param = new OneParam<string>();
        param.value = GameState.HOME;
        _stateMachine.ChangeState(GameState.LOADING, param);
    }
    public void PreloadAssets()  // refactor, rename func
    {
        AssetLoadManager.Init();

#if UNITY_EDITOR
        if (AssetLoadManager.SimulateAssetBundleInEditor)
#endif
        {
            Debug.Log("kiet log >> preload all assetbundles");
            PreloadAllAssetBundles();
        }

        InitTemplateManagers();

        //PlayBGM();
        //InitCheat();
    }
    private void PreloadAllAssetBundles()
    {
        foreach (var fileEntry in _appInfo.abFiles)
        {
            string abName = fileEntry.resName;
            AssetLoadManager.PreloadAssetBundle(abName);
        }
    }
    private void InitBehaviourManagers()
    {
        List<string> mgrList = _appInfo.managers;
        foreach (string mgr in mgrList)
        {
            Type mgrType = Assembly.GetExecutingAssembly().GetType(mgr);
            GameObject mgrObj = new GameObject();
            mgrObj.name = mgr;
            mgrObj.AddComponent(mgrType);
        }
    }
    private void InitTemplateManagers()
    {
        WindowManager.Singleton.Init();
        AudioManager.Singleton.Init();
        EffectManager.Singleton.Init();
        //AdsManager.Singleton.Init();
        //CrashlyticManager.Singleton.Init();
        NotificationManager.Singleton.Init();
        ServiceManager.Singleton.Init();
        //GuideManager.Singleton.Init();
        ConfigManager.Singleton.Init();
    }

    public void AddLocalManager(BaseLocalManager mgr)
    {
        if (!_localManagers.ContainsKey(mgr.GetType()))
        {
            _localManagers.Add(mgr.GetType(), mgr);
        }
    }

    public T GetLocalManager<T>() where T : BaseLocalManager
    {
        BaseLocalManager res = null;
        if (_localManagers.TryGetValue(typeof(T), out res))
            return _localManagers[typeof(T)] as T;
        return null;

    }

    public void RemoveLocalManager(Type type)
    {
        if (_localManagers.ContainsKey(type))
        {
            _localManagers.Remove(type);
        }
    }
    
    private void InitCheat()
    {
        //bool isCheatOn = ConfigBean.GetBean<t_global_constantBean, string>("IsCheat").t_int_param == 1;
        //if (isCheatOn)
        //{
        //    var cheatObj = ObjectManager.Singleton.GetObject("cheat");
        //    cheatObj.AddComponent<CheatBehaviour>();
        //}
    }
    public void InitGameState()
    {
        _stateMachine = new StateMachine(this);
        _stateMachine.RegisterState(GameState.HOME, new GameHomeState());
        _stateMachine.RegisterState(GameState.LOBBY, new GameLobbyState());
        _stateMachine.RegisterState(GameState.SHOP, new GameShopState());
        _stateMachine.RegisterState(GameState.FIGHT, new GameFightState());
        _stateMachine.RegisterState(GameState.LOADING, new GameLoadingState());
    }
    public void ChangeState(string gameState, object obj = null)
    {
        if (_stateMachine != null)
        {
            string tempPrevState = _stateMachine.GetCurrentState();
            if (tempPrevState != GameState.LOADING)
            {
                prevState = tempPrevState;
            }
            _stateMachine.ChangeState(gameState, obj);
        }
    }

    //public void StartLevel(int level)
    //{
    //    var param = new TwoParam<string, int>();
    //    param.value1 = GameState.BOARD;
    //    param.value2 = level;
    //    //var loadingWindow = WindowManager.Singleton.OpenWindow<LoadingWindow>(null, new WinInfo(param.value1), 0.5f) as LoadingWindow;
    //    //loadingWindow.PlayTransition(() =>
    //    //{
    //    //    ChangeState(GameState.LOADING, param);
    //    //}, () =>
    //    //{
    //    //    AudioManager.Singleton.PlayEffect("ui_window_open");   // refactor constant
    //    //    BoardManager.Singleton.PlaySetupAnim();
    //    //});
    //}

    public void PauseAll(bool pause = true)
    {
        //TODO
    }

    private void AddEventListener()
    {
        GED.ResED.addListener(EventID.ResWinGame, OnWinGame);
        GED.ResED.addListener(EventID.ResLoseGame, OnLoseGame);
    }
    private void RemoveEventListener()
    {
        GED.ResED.removeListener(EventID.ResWinGame, OnWinGame);
        GED.ResED.removeListener(EventID.ResLoseGame, OnLoseGame);
    }
    private void Update()
    {
        if (_stateMachine != null)
        {
            _stateMachine.Update();
        }
        //GuideManager.Singleton.Update();
    }
    private void PlayBGM()
    {
        AudioManager.Singleton.PlayMusic("bgm", 0.5f);
    }
    private void OnWinGame(GameEvent gameEvent)
    {
        int playingLevel = (int)gameEvent.Data;
        //WindowManager.Singleton.OpenWindow<WinGameWindow>(null, new WinInfo(playingLevel));

        AudioManager.Singleton.PlayEffect("win");     // refactor constant

#if !UNITY_EDITOR
        //TrackingManager.PushEvent(EVENT_TRACK.CmpLevel, "level", playingLevel);
#endif
    }

    private void OnLoseGame(GameEvent gameEvent)
    {
        int playingLevel = (int)gameEvent.Data;
        //WindowManager.Singleton.OpenWindow<LoseGameWindow>(null, new WinInfo(playingLevel));

        AudioManager.Singleton.PlayEffect("lose");     // refactor constant
#if !UNITY_EDITOR
        //TrackingManager.PushEvent(EVENT_TRACK.FailLevel, "level", playingLevel);
#endif
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        RemoveEventListener();

        //ServerManager.Singleton.UnbindAll();

        //GuideManager.Singleton.SkipGuideOnApplicationQuit();
    }
    private void OnApplicationPause(bool pause)
    {

    }
    private void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        //TrackingManager.PushEvent(EVENT_TRACK.QuitGame);
#endif
    }

    public static int GetMainFlag()  // dummy
    {
        return 999;
    }
}
public static class GameState
{
    public const string HOME = "Home";
    public const string LOBBY = "Lobby";
    public const string SHOP = "Shop";
    public const string FIGHT = "Fight";
    public const string LOADING = "Loading";
}
