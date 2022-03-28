using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGame : MonoBehaviour
{
    public GameObject camManager;
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        camManager.GetComponent<CameraManager>().setCamTrans(GameObject.Find("Camera_Door").transform);
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
