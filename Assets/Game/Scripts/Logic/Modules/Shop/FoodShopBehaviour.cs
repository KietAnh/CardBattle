using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _spaceModel;
    [SerializeField] private GameObject _cardModel;

    private List<Transform> _spaceList = new List<Transform>();
    public void Init()
    {
        var space = Instantiate(_spaceModel, transform);
        space.SetActive(true);
        space.transform.localPosition = new Vector3(-6, space.transform.position.y);
        _spaceList.Add(space.transform);

        Roll(1);
    }

    public void Roll(int turn)
    {
        for (int i = 0; i < _spaceList.Count; i++)
        {
            int foodId = GetRandomFood(turn);
            GameObject card = null;
            if (_spaceList[i].childCount == 0)
            {
                card = Instantiate(_cardModel, _spaceList[i].transform);
            }
            else
            {
                card = _spaceList[i].GetChild(0).gameObject;
            }
            var foodCard = new FoodCard();   // refactor: use object pool, problem: khi roll đủ nhiều thì object PetCard tạo ra trong bộ nhớ càng nhiều
            foodCard.cardObject = card.GetComponent<CardObject>();
            card.GetComponent<CardObject>().cardData = foodCard;
            foodCard.Init(foodId);
        }
    }

    public int GetRandomFood(int turn)
    {
        var probRecord = ConfigLoader.GetRecord<ProbRecord>(turn);
        int dice = RandomExtensions.RandomDependOnProbability(probRecord.probs) + 1;
        List<int> foodIds = (ConfigLoader.GetConfig<FoodRecord>() as FoodConfig).GetFoodsByDice(dice);
        int rand = Random.Range(0, foodIds.Count);
        return foodIds[rand];
    }
}
