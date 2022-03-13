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
    private Vector2 MousePos1;
    private Vector2 MousePos2;
    private Vector3 tempRotationDragging;
    private bool isDragging = false;

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
        if (globalManager.GetComponent<GlobalManager>().isInvestigate && thisInvestigate && (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && (hit.collider.gameObject == gameObject)) 
            {
                tempRotationDragging = transform.rotation.eulerAngles;
                MousePos1 = Input.mousePosition;
                isDragging = true;
            }
            else if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && !isDragging)
            {
                if (hit.collider.gameObject != gameObject)
                {
                    transform.position = tempPosition;
                    transform.rotation = tempRotation;
                    thisInvestigate = false;
                    globalManager.GetComponent<GlobalManager>().isInvestigate = false;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                MousePos2 = Input.mousePosition;
                transform.eulerAngles = new Vector3(tempRotationDragging.x + MousePos1.y - MousePos2.y, tempRotationDragging.y + +MousePos1.x - MousePos2.x, 0);
            }else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
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
