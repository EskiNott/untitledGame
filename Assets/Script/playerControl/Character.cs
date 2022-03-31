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
*/

public class Character
{
    public string name;
    public uint id;
    public LinkedList<bool> situation;
}
