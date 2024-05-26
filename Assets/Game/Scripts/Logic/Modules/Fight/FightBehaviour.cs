using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSlots;
    [SerializeField] private List<Transform> _enemySlots;
    public void Init(List<PetCard> playerPetList)
    {
        for (int i = 0; i < _playerSlots.Count; i++)
        {
            var cardObject = ObjectManager.Singleton.GetObject("card");
            //cardObject.GetComponent<CardObject>().Init(playerPetList[0]);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
