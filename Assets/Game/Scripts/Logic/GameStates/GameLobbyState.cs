using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobbyState : GameBaseState
{
    public override void OnEnter(object obj = null)
    {
        base.OnEnter(obj);
    }
    public override void OnLeave(string stateKey)
    {
        base.OnLeave(stateKey);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override string GetStateKey()
    {
        return GameState.LOBBY;
    }
}
