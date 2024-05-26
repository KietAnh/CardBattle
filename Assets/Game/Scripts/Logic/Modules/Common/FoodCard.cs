using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCard : BaseCard
{
    public void Init(int id)
    {
        this.id = id;

        var foodRecord = ConfigLoader.GetRecord<FoodRecord>(id);

        cardObject.RefreshView(foodRecord);
    }
}
