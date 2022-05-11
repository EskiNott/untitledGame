using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class backpack : MonoBehaviour
{
    static string jsonPath = "Prefabs\\Resources\\ItemList";
    static string prefabPath = "Prefabs\\Resources";

    public List<Resource> playerBag;
    public GlobalManager gm;
    public GameObject BackpackPanel;

    public GameObject SlotLeft;
    public GameObject SlotRight;
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
                panelControl_Refresh();
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

    private void panelControl_Refresh()
    {
        panelContorl_ClearBag();
        panelControl_ItemList();
    }

    private void panelControl_ItemList()
    {
        GameObject tempGO;
        foreach(Resource res in playerBag)
        {
            for(int i = 0; i < res.Amount; i++)
            {
                tempGO = Instantiate(SlotPrefab, SlotParent.transform);
                foreach(Transform t in tempGO.transform)
                {
                    t.GetComponent<Image>().sprite = _FindItemWithPrefabName(res.prefabName).sprite;
                    t.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    private void panelContorl_ClearBag()
    {
        foreach (Transform t in SlotParent.transform)
        {
            Destroy(t.gameObject);
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
                panelControl_Refresh();
                return true;
            }
            else
            {
                panelControl_Refresh();
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
        panelControl_Refresh();
        return false;
    }

    public bool playerBag_Reset(int id)
    {
        Resource tempRes = _ResourceFindInPlayerBag(id);
        if (tempRes != null) {
            tempRes.Amount = 0;
            return true;
        }
        panelControl_Refresh();
        return false;
    }

    public void playerBag_MassIncrese(int Mass)
    {
        playerBagMassMax += Mass;
        panelControl_Refresh();
    }

    public void playerBag_VolumeIncrease(int Volume)
    {
        playerBagVolumeMax += Volume;
        panelControl_Refresh();
    }

    public item _FindItemWithPrefabName(string PrefabName)
    {
        GameObject tempGO = Resources.Load<GameObject>(prefabPath + "\\" + PrefabName);
        if (tempGO != null)
        {
            return tempGO.GetComponent<item>();
        }
        else
        {
            return null;
        }
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
