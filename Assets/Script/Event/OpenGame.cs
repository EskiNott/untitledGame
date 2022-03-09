using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGame : MonoBehaviour
{
    public GameObject HomeScene;
    public GameObject DoorScene;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        HomeScene.SetActive(false);
        DoorScene.SetActive(true);
        MainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
