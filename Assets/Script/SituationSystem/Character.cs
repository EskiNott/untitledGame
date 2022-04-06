using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
�йؽ�ɫ֢״situation������:
01:����
02:��Ѫ
03:����
04:����
05:�ڿ�
06:���䲡
07:��Ѫ֢
08:��к
09:ҹä֢
10:��Ѫ�ϰ�
11:��������
12:��θ
13:����
14:ʧѪ���ݿ�
15:ƣ��
*/

[System.Serializable]
public class Character : MonoBehaviour
{
    [Header("��ɫ��")]
    public string CharacterName;

    [Header("��ɫID")]
    public uint ID;

    [Header("��ɫ����ֵ")]
    public float MaxHealth;
    [SerializeField]
    private float Health;

    [Header("��ɫ����ֵ")]
    public float MaxRaidatinValue;
    [SerializeField]
    private float RadiationValue;

    public HashSet<Sickness> Situations; //��ϣ���ϴ����ֹ�ظ�

    [SerializeField]
    [Header("��ϣ���ϵ����л� ������Ч")]
    private List<Sickness> SituList;
    public enum Sickness
    {
        Normal,
        Bleed,
        Fracture,
        Hunger,
        Thirst,
        RadiationSickness,
        Scurvy, //��Ѫ֢
        Diarrhoea, //��к
        NightBlindness,
        CoagulationDisorder, //��Ѫ�ϰ�
        Osteoporosis, //��������
        Nausea, //��θ
        Fever,
        HemorrhagicShock, //ʧѪ���ݿ�
        Fatigue //ƣ��
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
