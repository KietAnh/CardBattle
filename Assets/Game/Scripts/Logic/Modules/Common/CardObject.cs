using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardObject : MonoBehaviour  
{
    [SerializeField] private SpriteRenderer _cardSprite;
    [SerializeField] private SpriteRenderer _diceSprite;
    [SerializeField] private GameObject _selector;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private SpriteRenderer _levelSprite;
    [SerializeField] private GameObject[] _expObjs;

    public BaseCard cardData { get; set; }

    public void SetSelected(bool selected)
    {
        _selector.SetActive(selected);
    }

    public void RefreshView(PetRecord petRecord)
    {
        _cardSprite.sprite = petRecord.sprite;
        _diceSprite.sprite = GlobalConstant.GetSprite("img-dice-" + petRecord.dice);
        _attackText.text = petRecord.attack.ToString();
        _healthText.text = petRecord.health.ToString();
    }

    public void RefreshView(FoodRecord foodRecord)
    {
        _cardSprite.sprite = foodRecord.sprite;
        _diceSprite.sprite = GlobalConstant.GetSprite("img-dice-" + foodRecord.dice);
        _attackText.transform.parent.parent.gameObject.SetActive(false);
        _healthText.transform.parent.parent.gameObject.SetActive(false);
    }

    public void RefreshStateView(CardState state)
    {
        switch (state)
        {
            case CardState.Shop:
                _levelSprite.gameObject.SetActive(false);
                _diceSprite.gameObject.SetActive(true);
                break;
            case CardState.Formation:
                _levelSprite.gameObject.SetActive(true);
                _diceSprite.gameObject.SetActive(false);
                break;
            case CardState.Battle:
                break;
        }
    }

    public void RefreshDamageHealthView(PetCard cardData)
    {
        _levelSprite.sprite = GlobalConstant.GetSprite("img-level-" + cardData.level);

        for (int i = 0; i < cardData.exp; i++)
        {
            _expObjs[i].SetActive(true);
        }
        for (int i = cardData.exp; i < 3; i++)
        {
            _expObjs[i].SetActive(false);
        }

        _attackText.text = cardData.attack.ToString();
        _healthText.text = cardData.health.ToString();
    }

    
}