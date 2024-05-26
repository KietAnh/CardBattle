using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "PetConfig", menuName = "Config/PetConfig", order = 1)]
public class PetConfig : BaseConfig
{
    public List<PetRecord> recordList;

    public Dictionary<int, PetRecord> recordMap;
    public Dictionary<string, PetRecord> recordMapByName;

    public override void CreateRecordMap()
    {
        recordMap = new Dictionary<int, PetRecord>();
        recordMapByName = new Dictionary<string, PetRecord>();
        foreach (var record in recordList)
        {
            if (!recordMap.ContainsKey(record.id))
            {
                recordMap.Add(record.id, record);
            }
            if (!recordMapByName.ContainsKey(record.name))
            {
                recordMapByName.Add(record.name, record);
            }
        }
    }
    public override BaseRecord GetRecordById(int id)
    {
        if (recordMap.ContainsKey(id))
            return recordMap[id];
        else
        {
            DevLog.Log("id is invalid: " + id);
            return null;
        }
    }

    public override BaseRecord GetRecordByName(string name)
    {
        if (recordMapByName.ContainsKey(name))
            return recordMapByName[name];
        else
        {
            DevLog.Log("name is invalid: " + name);
            return null;
        }
    }

    public List<int> GetPetsByDice(int dice)
    {
        var res = new List<int>();
        foreach (var record in recordList)
        {
            if (record.dice == dice)
                res.Add(record.id);
        }
        return res;
    }
}

[Serializable]
public class PetRecord : BaseRecord
{
    public int dice;
    public int attack;
    public int health;
    public int[] skillIds;
    public Sprite sprite;
}
