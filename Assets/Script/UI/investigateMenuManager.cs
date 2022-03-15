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

    private Transform hitTransform;
    private Transform menuTransform;
    private ItemManager myItemM;
    private Transform myTransform;
    private GlobalManager gm;
    // Start is called before the first frame update
    void Start()
    {
        menuTransform = GameObject.Find("investigateMenu").GetComponent<Transform>();
        isMenuOpened = false;
        myItemM = GetComponent<ItemManager>();
        myTransform = GetComponent<Transform>();
        gm = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)&& !RaycastUI())
        {
            isMenuOpened = false;
            clearButton();
        }
        if (Physics.Raycast(ray, out hit) && !isMenuOpened && !gm.isInvestigate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitTransform = hit.collider.gameObject.GetComponent<Transform>();
                myItemM.go = hitTransform.gameObject;
                if (hitTransform.gameObject.GetComponent<item>() && hitTransform.gameObject.GetComponent<item>().interact.Length > 0)
                {
                    for (int i = 0; i < hitTransform.gameObject.GetComponent<item>().interact.Length; i++)
                    {
                        if (hitTransform.gameObject.GetComponent<item>().interact[i])
                        {
                            investigateMenu[i].SetActive(true);
                        }
                    }
                    menuTransform.position = new Vector3(cam.WorldToScreenPoint(hitTransform.position).x,
                                                         cam.WorldToScreenPoint(hitTransform.position).y,
                                                         cam.WorldToScreenPoint(hitTransform.position).z);
                    isMenuOpened = true;
                }
            }
        }
    }

    public void clearButton()
    {
        while (GameObject.FindWithTag("itemInvestigateOption"))
        {
            GameObject.FindWithTag("itemInvestigateOption").SetActive(false);
        }
    }

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
}
