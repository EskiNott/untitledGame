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
    [SerializeField]
    private CameraManager cm;

    private float mouseX;
    private float mouseY;
    [SerializeField]
    private float mouseYaw;
    private float mousePitch;

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

    public float rotateSpeed = 1.0f;


    private void Start()
    {
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
        //ÄÃÆð¼ì²é
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
            }
        }
        else
        //¿¿½ü¹Û²ì
        {
            if (!gm.isInvestigate)
            {
                goItem.thisInvestigate = true;
                go.GetComponent<Outline>().enabled = false;
                gm.isInvestigate = true;
                gm.investigateItem = go;
                cm.setCamPos(goTrans.position + goTrans.forward * goItem.checkDistance);
                cm.cam.transform.LookAt(goTrans.position);
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


    //¼ì²éÂß¼­
    private void _itemCheckPick()
    {
        //Ö÷ÒªÂß¼­
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

                //ÍÏ¶¯Âß¼­--------------------------------------------------------------------------------------------------------¡ý
                if (Input.GetMouseButton(1))
                {
                    goTrans.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * goItem.checkSpeed * 2.0f
                                            , -Input.GetAxis("Mouse X") * goItem.checkSpeed * 2.0f
                                            , 0);
                }
                //ÍË³ö¼ì²éÅÐ¶¨
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
                //ÍÏ¶¯Âß¼­--------------------------------------------------------------------------------------------------------¡ü
            }
            scrollZoom(cm.cam.transform, goTrans);
        }

    }


    //¹Û²ìÂß¼­
    private void _itemCheckClose()
    {
        Transform camTrans = cm.cam.transform;
        //Ö÷ÒªÂß¼­
        if (gm.isInvestigate
            && goItem.thisInvestigate
            && (Input.GetMouseButtonDown(0)
            || Input.GetMouseButton(1)
            || Input.GetMouseButtonDown(1)
            || Input.GetMouseButtonUp(1))) 
        {
            Ray ray = cm.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //ÍÏ¶¯Âß¼­--------------------------------------------------------------------------------------------------------¡ý
            if (Input.GetMouseButton(1))
            {
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");
                mousePitch += mouseY;
                mouseYaw += mouseX;
                if (mousePitch > goItem.maxRotation_Y)
                {
                    mousePitch = goItem.maxRotation_Y;
                }else if (mousePitch < goItem.minRotation_Y)
                {
                    mousePitch = goItem.minRotation_Y;
                }

                if (mouseYaw > goItem.maxRotation_Z)
                {
                    mouseYaw = goItem.maxRotation_Z;
                }
                else if (mouseYaw < goItem.minRotation_Z)
                {
                    mouseYaw = goItem.minRotation_Z;
                }

                Quaternion tempRotate = Quaternion.Euler(new Vector3(
                                        -mousePitch
                                        , mouseYaw
                                        , -mousePitch
                                        ));
                camTrans.position = goTrans.position + tempRotate * goTrans.forward.normalized * goItem.checkDistance;
                camTrans.LookAt(goTrans);
            }
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
            //ÍÏ¶¯Âß¼­--------------------------------------------------------------------------------------------------------¡ü
        }
        scrollZoom(goTrans, cm.cam.transform);
    }

    private void scrollZoom(Transform stabledItem, Transform movingItem)
    {
        //Ëõ·ÅÂß¼­
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
