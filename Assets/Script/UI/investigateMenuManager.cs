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

    private Ray ray;
    private RaycastHit hit;

    private Transform hitTransform;
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
    }

    public void set2DMenu(item itemAttribute)
    {

    }

    private void set3DMenu()
    {
        //�˵������߼�
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
                    isMenuOpened = true;
                }
            }

        }
        //���ò˵�λ��
        setMenu3DPosition();
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

    //�˵��ر�
    public void closeMenu()
    {
        if (Input.GetMouseButtonDown(0) && !RaycastMenuUI() && isMenuOpened)
        {
            isMenuOpened = false;
            clearButton();
        }
    }
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

    //�������Ƿ��ڲ˵�UI��
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
