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
    public Transform oTrans;

    private bool lerpFlag;
    private Transform lerpTrans;
    private bool camMove;
    private bool camPause;
    private Transform targetTrans;

    // Start is called before the first frame update
    void Awake()
    {
        camTrans = cam.transform;
        lerpFlag = false;
        camTrans = cam.transform;
        oTrans = new GameObject().transform;
        camPause = false;
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

        if (camMove && !camPause) 
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
        oTrans.rotation = oRotation;
        oTrans.position = oPosition;
    }
    public void setCamRot(Quaternion rot)
    {
        camTrans.rotation = rot;
    }
    public void setCamPos(Vector3 pos)
    {
        camTrans.position = pos;
    }
    public void setCamTrans(Transform trans)
    {
        camInit();
        setCamPos(trans.position);
        setCamRot(trans.rotation);
    }

    public void camLerpMoving(Transform trans)
    {
        lerpTrans = trans;
        lerpFlag = true;
    }

    public void camInit()
    {
        oPosition = camTrans.position;
        oRotation = camTrans.rotation;
    }

    public void camStop(bool isPause)
    {
        camPause = isPause;
    }
    public void camFocus(Transform trans)
    {
        targetTrans = trans;
        camMove = true;
    }

    public void camRevert()
    {
        camMove = false;
    }
}
