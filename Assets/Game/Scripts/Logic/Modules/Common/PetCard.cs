

public class PetCard : BaseCard
{
    public int level;
    public int exp;
    public int attack;
    public int health;

    public void Init(int id, CardState state)
    {
        this.id = id;
        this.level = 1;
        this.exp = 0;
        this.state = state;
        var petRecord = ConfigLoader.GetRecord<PetRecord>(id);
        attack = petRecord.attack;
        health = petRecord.health;

        cardObject.RefreshView(petRecord);
        cardObject.RefreshStateView(state);
    }

    //public void Init(PetData data, PetState state)
    //{
    //    cardData = data;
    //    ChangeState(state);
    //    RefreshView();
    //}
    public override void ChangeState(CardState state)
    {
        base.ChangeState(state);
        cardObject.RefreshStateView(state);
    }

    public void Eat(int foodId)
    {
        DevLog.Log("Eat " + foodId);
    }

    public void Upgrade()
    {
        DevLog.Log("upgrade");

        if (level == 3)
            return;

        // upgrade exp
        if ((level == 1 && exp == 2) || (level == 2 && exp == 3))
        {
            level += 1;
            exp = 0;
        }
        else
        {
            exp += 1;
        }

        // upgrade damage health
        attack += 1;
        health += 1;

        cardObject.RefreshDamageHealthView(this);
    }
}

