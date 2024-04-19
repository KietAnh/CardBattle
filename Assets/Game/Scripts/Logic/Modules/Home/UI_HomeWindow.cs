using UnityEngine.UI;

public class UI_HomeWindow : UIComponent
{
    public Button btnJoin;
    public override void Init()
    {
        base.Init();
        
        btnJoin = trans.Find("btn_join").GetComponent<Button>();

    }
}
