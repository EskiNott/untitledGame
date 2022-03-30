using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private CameraManager CameraM;
    private Quaternion tempRotation;
    private Transform camTrans;

    private investigateMenuManager investigateMenuM;
    public Camera cam;
    public float turnSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        investigateMenuM = GameObject.Find("ItemManager").GetComponent<investigateMenuManager>();
        CameraM = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        camTrans = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (investigateMenuM.isMenuOpened)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Outline>())
                {
                    Vector3 dir;
                    dir = hit.transform.position - camTrans.position;
                    Quaternion targetQua = Quaternion.LookRotation(dir);
                    camTrans.rotation = Quaternion.Lerp(camTrans.rotation, targetQua, Time.deltaTime * turnSpeed);
                }
                else
                {
                    camTrans.rotation = Quaternion.Lerp(camTrans.rotation, CameraM.oRotation, Time.deltaTime * turnSpeed);
                }
            }
            else
            {
                camTrans.rotation = Quaternion.Lerp(camTrans.rotation, CameraM.oRotation, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            camTrans.rotation = Quaternion.Lerp(camTrans.rotation, CameraM.oRotation, Time.deltaTime * turnSpeed);
        }

    }
}
