using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public bool[] interact = { false, false, false, false, false, false};
    public bool thisInvestigate = false;
    public float checkDistance = 4.0f;
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
