using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
有关角色症状situation的描述:
01:正常
02:流血
03:骨折
04:饥饿
05:口渴
06:辐射病
07:坏血症
08:腹泻
09:夜盲症
10:凝血障碍
11:骨质疏松
12:反胃
13:发热
14:失血性休克
15:疲劳
*/

[System.Serializable]
public class Character : MonoBehaviour
{
    [Header("角色名")]
    public string CharacterName;

    [Header("角色ID")]
    public uint ID;

    [Header("角色生命值")]
    public float MaxHealth;
    [SerializeField]
    private float Health;

    [Header("角色辐射值")]
    public float MaxRaidatinValue;
    [SerializeField]
    private float RadiationValue;

    public HashSet<Sickness> Situations; //哈希集合储存防止重复

    [SerializeField]
    [Header("哈希集合的序列化 更改无效")]
    private List<Sickness> SituList;
    public enum Sickness
    {
        Normal,
        Bleed,
        Fracture,
        Hunger,
        Thirst,
        RadiationSickness,
        Scurvy, //坏血症
        Diarrhoea, //腹泻
        NightBlindness,
        CoagulationDisorder, //凝血障碍
        Osteoporosis, //骨质疏松
        Nausea, //反胃
        Fever,
        HemorrhagicShock, //失血性休克
        Fatigue //疲劳
    }

    public void SituationCheck()
    {
        if(Situations.Count != 0)
        {
            if (Situations.Count > 1)
            {
                foreach (var s in Situations)
                {
                    if (s == Sickness.Normal)
                    {
                        Situations.Remove(s);
                        break;
                    }
                }
            }
        }
        else
        {
            SituationAdd(Sickness.Normal);
        }
    }
    public void SituationAdd(Sickness situ)
    {
        Situations.Add(situ);
    }

    public void SituationRemove(Sickness situ)
    {
        Situations.Remove(situ);
    }

    public void SerializeHashSet()
    {
        SituList.Clear();
        foreach(var s in Situations)
        {
            SituList.Add(s);
        }
    }
}
