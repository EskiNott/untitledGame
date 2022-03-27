using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public bool[] interact = { false, false, false, false, false, false};
    public bool thisInvestigate = false;
    public float checkDistance = 4.0f;
    public float minCheckDistance = 1.0f;
    public float maxCheckDistance = 10.0f;
    public bool isSizeBig;
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
