using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class backpack : MonoBehaviour
{
    static string jsonPath = "Prefabs\\Resources\\ItemList";
    static string ItemSpritePath = "Sprites\\Items";

    public List<Resource> playerBag;
    public GlobalManager gm;
    public GameObject BackpackPanel;

    [SerializeField]
    private GameObject SlotParent;
    [SerializeField]
    private GameObject SlotPrefab;

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
        playerBag = ParseTextJSON.ParseResourceListJSON(jsonPath);
    }

    // Update is called once per frame
    void Update()
    {
        panelControl_Appear();
    }
    private void panelControl_Appear()
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

    private void panelControl_ItemList()
    {
        GameObject _Slot;
        Transform _SlotTrans;
        if (IsOpenPlayerBag)
        {
            foreach(Resource res in playerBag)
            {
                for(int i = 0; i < res.Amount; i++)
                {
                    _Slot = Instantiate(SlotPrefab, SlotParent.transform);
                    _SlotTrans = _Slot.GetComponent<Transform>();
                    foreach(Transform _t in _SlotTrans)
                    {
                        _t.GetComponent<Image>().sprite = FindItemSpriteWithName(res.prefabName);
                    }
                }
            }
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

    public Sprite FindItemSpriteWithName(String PrefabName)
    {
        return Resources.Load<Sprite>(ItemSpritePath + "\\" + PrefabName);
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
