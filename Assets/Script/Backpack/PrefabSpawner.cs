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

    }

    public void deleteAllChild()
    {
        foreach(Transform temp in myTrans)
        {
            Destroy(temp.gameObject);
        }
    }
    public void spawnItemsinBackpack()
    {
        Vector3 SpawnPosition;
        foreach(Resource res in BP.playerBag)
        {
            if(res.Amount > 0)
            {
                for(int i = 0; i < res.Amount; i++)
                {
                    SpawnPosition = new Vector3(BP.Camera_Backpack.transform.position.x + Random.Range(-1f, 1f)
                    , BP.Camera_Backpack.transform.position.y
                    , BP.Camera_Backpack.transform.position.z + Random.Range(-0.5f, 0.5f));

                    playerBag_ListItem(res.id, SpawnPosition);
                }
            }
        }
    }

    public bool playerBag_ListItem(int id, Vector3 pos)
    {
        Resource tempRes = BP._ResourceFindInPlayerBag(id);
        if (tempRes != null)
        {
            GameObject tempPrefab;
            tempPrefab = Instantiate((GameObject)Resources.Load("Prefabs\\Resources\\" + tempRes.prefabName), pos, Quaternion.identity);
            if(tempPrefab != null)
            {
                tempPrefab.transform.parent = myTrans;
                tempPrefab.GetComponent<item>().ItemName = tempRes.Name;
            }
            return true;
        }
        return false;
    }
}
