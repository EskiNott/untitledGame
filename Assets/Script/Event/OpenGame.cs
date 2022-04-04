using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGame : MonoBehaviour
{
    public GameObject Camera_Door;
    public GameObject camManager;
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        camManager.GetComponent<CameraManager>().setCamTrans(Camera_Door);
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
