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
    public GameObject MenuItemNameGO;

    [SerializeField]
    private Transform hitTransform;
    private Transform menuTransform;
    private ItemManager myItemM;
    // Start is called before the first frame update
    void Start()
    {
        menuTransform = GameObject.Find("investigateMenu").GetComponent<Transform>();
        isMenuOpened = false;
        myItemM = GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //菜单关闭逻辑
        if (Input.GetMouseButtonDown(0) && !RaycastUI() && isMenuOpened)
        {
            isMenuOpened = false;
            clearButton();
        }
        //菜单开启逻辑
        if (Physics.Raycast(ray, out hit) && !isMenuOpened && !gm.isInvestigate)
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
                    isMenuOpened = true;
                }
            }

        }
        //设置菜单位置
        if (isMenuOpened)
        {
            menuTransform.position = new Vector3(cam.WorldToScreenPoint(hitTransform.position).x,
                                 cam.WorldToScreenPoint(hitTransform.position).y,
                                 cam.WorldToScreenPoint(hitTransform.position).z);
        }
    }

    //菜单关闭
    public void clearButton()
    {
        while (GameObject.FindWithTag("itemInvestigateOption"))
        {
            GameObject.FindWithTag("itemInvestigateOption").SetActive(false);
        }
        hitTransform.GetComponent<OutlinePointerEvent>().forceOn = false;
        hitTransform.GetComponent<Outline>().enabled = false;
        cm.camRevert();
    }

    //检测鼠标是否在UI上
    public static bool RaycastUI()
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
