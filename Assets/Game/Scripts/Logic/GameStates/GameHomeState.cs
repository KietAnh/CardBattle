using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHomeState : GameBaseState
{
    public override void OnEnter(object obj = null)
    {
        base.OnEnter(obj);

        WindowManager.Singleton.OpenWindow<HomeWindow>();
    }
    public override void OnLeave(string stateKey)
    {
        base.OnLeave(stateKey);

        WindowManager.Singleton.CloseWindow<HomeWindow>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override string GetStateKey()
    {
        return GameState.HOME;
    }
}
