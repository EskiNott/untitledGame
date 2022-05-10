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

    [Header("���� ��� ���� ʳ�� ��̸ ȡ��")]
    public bool[] interact = { false, false, false, false, false, false};

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
        Check,
        Take,
        Eat,
        Attack,
        Talk,
        Interact
    }
}
