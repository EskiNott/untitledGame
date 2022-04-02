using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public backpack BP;
    // Start is called before the first frame update
    void Start()
    {
        BP = GameObject.Find("BackpackPanel").GetComponent<backpack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnItemsinBackpack(int itemID)
    {

    }
}
