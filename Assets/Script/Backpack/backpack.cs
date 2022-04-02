using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class backpack : MonoBehaviour
{
    static string jsonAddress = "Prefabs\\Resources\\ItemList";
    [SerializeField]
    public List<Resource> playerBag;
    public List<GameObject> resourcesList;
    // Start is called before the first frame update
    void Start()
    {
        playerBag = ParseTextJSON.ParseResourceListJSON(jsonAddress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool playerBag_Add(Resource res)
    {
        if (res != null)
        {
            playerBag.Add(res);
            return true;
        }
        return false;
    }

    public List<Resource> playerBag_Get()
    {
        return playerBag;
    }

    public bool playerBag_Decrease(int id)
    {
        foreach(Resource tempRes in playerBag)
        {
            if(tempRes.id == id)
            {
                if(tempRes.amount > 0)
                {
                    tempRes.amount--;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public bool playerBag_Reset(int id)
    {
        foreach(Resource tempRes in playerBag)
        {
            if(tempRes.id == id)
            {
                tempRes.amount = 0;
                return true;
            }
        }
        return false;
    }

    public bool playerBag_ListItem(int id,Vector3 pos)
    {
        foreach (Resource tempRes in playerBag)
        {
            if (tempRes.id == id)
            {
                foreach(GameObject tempGo in resourcesList)
                {
                    if(tempGo.name == tempRes.prefabName)
                    {
                        Instantiate(tempGo, pos, Quaternion.identity);
                        return true;
                    }
                }

            }
        }
        return false;
    }
}
