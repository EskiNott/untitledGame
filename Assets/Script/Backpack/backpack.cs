using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpack : MonoBehaviour
{
    private LinkedList<Resource> playerBag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool playerBag_Add(Resource res)
    {
        if (res != null)
        {
            playerBag.AddLast(res);
            return true;
        }
        else
        {
            return false;
        }

    }

    public LinkedList<Resource> playerBag_Return()
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

    public bool playerBag_RemoveResource(int id)
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
}
