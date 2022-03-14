using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private GameObject globalManager;
    private Vector3 tempTransform;
    private Quaternion tempRotation;
    public Camera cam;
    public float turnSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.Find("GlobalManager");
        tempTransform = transform.position;
        tempRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (!globalManager.GetComponent<GlobalManager>().isInvestigate)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Outline>())
                {
                    Vector3 dir;
                    dir = hit.transform.position - transform.position;
                    Quaternion targetQua = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, tempRotation, Time.deltaTime * turnSpeed);
                }
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, tempRotation, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, tempRotation, Time.deltaTime * turnSpeed);
        }

    }
}
