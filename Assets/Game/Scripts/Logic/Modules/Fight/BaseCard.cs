using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard
{
    public CardObject cardObject;
    public CardState state;

    public int id;

    public virtual void ChangeState(CardState state)
    {
        this.state = state;
    }
}

public enum CardType
{
    Pet,
    Food,
}

public enum CardState
{
    Shop,
    Formation,
    Battle,
}
