using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;
    private Image image;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform trans = GetComponentInChildren<Transform>();
        image = trans.GetChild(1).GetComponent<Image>();
        text = trans.GetChild(0).GetComponent<Text>();
    }
}
