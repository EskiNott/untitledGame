using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class investigate : MonoBehaviour,IPointerClickHandler
{
    private GameObject globalManager;
    private Vector3 tempPosition;
    private Quaternion tempRotation;
    private bool thisInvestigate;
    public bool test = false;
    public Camera cam;
    public float distance = 1.0f;
    // Start is called before the first frame update

    void Start()
    {
        thisInvestigate = false;
        tempPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        tempRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        globalManager = GameObject.Find("GlobalManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (globalManager.GetComponent<GlobalManager>().isInvestigate && Input.GetMouseButton(0) && thisInvestigate)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            test = true;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != gameObject) 
                {
                    transform.position = tempPosition;
                    transform.rotation = tempRotation;
                    thisInvestigate = false;
                    globalManager.GetComponent<GlobalManager>().isInvestigate = false;
                }
                else
                {

                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!globalManager.GetComponent<GlobalManager>().isInvestigate)
        {
            thisInvestigate = true;
            GetComponent<Outline>().enabled = false;
            globalManager.GetComponent<GlobalManager>().isInvestigate = true;
            transform.position = cam.transform.position + cam.transform.forward * distance;
            transform.LookAt(cam.transform.position);
        }
    }
}
