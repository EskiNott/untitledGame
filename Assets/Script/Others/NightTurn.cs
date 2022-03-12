using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTurn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(90, transform.forward) * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation =
    }
}
