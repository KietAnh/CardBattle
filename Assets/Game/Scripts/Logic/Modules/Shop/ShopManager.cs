using System;
using System.Collections.Generic;

public class ShopManager : BaseLocalManager
{
    public ShopBehaviour shopBehaviour;

    public int numCoin;
    public int numHeart;
    public int numTurn;
    public int numTrophy;

    public SpaceObject selectedSpace;

    public List<PetCard> petDataList;

    public void Init()
    {
        var shopObj = ObjectManager.Singleton.GetObject("shop", null);
        shopBehaviour = shopObj.GetComponent<ShopBehaviour>();
        shopBehaviour.Init();

        numCoin = 10;
        numHeart = 5;
        numTurn = 1;
        numTrophy = 0;
    }

    public void SelectCard(SpaceObject space)
    {
        if (selectedSpace != null)
        {
            selectedSpace.GetComponentInChildren<CardObject>().SetSelected(false);
        }
        selectedSpace = space;
        if (space != null)
        {
            selectedSpace.GetComponentInChildren<CardObject>().SetSelected(true);
        }
    }

    public bool Buy(SpaceObject spaceObject)
    {
        if (numCoin >= 3)
        {
            CardObject card = selectedSpace.transform.GetChild(0).GetComponent<CardObject>();
            DevLog.Log("Buy " + card);
            card.transform.SetParent(spaceObject.transform, false);
            card.SetSelected(false);
            card.cardData.ChangeState(CardState.Formation);
            selectedSpace = null;

            numCoin -= 3;

            GED.ED.dispatchEvent(EventID.OnBuyCard);

            return true;
        }
        else
        {
            DevLog.Log("Not enough coin");
            SelectCard(null);

            return false;
        }
    }

    public void Swap(SpaceObject spaceObject)
    {
        DevLog.Log("Swap card");
        CardObject card = selectedSpace.transform.GetChild(0).GetComponent<CardObject>();
        if (spaceObject.transform.childCount > 0)
        {
            CardObject swapCard = spaceObject.transform.GetChild(0).GetComponent<CardObject>();
            swapCard.transform.SetParent(selectedSpace.transform, false);
        }
        card.transform.SetParent(spaceObject.transform, false);
        card.SetSelected(false);
        selectedSpace = null;
    }

    public void Feed(SpaceObject spaceObject)
    {
        DevLog.Log("Feed");
        CardObject foodCard = selectedSpace.transform.GetChild(0).GetComponent<CardObject>();
        CardObject petCard = spaceObject.transform.GetComponentInChildren<CardObject>();
        if (Buy(spaceObject))
        {
            petCard.cardData.Eat(foodCard.id);

            UnityEngine.Object.Destroy(foodCard.gameObject);
        }
    }

    public void Roll()
    {
        if (numCoin > 0)
        {
            numCoin -= 1;
            GED.ED.dispatchEvent(EventID.OnRoll);
        }
        else
        {
            DevLog.Log("Not enough coin");
        }
    }

    public void Merge(CardObject targetCard, bool isBuy)
    {
        if (isBuy)
        {
            if (numCoin >= 3)
            {
                numCoin -= 3;

                GED.ED.dispatchEvent(EventID.OnBuyCard);

                CardObject card = selectedSpace.transform.GetChild(0).GetComponent<CardObject>();
                selectedSpace = null;
                UnityEngine.Object.Destroy(card.gameObject);
                targetCard.Upgrade();
            }
            else
            {
                DevLog.Log("Not enough coin");
            }
        }
        else
        {
            CardObject card = selectedSpace.transform.GetChild(0).GetComponent<CardObject>();
            selectedSpace = null;
            UnityEngine.Object.Destroy(card.gameObject);
            targetCard.Upgrade();
        }
    }

    public void StartTurn()
    {
        petDataList = shopBehaviour.GetPetList();
        GameManager.Singleton.ChangeState(GameState.FIGHT);
    }
}
