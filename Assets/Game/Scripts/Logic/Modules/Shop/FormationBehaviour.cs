using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBehaviour : MonoBehaviour
{
    [SerializeField] List<SpaceObject> _spaceList;


    public List<PetCard> GetPetList()
    {
        var petList = new List<PetCard>(5);
        for (int i = _spaceList.Count - 1; i >= 0; i--)
        {
            if (_spaceList[i].transform.childCount > 0)
            {
                petList[_spaceList.Count - 1 - i] = _spaceList[i].transform.GetChild(0).GetComponent<CardObject>().cardData as PetCard;
            }
        }
        return petList;
    }
}
