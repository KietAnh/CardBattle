using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindow : UIBase
{
    private UI_ShopWindow _view;

    private ShopManager _shopManager;

    public override void OnOpen()
    {
        _view = GetUIWindow<UI_ShopWindow>();

        base.OnOpen();

        _shopManager = GameManager.Singleton.GetLocalManager<ShopManager>();

        InitView();
    }

    protected override void InitView()
    {
        base.InitView();

        if (_shopManager != null)
        {
            _view.textNumCoin.text = _shopManager.numCoin.ToString();
            _view.textNumHeart.text = _shopManager.numHeart.ToString();
            _view.textNumTurn.text = _shopManager.numTurn.ToString();
            _view.textNumTrophy.text = _shopManager.numTrophy.ToString() + "/10";
        }
    }

    protected override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnBuyCard, RefreshView);
        GED.ED.addListener(EventID.OnRoll, RefreshView);
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnBuyCard, RefreshView);
        GED.ED.removeListener(EventID.OnRoll, RefreshView);
    }

    protected override void AddButtonOnClickListener()
    {
        _view.btnRoll.onClick.AddListener(OnClickRoll);
        _view.btnStart.onClick.AddListener(OnClickStart);
        _view.btnHome.onClick.AddListener(OnClickHome);
    }
    protected override void RemoveButtonOnClickListener()
    {
        _view.btnRoll.onClick.RemoveAllListeners();
        _view.btnStart.onClick.RemoveAllListeners();
        _view.btnHome.onClick.RemoveAllListeners();
    }

    public void RefreshView(GameEvent gameEvent)
    {
        _view.textNumCoin.text = _shopManager.numCoin.ToString();
    }

    public void OnClickRoll()
    {
        DevLog.Log("On Click Roll");
        _shopManager.Roll();
    }

    public void OnClickStart()
    {
        DevLog.Log("On Click Start");
        _shopManager.StartTurn();
    }

    public void OnClickHome()
    {
        DevLog.Log("On Click Home");
    }
}
