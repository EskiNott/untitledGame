using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public Transform camTrans;
    public float posSpeed;
    public float rotSpeed;

    private Vector3 oPosition;
    private Quaternion oRotation;
    private bool lerpFlag;
    private Transform lerpTrans;
    // Start is called before the first frame update
    void Start()
    {
        camTrans = cam.transform;
        lerpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpFlag)
        {
            camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, oPosition + lerpTrans.position, Time.deltaTime * (posSpeed + 2));
            camTrans.localRotation = Quaternion.Lerp(camTrans.localRotation, Quaternion.Euler(oRotation.eulerAngles + lerpTrans.rotation.eulerAngles), Time.deltaTime * rotSpeed);
            if (ItemCheckMethod.isFinishVector(camTrans.localPosition, oPosition + lerpTrans.position) && ItemCheckMethod.isFinishQuaternion(camTrans.localRotation, Quaternion.Euler(oRotation.eulerAngles + lerpTrans.rotation.eulerAngles)))
            {
                lerpFlag = false;
            }
        }
    }

    public void setCamTrans(Transform trans)
    {
        oPosition = camTrans.position;
        oRotation = camTrans.rotation;
        camTrans.position = trans.position;
        camTrans.rotation = trans.rotation;
    }

    public void camLerpMoving(Transform trans)
    {
        oPosition = camTrans.position;
        oRotation = camTrans.rotation;
        lerpTrans = trans;
        lerpFlag = true;
    }
}
