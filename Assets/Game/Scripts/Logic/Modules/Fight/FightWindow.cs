using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightWindow : UIBase
{
    private UI_FightWindow _view;

    public override void OnOpen()
    {
        _view = GetUIWindow<UI_FightWindow>();

        base.OnOpen();

        InitView();
    }

    protected override void InitView()
    {
        base.InitView();

        
    }
}
