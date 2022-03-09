using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGame : MonoBehaviour
{
    public Camera Camera_Home;
    public Camera Camera_Door;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        Camera_Home.enabled = false;
        Camera_Door.enabled = true;
        MainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
