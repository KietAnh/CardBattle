
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PetShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _spaceModel;
    [SerializeField] private GameObject _cardModel;

    private List<Transform> _spaceList = new List<Transform>();
    public void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            var space = Instantiate(_spaceModel, transform);
            space.SetActive(true);
            space.transform.localPosition = new Vector3(i, space.transform.position.y);
            _spaceList.Add(space.transform); 
        }

        Roll(1);
    }

    public void Roll(int turn)
    {
        for (int i = 0; i < _spaceList.Count; i++)
        {
            int petId = GetRandomPet(turn);
            GameObject card = null;
            if (_spaceList[i].childCount == 0)
            {
                card = Instantiate(_cardModel, _spaceList[i].transform);
            }
            else
            {
                card = _spaceList[i].GetChild(0).gameObject;
            }
            var petCard = new PetCard();   // refactor: use object pool, problem: khi roll đủ nhiều thì object PetCard tạo ra trong bộ nhớ càng nhiều
            petCard.cardObject = card.GetComponent<CardObject>();
            card.GetComponent<CardObject>().cardData = petCard;
            petCard.Init(petId, CardState.Shop);
        }
    }

    public int GetRandomPet(int turn)
    {
        var probRecord = ConfigLoader.GetRecord<ProbRecord>(turn);
        int dice = RandomExtensions.RandomDependOnProbability(probRecord.probs) + 1;
        List<int> petIds = (ConfigLoader.GetConfig<PetRecord>() as PetConfig).GetPetsByDice(dice);
        int rand = Random.Range(0, petIds.Count);
        return petIds[rand];
    }

}
