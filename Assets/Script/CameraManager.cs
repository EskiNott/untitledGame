using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public Transform camTrans;
    public float posSpeed;
    public float rotSpeed;
    public float turnSpeed = 5.0f;
    public Vector3 oPosition;
    public Quaternion oRotation;

    private bool lerpFlag;
    private Transform lerpTrans;
    [SerializeField]
    private bool camMove;
    [SerializeField]
    private Transform targetTrans;

    // Start is called before the first frame update
    void Awake()
    {
        camTrans = cam.transform;
        lerpFlag = false;
        camTrans = cam.transform;
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

        if (camMove)
        {
            Vector3 dir;
            dir = targetTrans.position - camTrans.position;
            Quaternion targetQua = Quaternion.LookRotation(dir);
            camTrans.rotation = Quaternion.Lerp(camTrans.rotation, targetQua, Time.deltaTime * turnSpeed);
        }
        else
        {
            camTrans.rotation = Quaternion.Lerp(camTrans.rotation, oRotation, Time.deltaTime * turnSpeed);
        }
    }

    public void setCamTrans(Transform trans)
    {
        camReset();
        camTrans.position = trans.position;
        camTrans.rotation = trans.rotation;

    }

    public void camLerpMoving(Transform trans)
    {
        camReset();
        lerpTrans = trans;
        lerpFlag = true;
    }

    public void camReset()
    {
        oPosition = camTrans.position;
        oRotation = camTrans.rotation;
    }
    public void camFocus(Transform trans)
    {
        camReset();
        targetTrans = trans;
        camMove = true;
    }

    public void camRevert()
    {
        camMove = false;
    }
}
