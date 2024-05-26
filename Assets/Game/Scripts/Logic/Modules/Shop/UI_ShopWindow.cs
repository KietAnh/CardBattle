using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopWindow : UIComponent
{
    public TextMeshProUGUI textNumCoin;
    public TextMeshProUGUI textNumHeart;
    public TextMeshProUGUI textNumTurn;
    public TextMeshProUGUI textNumTrophy;
    public Button btnRoll;
    public Button btnStart;
    public Button btnHome;
    public override void Init()
    {
        base.Init();

        textNumCoin = trans.Find("coin/Image/Text (TMP)").GetComponent<TextMeshProUGUI>();
        textNumHeart = trans.Find("heart/Image/Text (TMP)").GetComponent<TextMeshProUGUI>();
        textNumTurn = trans.Find("turn/Image/Text (TMP)").GetComponent<TextMeshProUGUI>();
        textNumTrophy = trans.Find("cup/Image/Text (TMP)").GetComponent<TextMeshProUGUI>();
        btnRoll = trans.Find("btn-roll").GetComponent<Button>();
        btnStart = trans.Find("btn-end-turn").GetComponent<Button>();
        btnHome = trans.Find("btn-home").GetComponent<Button>();
    }
}
