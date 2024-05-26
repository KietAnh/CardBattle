using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceObject : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var shopManager = GameManager.Singleton.GetLocalManager<ShopManager>();
        if (shopManager == null)
            return;
        Sprite selectedSprite = GlobalConstant.GetSprite("img-card-selected");
        if (shopManager.selectedSpace == null)
        {
            if (transform.childCount > 0)  // occupy
            {
                shopManager.SelectCard(this);
            }
        }
        else
        {
            CardObject selectedCard = shopManager.selectedSpace.GetComponentInChildren<CardObject>();
            
            if (shopManager.selectedSpace == this) // click lên card đã select
            {
                shopManager.SelectCard(null);
            }
            else if (transform.childCount > 0) // click lên card khác
            {
                CardObject thisCard = transform.GetChild(0).GetComponent<CardObject>();

                if (transform.parent.GetComponent<FormationBehaviour>() != null &&
                    shopManager.selectedSpace.transform.parent.GetComponent<FormationBehaviour>() != null)  // cả 2 card đều treen formation
                {
                    if (selectedCard.id == thisCard.id)
                    {
                        shopManager.Merge(thisCard, false);
                    }
                    else
                    {
                        shopManager.Swap(this);
                    }
                }
                else
                {
                    if (selectedCard.type == CardType.Pet)
                    {
                        if (selectedCard.id == thisCard.id)
                            shopManager.Merge(thisCard, true);
                        else
                            shopManager.SelectCard(this);
                    }
                    else
                    {
                        if (transform.parent.GetComponent<FormationBehaviour>() != null)
                            shopManager.Feed(this);
                        else
                            shopManager.SelectCard(this);
                    }
                }   
            }
            else  // click lên space ko có card
            {
                if (selectedCard.type == CardType.Food)
                {
                    shopManager.SelectCard(null);
                }
                else
                {
                    if (transform.parent.GetComponent<FormationBehaviour>() != null)  // treen formation
                    {
                        if (shopManager.selectedSpace.transform.parent.GetComponent<FormationBehaviour>() != null)
                        {
                            shopManager.Swap(this);

                        }
                        else
                        {
                            shopManager.Buy(this);
                        }

                    }
                    else   // tren shop
                    {
                        shopManager.SelectCard(null);
                    }
                }
            }

            // click lên background
        }
    }
}
