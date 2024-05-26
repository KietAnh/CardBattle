

public class GameFightState : GameBaseState
{
    public override void OnEnter(object obj = null)
    {
        base.OnEnter(obj);

        var fightManager = new FightManager();
        fightManager.Init();
        GameManager.Singleton.AddLocalManager(fightManager);

        WindowManager.Singleton.OpenWindow<FightWindow>();
    }
    public override void OnLeave(string stateKey)
    {
        base.OnLeave(stateKey);
        GameManager.Singleton.GetLocalManager<FightManager>().fightBehaviour.Hide();

        WindowManager.Singleton.HideWindow<FightWindow>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override string GetStateKey()
    {
        return GameState.FIGHT;
    }
}
