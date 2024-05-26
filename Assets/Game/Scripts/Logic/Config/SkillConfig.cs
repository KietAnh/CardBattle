using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "Config/SkillConfig", order = 1)]
public class SkillConfig : BaseConfig
{
    public List<SkillRecord> recordList;

    public Dictionary<int, SkillRecord> recordMap;
    public Dictionary<string, SkillRecord> recordMapByName;

    public override void CreateRecordMap()
    {
        recordMap = new Dictionary<int, SkillRecord>();
        recordMapByName = new Dictionary<string, SkillRecord>();
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
}

[System.Serializable]
public class SkillRecord : BaseRecord
{
    public SkillCondition condition;
    public SkillEffect effect;
    public int skillScope;   // random, phía sau, phía trước, kẻ địch, đồng minh
    public int[] param;
}


public enum SkillCondition
{
    None,
    StartOfBattle,
    Faint,
    Hurt,
    Dead,
}

public enum SkillEffect
{
    Buff,
    Damage,
    Summon,
}
