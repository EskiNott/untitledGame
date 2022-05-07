using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GlobalManager gm;
    [SerializeField]
    private CameraManager cm;
    [SerializeField]
    private backpack bpm;
    private investigateMenuManager iMM;
    private Vector3 tempRotationDragging;
    private Vector3 tempPositionDragging;
    private Vector2 MousePos1;
    private Vector2 MousePos2;
    private int options;
    private item goItem;
    private Transform goTrans;
    private Vector3  ItemPrePosition;
    private Quaternion ItemPreRotation;
    public float rotateSpeed = 1.0f;
    [HideInInspector]
    public bool isDragging = false;
    [HideInInspector]
    public GameObject go;


    private void Start()
    {
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
                    _itemCheckPick();
                }
                else
                {
                    _itemCheckClose();
                    cm.camStop(gm.isInvestigate);
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

    public void itemCheck()
    {
        goItem = go.GetComponent<item>();
        goTrans = go.GetComponent<Transform>();
        //拿起检查
        if (!goItem.isSizeBig)

        {
            if (!gm.isInvestigate)
            {
                ItemPrePosition = new Vector3(goTrans.position.x, goTrans.position.y, goTrans.position.z);
                ItemPreRotation = new Quaternion(goTrans.rotation.x, goTrans.rotation.y, goTrans.rotation.z, goTrans.rotation.w);
                goItem.thisInvestigate = true;
                go.GetComponent<Outline>().enabled = false;
                gm.isInvestigate = true;
                gm.investigateItem = go;
                goTrans.position = cm.oTrans.position + cm.oTrans.rotation*(new Vector3(0, 0, 1)).normalized * goItem.checkDistance;
                goTrans.LookAt(cm.oTrans.position);
                tempRotationDragging = goTrans.rotation.eulerAngles;
            }
        }
        else
        //靠近观察
        {
            if (!gm.isInvestigate)
            {
                goItem.thisInvestigate = true;
                go.GetComponent<Outline>().enabled = false;
                gm.isInvestigate = true;
                gm.investigateItem = go;
                cm.setCamPos(goTrans.position + goTrans.forward * goItem.checkDistance);
                cm.cam.transform.LookAt(goTrans.position);
                tempPositionDragging = cm.cam.transform.position;
                tempRotationDragging = cm.cam.transform.rotation.eulerAngles;
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
    private void _itemCheckPick()
    {
        //主要逻辑
        if (gm.isInvestigate
            && goItem.thisInvestigate)
        {
            if (Input.GetMouseButtonDown(0)
                || Input.GetMouseButton(1)
                || Input.GetMouseButtonDown(1)
                || Input.GetMouseButtonUp(1))
            {
                Ray ray = cm.cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //拖动逻辑--------------------------------------------------------------------------------------------------------↓
                //拖动开始判定
                if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit)
                    && ((hit.collider.gameObject == go) || isRayHitThis(hit, goItem.childParts)))
                {
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
                            go.transform.position = ItemPrePosition;
                            go.transform.rotation = ItemPreRotation;
                            goItem.thisInvestigate = false;
                            gm.isInvestigate = false;
                            gm.investigateItem = null;
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
            scrollZoom(cm.cam.transform, goTrans);
        }

    }


    //观察逻辑
    private void _itemCheckClose()
    {
        Transform camTrans = cm.cam.transform;
        //主要逻辑
        if (gm.isInvestigate
            && goItem.thisInvestigate
            && (Input.GetMouseButtonDown(0)
            || Input.GetMouseButton(1)
            || Input.GetMouseButtonDown(1)
            || Input.GetMouseButtonUp(1))) 
        {
            Ray ray = cm.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //拖动逻辑--------------------------------------------------------------------------------------------------------↓
            //拖动开始判定
            if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit)
                && ((hit.collider.gameObject == go) || isRayHitThis(hit, goItem.childParts)))
            {
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
                        cm.setCamTrans(cm.oTrans);
                        goItem.thisInvestigate = false;
                        gm.isInvestigate = false;
                        gm.investigateItem = null;
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
                }else if (rotateY < goItem.minRotation_Y)
                {
                    rotateY = goItem.minRotation_Y;
                }
                if(rotateZ > goItem.maxRotation_Z)
                {
                    rotateZ = goItem.maxRotation_Z;
                }else if(rotateZ < goItem.minRotation_Z)
                {
                    rotateZ = goItem.minRotation_Z;
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
        scrollZoom(goTrans, cm.cam.transform);
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
