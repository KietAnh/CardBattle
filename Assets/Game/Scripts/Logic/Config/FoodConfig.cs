using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodConfig", menuName = "Config/FoodConfig", order = 1)]
public class FoodConfig : BaseConfig
{
    public List<FoodRecord> recordList;

    public Dictionary<int, FoodRecord> recordMap;
    public Dictionary<string, FoodRecord> recordMapByName;

    public override void CreateRecordMap()
    {
        recordMap = new Dictionary<int, FoodRecord>();
        recordMapByName = new Dictionary<string, FoodRecord>();
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

    public List<int> GetFoodsByDice(int dice)
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
public class FoodRecord : BaseRecord
{
    public int dice;
    public int skillId;
    public Sprite sprite;
}

