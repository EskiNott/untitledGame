using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class backpack : MonoBehaviour
{
    static string jsonAddress = "Prefabs\\Resources\\ItemList";
    public List<Resource> playerBag;
    public List<GameObject> resourcesList;
    public GameObject ResourceSpawnerGO;
    public GameObject GlobalManagerGO;
    public GameObject CameraManagerGO;
    public GameObject Camera_Backpack;

    private CameraManager cm;
    private GlobalManager go;
    private bool IsOpenPlayerBag = false;
    private GameObject preCamera;

    public float playerBagMassMax = 10;
    private float playerBagMassNow = 0;
    public float playerBagVolumeMax = 10;
    private float playerBagVolumeNow = 0;

    // Start is called before the first frame update
    void Start()
    {
        go = GlobalManagerGO.GetComponent<GlobalManager>();
        cm = CameraManagerGO.GetComponent<CameraManager>();
        playerBag = ParseTextJSON.ParseResourceListJSON(jsonAddress);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && go.canSituationCheck && go.isCameraEqualOrigin)
        {
            IsOpenPlayerBag = !IsOpenPlayerBag;
            MoveCameraToBackpack();
        }
    }
    
    public void playerBag_Open(bool isOpen)
    {
        IsOpenPlayerBag = isOpen;
    }
    public List<Resource> playerBag_Get()
    {
        return playerBag;
    }
    public bool playerBag_Add(int id)
    {
        Resource tempRes = _ResourceFindInPlayerBag(id);
        if (tempRes != null)
        {
            if (tempRes.Mass <= playerBagMassMax - playerBagMassNow
                && tempRes.Volume <= playerBagVolumeMax - playerBagVolumeNow) 
            {
                playerBagMassNow += tempRes.Mass;
                playerBagVolumeNow += tempRes.Volume;
                tempRes.Amount++;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool playerBag_Decrease(int id)
    {
        Resource tempRes = _ResourceFindInPlayerBag(id);
        if (tempRes != null)
        {
            if (tempRes.Amount > 0)
            {
                playerBagMassNow -= tempRes.Mass;
                playerBagVolumeNow -= tempRes.Volume;
                tempRes.Amount--;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool playerBag_Reset(int id)
    {
        Resource tempRes = _ResourceFindInPlayerBag(id);
        if (tempRes != null) {
            tempRes.Amount = 0;
            return true;
        }
        return false;
    }

    public bool playerBag_ListItem(int id,Vector3 pos)
    {
        Resource tempRes = _ResourceFindInPlayerBag(id);
        if (tempRes != null)
        {
            foreach (GameObject tempGo in resourcesList)
            {
                if (tempGo.name == tempRes.prefabName)
                {
                    Instantiate((GameObject)Resources.Load("Prefabs\\Resources\\"+tempRes.prefabName), pos, Quaternion.identity);
                    return true;
                }
            }
        }
        return false;
    }

    public void playerBagMassIncrese(int Mass)
    {
        playerBagMassMax += Mass;
    }

    public void playerBagVolumeIncrease(int Volume)
    {
        playerBagVolumeMax += Volume;
    }

    private void MoveCameraToBackpack()
    {
        if (IsOpenPlayerBag)
        {
            preCamera = cm.nowCamera;
            cm.setCamTrans(Camera_Backpack);
        }
        else
        {
            cm.setCamTrans(preCamera);
        }
    }
    private Resource _ResourceFindInPlayerBag(int id)
    {
        foreach (Resource tempRes in playerBag)
        {
            if (tempRes.id == id)
            {
                return tempRes;
            }
        }
        return null;
    }
}
