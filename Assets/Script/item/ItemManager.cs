using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject globalManager;
    private investigateMenuManager iMM;
    private Vector3 prePosition;
    private Quaternion preRotation;
    public bool isDragging = false;
    private Vector3 tempRotationDragging;
    private Vector3 tempPositionDragging;
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
                if (!goItem.isSizeBig)
                {
                    _itemCheckPick(mCam);
                }
                else
                {
                    _itemCheckClose(mCam);
                }

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

        //拿起检查
        if (!goItem.isSizeBig)
        {
            prePosition = new Vector3(goTrans.position.x, goTrans.position.y, goTrans.position.z);
            preRotation = new Quaternion(goTrans.rotation.x, goTrans.rotation.y, goTrans.rotation.z, goTrans.rotation.w);
            if (!globalManager.GetComponent<GlobalManager>().isInvestigate)
            {
                goItem.thisInvestigate = true;
                go.GetComponent<Outline>().enabled = false;
                globalManager.GetComponent<GlobalManager>().isInvestigate = true;
                goTrans.position = cam.transform.position + goTrans.forward * goItem.checkDistance;
                goTrans.LookAt(cam.transform.position);
            }
        }
        else
        //靠近观察
        {
            prePosition = new Vector3(cam.gameObject.transform.position.x, cam.gameObject.transform.position.y, cam.gameObject.transform.position.z);
            preRotation = new Quaternion(cam.gameObject.transform.rotation.x, cam.gameObject.transform.rotation.y, cam.gameObject.transform.rotation.z, cam.gameObject.transform.rotation.w);
            if (!globalManager.GetComponent<GlobalManager>().isInvestigate)
            {
                goItem.thisInvestigate = true;
                go.GetComponent<Outline>().enabled = false;
                globalManager.GetComponent<GlobalManager>().isInvestigate = true;
                cam.transform.position = goTrans.position + goTrans.forward * goItem.checkDistance;
                cam.transform.LookAt(goTrans.position);
            }
        }

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


    //检查逻辑
    private void _itemCheckPick(Camera cam)
    {
        //主要逻辑
        if (globalManager.GetComponent<GlobalManager>().isInvestigate
            && goItem.thisInvestigate
            && (Input.GetMouseButtonDown(0)
            || Input.GetMouseButton(1)
            || Input.GetMouseButtonDown(1)
            || Input.GetMouseButtonUp(1)))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //拖动逻辑--------------------------------------------------------------------------------------------------------↓
            //拖动开始判定
            if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit)
                && ((hit.collider.gameObject == go) || isRayHitThis(hit, goItem.childParts)))
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
                    if (!isRayHitThis(hit, goItem.childParts))
                    {
                        goTrans.position = prePosition;
                        goTrans.rotation = preRotation;
                        goItem.thisInvestigate = false;
                        globalManager.GetComponent<GlobalManager>().isInvestigate = false;
                    }
                }
            }
            //拖动逻辑
            else if (Input.GetMouseButton(1)) 
            {
                MousePos2 = Input.mousePosition;
                goTrans.eulerAngles = new Vector3(tempRotationDragging.x + (MousePos1.y - MousePos2.y) * rotateSpeed, tempRotationDragging.y + (MousePos1.x - MousePos2.x) * rotateSpeed, 0);
            }
            //结束拖动判定
            if (Input.GetMouseButtonUp(1))
            {
                isDragging = false;
            }
            //拖动逻辑--------------------------------------------------------------------------------------------------------↑
        }
        scrollZoom(cam.gameObject.transform, goTrans);
    }


    //观察逻辑
    private void _itemCheckClose(Camera cam)
    {
        Transform camTrans = cam.transform;
        //主要逻辑
        if (globalManager.GetComponent<GlobalManager>().isInvestigate
            && goItem.thisInvestigate
            && (Input.GetMouseButtonDown(0)
            || Input.GetMouseButton(1)
            || Input.GetMouseButtonDown(1)
            || Input.GetMouseButtonUp(1))) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //拖动逻辑--------------------------------------------------------------------------------------------------------↓
            //拖动开始判定
            if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit)
                && ((hit.collider.gameObject == go) || isRayHitThis(hit, goItem.childParts)))
            {
                tempPositionDragging = camTrans.position;
                tempRotationDragging = camTrans.rotation.eulerAngles;
                MousePos1 = Input.mousePosition;
                isDragging = true;
            }
            //退出检查判定
            else if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && !isDragging)
            {
                if (hit.collider.gameObject != go)
                {
                    if (!isRayHitThis(hit, goItem.childParts))
                    {
                        camTrans.position = prePosition;
                        camTrans.rotation = preRotation;
                        goItem.thisInvestigate = false;
                        globalManager.GetComponent<GlobalManager>().isInvestigate = false;
                    }
                }
            }
            //拖动逻辑
            else if (Input.GetMouseButton(1))
            {
                MousePos2 = Input.mousePosition;
                float rotateY = MousePos2.x - MousePos1.x;
                float rotateZ = MousePos1.y - MousePos2.y;
                if (rotateY > goItem.maxRotation_Y) 
                {
                    rotateY = goItem.maxRotation_Y;
                }else if (rotateY < -goItem.maxRotation_Y)
                {
                    rotateY = -goItem.maxRotation_Y;
                }
                if(rotateZ > goItem.maxRotation_Z)
                {
                    rotateZ = goItem.maxRotation_Z;
                }else if(rotateZ < -goItem.maxRotation_Z)
                {
                    rotateZ = -goItem.maxRotation_Z;
                }

                Quaternion tempRotate = Quaternion.Euler(new Vector3(
                    0
                    , rotateY
                    , rotateZ
                    ));

                camTrans.position = goTrans.position + tempRotate * (tempPositionDragging - goTrans.position).normalized * goItem.checkDistance;
                camTrans.LookAt(goTrans);
                
            }
            //结束拖动判定
            if (Input.GetMouseButtonUp(1))
            {
                isDragging = false;
            }
            //拖动逻辑--------------------------------------------------------------------------------------------------------↑
        }
        scrollZoom(goTrans, cam.gameObject.transform);
    }

    private void scrollZoom(Transform stabledItem, Transform movingItem)
    {
        //缩放逻辑
        float mouseCenter = Input.GetAxis("Mouse ScrollWheel");
        if (mouseCenter < 0 && goItem.checkDistance < goItem.maxCheckDistance)
        {
            goItem.checkDistance += 10 * Time.deltaTime;
            movingItem.position = stabledItem.position + (movingItem.position - stabledItem.position).normalized * goItem.checkDistance;
        }
        else if (mouseCenter > 0 && goItem.checkDistance > goItem.minCheckDistance)
        {
            goItem.checkDistance -= 10 * Time.deltaTime;
            movingItem.position = stabledItem.position + (movingItem.position - stabledItem.position).normalized * goItem.checkDistance;
        }
    }

    static private bool isRayHitThis(RaycastHit hit, GameObject[] gos)
    {
        bool isHit = false;
        foreach(GameObject tempGo in gos)
        {
            if(tempGo == hit.collider.gameObject)
            {
                isHit = true;
                break;
            }
        }
        return isHit;
    }
}
