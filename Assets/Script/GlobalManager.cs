using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public bool isInvestigate;
    public GameObject investigateItem;
    public bool isCameraEqualOrigin;
    public GameObject CameraManagerGO;
    public bool canSituationCheck;

    //EventControl
    public bool canOpenBag = true;

    private CameraManager cm;
    void Start()
    {
        isInvestigate = false;
        isCameraEqualOrigin = false;
        canSituationCheck = true;
        cm = CameraManagerGO.GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isCameraEqualOrigin = (cm.camTrans.position == cm.oTrans.position 
            && cm.camTrans.rotation == cm.oTrans.rotation);
    }

    public void InformationButtonOnClick(GameObject Description)
    {
        Description.SetActive(!Description.activeSelf);
    }
}
