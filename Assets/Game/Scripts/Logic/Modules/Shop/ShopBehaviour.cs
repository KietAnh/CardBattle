using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private FormationBehaviour _formation;
    [SerializeField] private PetShopBehaviour _petShop;
    [SerializeField] private FoodShopBehaviour _foodShop;

    private void Awake()
    {
        GED.ED.addListener(EventID.OnRoll, Roll);
    }

    private void OnDestroy()
    {
        GED.ED.removeListener(EventID.OnRoll, Roll);
    }

    public void Init()
    {
        _petShop.Init();
        _foodShop.Init();
    }

    public void Roll(GameEvent gameEvent)
    {
        var shopManager = GameManager.Singleton.GetLocalManager<ShopManager>();
        if (shopManager != null)
        {
            _petShop.Roll(shopManager.numTurn);
            _foodShop.Roll(shopManager.numTurn);
        }
    }

    public List<PetCard> GetPetList()
    {
        return _formation.GetPetList();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
