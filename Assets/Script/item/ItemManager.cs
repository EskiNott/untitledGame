using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject globalManager;
    private investigateMenuManager iMM;
    private Vector3 prePosition;
    private Quaternion preRotation;
    private bool isDragging = false;
    private Vector3 tempRotationDragging;
    private Vector2 MousePos1;
    private Vector2 MousePos2;
    private int options;
    private Camera mCam;
    private item goItem;
    private Transform goTrans;

    public GameObject go;
    public float rotateSpeed = 1.0f;


    private void Start()
    {
        globalManager = GameObject.Find("GlobalManager");
        iMM = GetComponent<investigateMenuManager>();
        options = -1;
    }

    private void Update()
    {
        switch (options)
        {
            case 0:
                break;
            case 1:
                _itemCheck(mCam, goItem, goTrans);
                break;
            case 2:
                break;
        }
    }

    public void itemCheck(Camera cam)
    {
        mCam = cam;
        goItem = go.GetComponent<item>();
        goTrans = go.GetComponent<Transform>();
        prePosition = new Vector3(goTrans.position.x, goTrans.position.y, goTrans.position.z);
        preRotation = new Quaternion(goTrans.rotation.x, goTrans.rotation.y, goTrans.rotation.z, goTrans.rotation.w);
        if (!globalManager.GetComponent<GlobalManager>().isInvestigate)
        {
            goItem.thisInvestigate = true;
            go.GetComponent<Outline>().enabled = false;
            globalManager.GetComponent<GlobalManager>().isInvestigate = true;
            goTrans.position = cam.transform.position + cam.transform.forward * goItem.checkDistance;
            goTrans.LookAt(cam.transform.position);
        }
        options = 1;
        iMM.clearButton();
    }

    public void itemAttack()
    {

    }
    public void itemTalk()
    {

    }

    public void itemInteract()
    {

    }

    public void itemEat()
    {

    }

    public void itemTake()
    {

    }


    private void _itemCheck(Camera cam, item goItem, Transform goTrans)
    {
        //检查主要逻辑
        if (globalManager.GetComponent<GlobalManager>().isInvestigate
            && goItem.thisInvestigate
            && (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //拖动开始判定
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit)
                && ((hit.collider.gameObject == go) || isRayHitChild(hit, go)))
            {
                tempRotationDragging = goTrans.rotation.eulerAngles;
                MousePos1 = Input.mousePosition;
                isDragging = true;
            }

            //退出检查判定
            else if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && !isDragging)
            {
                if (hit.collider.gameObject != go)
                {
                    if (!isRayHitChild(hit, go))
                    {
                        goTrans.position = prePosition;
                        goTrans.rotation = preRotation;
                        goItem.thisInvestigate = false;
                        globalManager.GetComponent<GlobalManager>().isInvestigate = false;
                    }
                }
            }

            //拖动逻辑
            else if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit) && (hit.collider.gameObject == go || isRayHitChild(hit, go))) 
            {
                MousePos2 = Input.mousePosition;
                goTrans.eulerAngles = new Vector3(tempRotationDragging.x + (MousePos1.y - MousePos2.y) * rotateSpeed, tempRotationDragging.y + (MousePos1.x - MousePos2.x) * rotateSpeed, 0);
            }

            //结束拖动判定
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }

    static private bool isRayHitChild(RaycastHit hit, GameObject go)
    {
        bool _isColliderHitChild = false;
        foreach (Transform child in go.GetComponent<Transform>())
        {
            if (hit.collider.gameObject == child.gameObject)
            {
                _isColliderHitChild = true;
                break;
            }
        }
        return _isColliderHitChild;
    }
}
