using Data.Beans;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWindow : UIBase
{
    private UI_HomeWindow _view;

    public override void OnOpen()
    {
        _view = GetUIWindow<UI_HomeWindow>();

        base.OnOpen();

        InitView();
    }
    protected override void AddButtonOnClickListener()
    {
        _view.btnJoin.onClick.AddListener(OnClickJoin);
    }
    protected override void RemoveButtonOnClickListener()
    {
        _view.btnJoin.onClick.RemoveAllListeners();
    }
    protected override void InitView()
    {
        base.InitView();

        
    }

    public void OnClickJoin()
    {
        DevLog.Log("Join");
    }
}
