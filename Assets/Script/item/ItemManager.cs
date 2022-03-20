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
            case 3:
                break;
            case 4:
                break;
            case 5:
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
/*        if (!globalManager.GetComponent<GlobalManager>().isInvestigate)
        {*/
            goItem.thisInvestigate = true;
            go.GetComponent<Outline>().enabled = false;
            globalManager.GetComponent<GlobalManager>().isInvestigate = true;
            goTrans.position = cam.transform.position + cam.transform.forward * goItem.checkDistance;
            goTrans.LookAt(cam.transform.position);
/*        }*/
        options = 1;
        iMM.clearButton();
    }

    public void itemAttack()
    {
        options = 0;
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
        //�����Ҫ�߼�
        if (globalManager.GetComponent<GlobalManager>().isInvestigate
            && goItem.thisInvestigate
            && (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //�϶��߼�--------------------------------------------------------------------------------------------------------��
            //�϶���ʼ�ж�
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit)
                && ((hit.collider.gameObject == go) || isRayHitChildOrFather(hit, go)))
            {
                tempRotationDragging = goTrans.rotation.eulerAngles;
                MousePos1 = Input.mousePosition;
                isDragging = true;
            }
            //�˳�����ж�
            else if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && !isDragging)
            {
                if (hit.collider.gameObject != go)
                {
                    if (!isRayHitChildOrFather(hit, go))
                    {
                        goTrans.position = prePosition;
                        goTrans.rotation = preRotation;
                        goItem.thisInvestigate = false;
                        globalManager.GetComponent<GlobalManager>().isInvestigate = false;
                    }
                }
            }
            //�϶��߼�
            else if (Input.GetMouseButton(0)) 
            {
                MousePos2 = Input.mousePosition;
                goTrans.eulerAngles = new Vector3(tempRotationDragging.x + (MousePos1.y - MousePos2.y) * rotateSpeed, tempRotationDragging.y + (MousePos1.x - MousePos2.x) * rotateSpeed, 0);
            }
            //�����϶��ж�
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
            //�϶��߼�--------------------------------------------------------------------------------------------------------��

            //�����߼�--------------------------------------------------------------------------------------------------------��
            float mouseCenter = Input.GetAxis("Mouse ScrollWheel");
            if (mouseCenter < 0)
            {
                go.GetComponent<item>().checkDistance += 10 * Time.deltaTime;
            }else if (mouseCenter > 0)
            {
                go.GetComponent<item>().checkDistance -= 10 * Time.deltaTime;
            }
            //�����߼�--------------------------------------------------------------------------------------------------------��
        }
    }

    static private bool isRayHitChildOrFather(RaycastHit hit, GameObject go)
    {
        bool isColliderHitChildOrFather = false;
        GameObject tempGo = hit.collider.gameObject;
        Transform father = go.GetComponentInChildren<Transform>();
        foreach (Transform child in father)
        {
            if (hit.collider.gameObject == child.gameObject)
            {
                isColliderHitChildOrFather = true;
                break;
            }
        }
        if (!isColliderHitChildOrFather)
        {
            while (tempGo.transform.parent != null)
            {
                tempGo = tempGo.transform.parent.gameObject;
                if (tempGo = go)
                {
                    isColliderHitChildOrFather = true;
                    break;
                }
            }
        }
        return isColliderHitChildOrFather;
    }
}
