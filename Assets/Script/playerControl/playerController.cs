using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float movementSpeed = 1.0f;
    public GameObject BackpackPanel;
    void Start()
    {
        player = GetComponent<GameObject>();
        Transform canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        BackpackPanel = canvas.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, verticalInput * movementSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.B)){
            BackpackPanel.SetActive(!BackpackPanel.activeSelf);
        }
    }
}
