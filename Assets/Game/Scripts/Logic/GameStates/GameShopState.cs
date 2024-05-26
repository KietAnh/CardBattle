using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShopState : GameBaseState
{
    public override void OnEnter(object obj = null)
    {
        base.OnEnter(obj);

        var shopManager = new ShopManager();
        shopManager.Init();
        GameManager.Singleton.AddLocalManager(shopManager);
        WindowManager.Singleton.OpenWindow<ShopWindow>();
    }
    public override void OnLeave(string stateKey)
    {
        base.OnLeave(stateKey);

        //GameManager.Singleton.RemoveLocalManager(typeof(ShopManager));
        GameManager.Singleton.GetLocalManager<ShopManager>().shopBehaviour.Hide();
        WindowManager.Singleton.HideWindow<ShopWindow>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override string GetStateKey()
    {
        return GameState.SHOP;
    }
}
