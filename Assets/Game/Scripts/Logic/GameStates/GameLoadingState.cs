using UnityEngine;
using UnityEngine.UI;

public class GameLoadingState : GameBaseState
{
    private string _nextState;
    public override void OnEnter(object obj = null)
    {
        base.OnEnter(obj);

        var param = obj as OneParam<string>;
        if (param != null)
        {
            _nextState = param.value;
        }

        if (GameManager.Singleton.prevState == StateMachine.InvalidState) // startup
        {
            GameManager.Singleton.PreloadAssets();
            LoadingManager.Singleton.CloseLoading();
        }
        //if (_nextState == GameState.BOARD && level >= 0)
        //{
        //    LoadBg(level);
        //    BoardManager.Singleton.Init(level);
        //    if (level == 0)
        //    {
        //        //CoroutineManager.Singleton.delayedCall(0.1f, () => BoardManager.Singleton.PlaySetupAnim());
        //        BoardManager.Singleton.PlaySetupAnim();
        //    }
        //    EffectManager.Singleton.PreloadEffects("match3Effect");         // refactor, constant

        //    GameManager.Singleton.ChangeState(GameState.BOARD, level);
        //}
        if (_nextState == GameState.HOME)
        {
            GameManager.Singleton.ChangeState(_nextState);
        }
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
        return GameState.LOADING;
    }
}