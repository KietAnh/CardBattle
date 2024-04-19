using Data.Beans;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : SingletonTemplate<GuideManager>
{
    //private int _nextGuideId;
    //private bool _isPlayingGuide;
    //private List<GameObject> _objHighlights;

    //private int _hightlightSortOrder = 3500;
    //public void Init()
    //{
    //    _nextGuideId = UserDataService.GetData<int>(PREF_KEY.NextGuide, 1001);  // refactor, constant
    //    _isPlayingGuide = false;
    //}

    //public void Update()
    //{
    //    if (_nextGuideId > 0 && !_isPlayingGuide)
    //    {
    //        CheckPlayGuide(_nextGuideId);
    //    }
    //}
    //public void CheckPlayGuide(int guideId)
    //{
    //    bool isTrigger = CheckTrigger(guideId);
    //    if (isTrigger)
    //    {
    //        PlayGuide(guideId);
    //    }
    //}
    //private bool CheckTrigger(int guideId)
    //{
    //    var guideCfg = ConfigBean.GetBean<t_guideBean, int>(guideId);
    //    string[] arr = guideCfg.t_trigger.Split('+');
    //    TriggerType triggerType = (TriggerType)int.Parse(arr[0]);
    //    switch (triggerType)
    //    {
    //        case TriggerType.None:
    //            return true;
    //        case TriggerType.ChangeState:
    //            string state = arr[1];
    //            if (GameManager.Singleton.currentState == state)
    //            {
    //                return true;
    //            }
    //            break;
    //        case TriggerType.Playing:
    //            if (BoardManager.isPlaying)
    //            {
    //                return true;
    //            }
    //            break;
    //    }

    //    return false;
    //}
    //private void PlayGuide(int guideId)
    //{
    //    _isPlayingGuide = true;
    //    var guideCfg = ConfigBean.GetBean<t_guideBean, int>(guideId);
    //    float timeDelay = guideCfg.t_delay / (1000 * 1.0f);
    //    CoroutineManager.Singleton.delayedCall(timeDelay, () =>
    //    {
    //        var guideWindow = WindowManager.Singleton.GetWindow<GuideWindow>();
    //        if (guideWindow != null)
    //        {
    //            (guideWindow as GuideWindow).SetGuideId(guideId);
    //        }
    //        WindowManager.Singleton.OpenWindow<GuideWindow>(null, new WinInfo(guideId));
    //        HighlightObjects(guideCfg.t_objects);
    //        var mainObj = GameObject.Find(guideCfg.t_main_object);
    //        var guideTarget = mainObj.AddComponent<GuideTarget>();
    //        guideTarget.OnCompleteGuide = CompleteGuide;
    //    });
    //}
    //private void HighlightObjects(string objectStr)
    //{
    //    if (_objHighlights == null)
    //        _objHighlights = new List<GameObject>();
    //    string[] pathList = objectStr.Split(';');
    //    for (int i = 0; i < pathList.Length; i++)
    //    {
    //        var obj = GameObject.Find(pathList[i]);
    //        if (obj != null && !_objHighlights.Contains(obj))
    //        {
    //            var canvas = obj.AddComponent<Canvas>();
    //            canvas.overrideSorting = true;
    //            canvas.sortingOrder = _hightlightSortOrder++;   
    //            obj.AddComponent<GraphicRaycaster>();
    //            _objHighlights.Add(obj);
    //        }
    //    }
    //}
    //private void CompleteGuide(GuideTarget target)
    //{
    //    int guideId = _nextGuideId;
    //    var guideCfg = ConfigBean.GetBean<t_guideBean, int>(guideId);
    //    _nextGuideId = guideCfg.t_next_guide;
    //    _isPlayingGuide = false;
    //    UserDataService.SetData(PREF_KEY.NextGuide, _nextGuideId);

    //    if (_nextGuideId > 0 && _IsSameGuideGroup(guideId, _nextGuideId))
    //    {
    //        var guideWindow = WindowManager.Singleton.GetWindow<GuideWindow>() as GuideWindow;
    //        guideWindow.SetActiveHand(false);
            
    //    }
    //    else
    //    {
    //        WindowManager.Singleton.CloseWindow<GuideWindow>();

    //        for (int i = 0; i < _objHighlights.Count; i++)
    //        {
    //            var obj = _objHighlights[i];
    //            if (obj != null)
    //            {
    //                GameObject.Destroy(obj.GetComponent<GraphicRaycaster>());
    //                GameObject.Destroy(obj.GetComponent<Canvas>());
    //            }
    //        }
    //        _objHighlights.Clear();
    //        _objHighlights = null;

    //        _hightlightSortOrder = 3500;
    //    }

    //    GameObject.Destroy(target);
    //}

    //public void SkipGuideOnApplicationQuit()
    //{
    //    if (_isPlayingGuide)
    //    {
    //        var guideCfg = ConfigBean.GetBean<t_guideBean, int>(_nextGuideId);
    //        _nextGuideId = guideCfg.t_next_guide_skip;
    //        UserDataService.SetData(PREF_KEY.NextGuide, _nextGuideId);
    //    }
    //}

    //private bool _IsSameGuideGroup(int id1, int id2)
    //{
    //    var guide1 = ConfigBean.GetBean<t_guideBean, int>(id1);
    //    var guide2 = ConfigBean.GetBean<t_guideBean, int>(id2);
    //    return guide1.t_next_guide_skip == guide2.t_next_guide_skip;
    //}
    
}

public enum TriggerType
{
    None = 0,  // hoàn thành guide trước đó

    ChangeState = 1, 
    Playing = 2,    // khi bắt đầu play ingame
}
