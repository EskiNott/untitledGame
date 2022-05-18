using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class backpack : MonoBehaviour
{
    static string jsonPath = "Prefabs\\Resources\\ItemList";
    static string ItemSpritePath = "Sprites\\Items";
    static string ResourcesPrefabPath = "Prefabs\\Resources";

    public List<Resource> playerBag;
    public GlobalManager gm;
    public GameObject BackpackPanel;
    public investigateMenuManager iMM;

    [SerializeField]
    private GameObject SlotParent;
    [SerializeField]
    private Transform SlotParentTrans;
    [SerializeField]
    private GameObject SlotPrefab;
    [SerializeField]
    private CanvasGroup bpCG;
    [SerializeField]
    private RectTransform playerBagPanelRectTrans;

    [SerializeField]
    private RectTransform WeightProgressTrans;
    [SerializeField]
    private RectTransform VolumeProgressTrans;

    public float PanelMovingSpeed = 15.0f;
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
        panelControl_Progress();
    }
    private void panelControl_Appear()
    {
        if (gm.canOpenBag)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                gm.IsOpenPlayerBag = !gm.IsOpenPlayerBag;
                panelControl_Refresh();
            }
            int targetAlpha;
            if (gm.IsOpenPlayerBag)
            {
                targetAlpha = 1;
                if(bpCG.alpha > 0.001)
                {
                    BackpackPanel.SetActive(true);
                }
            }
            else
            {
                targetAlpha = 0;
                if(bpCG.alpha < 0.001)
                {
                    BackpackPanel.SetActive(false);
                    bpCG.alpha = 0;
                }
            }
            bpCG.alpha = Mathf.Lerp(bpCG.alpha, targetAlpha, Time.deltaTime * PanelMovingSpeed);
        }

    }
    private void panelControl_Refresh()
    {
        foreach(Transform item in SlotParentTrans)
        {
            Destroy(item.gameObject);
        }
        panelControl_ItemList();
    }
    private void panelControl_Progress()
    {
        Vector2 targetVol = new Vector2(
            VolumeProgressTrans.parent.GetComponent<RectTransform>().sizeDelta.x,
            VolumeProgressTrans.parent.GetComponent<RectTransform>().sizeDelta.y
            *(playerBagVolumeNow/playerBagVolumeMax)
            );
        VolumeProgressTrans.sizeDelta = Vector2.Lerp(VolumeProgressTrans.sizeDelta,
            targetVol,
            Time.deltaTime * PanelMovingSpeed * 0.5f);
        Vector2 targetWei = new Vector2(
            WeightProgressTrans.parent.GetComponent<RectTransform>().sizeDelta.x,
            WeightProgressTrans.parent.GetComponent<RectTransform>().sizeDelta.y
            * (playerBagMassNow / playerBagMassMax)
            );
        WeightProgressTrans.sizeDelta = Vector2.Lerp(WeightProgressTrans.sizeDelta,
            targetWei,
            Time.deltaTime * PanelMovingSpeed * 0.5f);
    }

    private void panelControl_ItemList()
    {
        GameObject _Slot;
        Transform _SlotTrans;
        Image _ChildImage;
        if (gm.IsOpenPlayerBag)
        {
            foreach(Resource res in playerBag)
            {
                _Slot = Instantiate(SlotPrefab, SlotParent.transform);
                _SlotTrans = _Slot.GetComponent<Transform>();
                foreach(Transform _p in _SlotTrans)
                {
                    _p.GetComponent<CanvasGroup>().alpha = 1.0f;
                    foreach (Transform _t in _p)
                    {
                        if (_t.gameObject.name == "Sprite")
                        {
                            _ChildImage = _t.GetComponent<Image>();
                            _ChildImage.sprite = FindItemSpriteWithName(res.prefabName);
                        }
                        else if (_t.gameObject.name == "Amount")
                        {
                            _t.GetComponent<Text>().text = res.Amount.ToString();
                        }else if(_t.gameObject.name == "ItemAttribute")
                        {
                            UnityEditorInternal.ComponentUtility.CopyComponent(FindItemWithPrefabName(res.prefabName));
                            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(_t.gameObject);

                            _Slot.GetComponent<Button>().onClick.AddListener(delegate {
                                iMM.set2DMenu(_t.GetComponent<item>());
                                iMM.setMenu2DPosition(_t);
                            });

                        }
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
                panelControl_Refresh();
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
                panelControl_Refresh();
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
            panelControl_Refresh();
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
    public item FindItemWithPrefabName(String PrefabName)
    {
        return Resources.Load<GameObject>(ResourcesPrefabPath + "\\" + PrefabName).GetComponent<item>();
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
