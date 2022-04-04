using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public backpack BP;
    private Transform myTrans;
    // Start is called before the first frame update
    void Start()
    {
        myTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        spawnItemsinBackpack();
    }

    public void spawnItemsinBackpack()
    {
        foreach(Resource res in BP.playerBag)
        {
            if(res.Amount > 0)
            {
                BP.playerBag_ListItem(res.id,myTrans.position);
            }
        }
    }
}
