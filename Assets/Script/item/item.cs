using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[Serializable]
public class item : MonoBehaviour
{
    [Header("物品名")]
    [TextArea]
    public string ItemName;

    [Header("资源ID")]
    public int ResID = -1;

    [Header("Sprite")]
    public Sprite sprite;

    [Header("交互 检查 攻击 食用 交谈 取走 装备")]
    public bool[] interact = { false, false, false, false, false, false, false };

    [HideInInspector]
    public bool thisInvestigate = false;

    [Header("默认检查时的距离")]
    public float checkDistance = 4.0f;

    [Header("检查距离限制")]
    public float minCheckDistance = 1.0f;
    public float maxCheckDistance = 10.0f;

    [Header("检查速度")]
    public float checkSpeed = 2.0f;

    [Header("靠近检查转动限制")]
    public float maxRotation_Y = 15.0f;
    public float minRotation_Y = -15.0f;
    public float maxRotation_Z = 15.0f;
    public float minRotation_Z = -15.0f;

    [Header("是否为大件物品")]
    public bool isSizeBig;

    [Header("子对象")]
    public GameObject[] childParts;
    public enum interactType
    {
        Interact,
        Check,
        Attack,
        Eat,
        Talk,
        Take,
        Equip
    }
}
