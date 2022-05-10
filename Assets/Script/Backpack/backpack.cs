using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class backpack : MonoBehaviour
{
    static string jsonAddress = "Prefabs\\Resources\\ItemList";
    public List<Resource> playerBag;
    public GlobalManager gm;
    public GameObject BackpackPanel;

    private bool IsOpenPlayerBag = false;

    [SerializeField]
    private RectTransform _BackpackOpenTrans;
    [SerializeField]
    private RectTransform _BackpackCloseTrans;
    [SerializeField]
    private RectTransform playerBagPanelRectTrans;
    public float PanelMovingSpeed = 10.0f;

    public float playerBagMassMax = 10;
    [SerializeField]
    private float playerBagMassNow = 0;
    public float playerBagVolumeMax = 10;
    [SerializeField]
    private float playerBagVolumeNow = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerBag = ParseTextJSON.ParseResourceListJSON(jsonAddress);
    }

    // Update is called once per frame
    void Update()
    {
        playerBagPanelControl();
    }
    private void playerBagPanelControl()
    {
        if (gm.canOpenBag)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                IsOpenPlayerBag = !IsOpenPlayerBag;
            }
            Vector3 _movingTarget;
            if (IsOpenPlayerBag)
            {
                _movingTarget = _BackpackOpenTrans.position;
            }
            else
            {
                _movingTarget = _BackpackCloseTrans.position;
            }
            playerBagPanelRectTrans.position = Vector3.Lerp(playerBagPanelRectTrans.position, _movingTarget, Time.deltaTime * PanelMovingSpeed);
        }
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

    public void playerBagMassIncrese(int Mass)
    {
        playerBagMassMax += Mass;
    }

    public void playerBagVolumeIncrease(int Volume)
    {
        playerBagVolumeMax += Volume;
    }

    public Resource _ResourceFindInPlayerBag(int id)
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
