using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[Serializable]
public class item : MonoBehaviour
{
    [Header("��Ʒ��")]
    [TextArea]
    public string ItemName;

    [Header("��ԴID")]
    public int ResID = -1;

    [Header("Sprite")]
    public Sprite sprite;

    [Header("���� ��� ���� ʳ�� ��̸ ȡ�� װ��")]
    public bool[] interact = { false, false, false, false, false, false, false };

    [HideInInspector]
    public bool thisInvestigate = false;

    [Header("Ĭ�ϼ��ʱ�ľ���")]
    public float checkDistance = 4.0f;

    [Header("����������")]
    public float minCheckDistance = 1.0f;
    public float maxCheckDistance = 10.0f;

    [Header("����ٶ�")]
    public float checkSpeed = 2.0f;

    [Header("�������ת������")]
    public float maxRotation_Y = 15.0f;
    public float minRotation_Y = -15.0f;
    public float maxRotation_Z = 15.0f;
    public float minRotation_Z = -15.0f;

    [Header("�Ƿ�Ϊ�����Ʒ")]
    public bool isSizeBig;

    [Header("�Ӷ���")]
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
