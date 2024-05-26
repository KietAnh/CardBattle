using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : BaseLocalManager
{
    public FightBehaviour fightBehaviour;

    public List<PetCard> playerPetList;
    public List<PetCard> enemyPetList;

    public void Init()
    {
        var shopManager = GameManager.Singleton.GetLocalManager<ShopManager>();

        playerPetList = shopManager.petDataList;
        //enemyPetList = ConfigLoader.GetRecord<LevelRecord>(shopManager.numTurn);

        var fightObject = ObjectManager.Singleton.GetObject("fight", null);
        fightBehaviour = fightObject.GetComponent<FightBehaviour>();
        fightBehaviour.Init(playerPetList);
    }
}
