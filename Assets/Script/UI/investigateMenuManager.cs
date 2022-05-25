using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class investigateMenuManager : MonoBehaviour
{
    public Camera cam;
    public GameObject[] investigateMenu;
    public bool isMenuOpened;
    public CameraManager cm;
    public GlobalManager gm;
    public backpack bp;
    public GameObject MenuItemNameGO;

    private bool showButtons = false;

    private Ray ray;
    private RaycastHit hit;

    private Transform hitTransform;
    private bool Menu3d;
    [SerializeField]
    private Transform menuTransform;
    [SerializeField]
    private ItemManager myItemM;

    // Start is called before the first frame update
    void Start()
    {
        isMenuOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        closeMenu();
        set3DMenu();
        showButton();
    }

    public void set2DMenu(item itemAttribute)
    {
        foreach (GameObject child in investigateMenu)
        {
            if (child.name != "ItemName")
            {
                child.SetActive(false);
            }
        }
        MenuItemNameGO.GetComponentInChildren<Text>().text = itemAttribute.ItemName;
        MenuItemNameGO.SetActive(true);
        myItemM.go = itemAttribute.gameObject;
        for (int i = 2; i < itemAttribute.interact.Length; i++)
        {
            if (itemAttribute.interact[i] && i != 5) 
            {
                investigateMenu[i].SetActive(true);
            }
            showButton(true);
        }
        isMenuOpened = true;
        Menu3d = false;
    }

    private void GraduallyAppear(CanvasGroup canvasgGroup, bool setAppear)
    {
        bool isFinish = false;
        int targetAlpha;
        targetAlpha = setAppear ? 1 : 0;

        if(!isFinish)
        {
            canvasgGroup.alpha = Mathf.Lerp(canvasgGroup.alpha, targetAlpha, Time.deltaTime * 20);
            if (targetAlpha < 0.0001 && !setAppear)
            {
                isFinish = true;

            }else if (targetAlpha > 0.9999 & setAppear)
            {
                isFinish = true;
            }
            canvasgGroup.gameObject.SetActive((canvasgGroup.alpha > 0.01) ? true : false);
        }

    }

    private void set3DMenu()
    {
        //菜单开启逻辑
        if (Physics.Raycast(ray, out hit) && !isMenuOpened && !gm.isInvestigate && !gm.IsOpenPlayerBag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitTransform = getFatherOrThishasItem(hit).transform;
                myItemM.go = hitTransform.gameObject;
                if (hitTransform.gameObject.GetComponent<item>() && hitTransform.gameObject.GetComponent<item>().interact.Length > 0)
                {
                    hitTransform.GetComponent<OutlinePointerEvent>().forceOn = true;
                    cm.camFocus(hitTransform);
                    MenuItemNameGO.GetComponentInChildren<Text>().text = hitTransform.gameObject.GetComponent<item>().ItemName;
                    MenuItemNameGO.SetActive(true);
                    for (int i = 0; i < hitTransform.gameObject.GetComponent<item>().interact.Length; i++)
                    {
                        if (hitTransform.gameObject.GetComponent<item>().interact[i])
                        {
                            investigateMenu[i].SetActive(true);
                        }
                    }
                    showButton(true);
                    isMenuOpened = true;
                    Menu3d = true;
                }
            }
        }
        //设置菜单位置
        if(!gm.IsOpenPlayerBag)
        setMenu3DPosition();
    }
    public void setMenu2DPosition(Transform targetTrans)
    {
        if (isMenuOpened)
        {
            menuTransform.position = targetTrans.position;
        }
    }

    public void setMenu3DPosition()
    {
        if (isMenuOpened)
        {
            menuTransform.position = new Vector3(cam.WorldToScreenPoint(hitTransform.position).x,
                                 cam.WorldToScreenPoint(hitTransform.position).y,
                                 cam.WorldToScreenPoint(hitTransform.position).z);
        }
    }

    //菜单关闭
    public void closeMenu()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.B)) && !RaycastMenuUI() && isMenuOpened) 
        {
            foreach (GameObject child in investigateMenu)
            {
                if (child.name != "ItemName")
                {
                    child.SetActive(false);
                }
            }
            isMenuOpened = false;
            showButton(false);
            if (Menu3d)
            {
                hitTransform.GetComponent<OutlinePointerEvent>().forceOn = false;
                hitTransform.GetComponent<Outline>().enabled = false;
                cm.camRevert();
            }

        }
    }
    public void showButton()
    {
        if (!showButtons)
        {
            GraduallyAppear(menuTransform.GetComponent<CanvasGroup>(), false);
        }
        else
        {
            GraduallyAppear(menuTransform.GetComponent<CanvasGroup>(), true);
        }
    }
    public void showButton(bool ifShow)
    {
        showButtons = ifShow;
    }

    //检测鼠标是否在菜单UI上
    public static bool RaycastMenuUI()
    {
        if (EventSystem.current == null)
            return false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
            return results[0].gameObject.CompareTag("itemInvestigateOption");
        else
            return false;
    }

    public GameObject getFatherOrThishasItem(RaycastHit hit)
    {
        GameObject go = hit.collider.gameObject;
        if (hit.collider.gameObject.GetComponent<item>() == null)
        {
            while (go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                if (go.GetComponent<item>())
                {
                    break;
                }
            }
        }
        return go;
    }
}
