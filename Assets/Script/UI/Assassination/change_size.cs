using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_size : MonoBehaviour
{
    private bool bigger = true;
    public Vector3 scaleChange;
    public double biggest;
    public double smallest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bigger)
        {
            transform.localScale += (scaleChange * Time.deltaTime);
            if (transform.localScale.x > biggest)
            {
                bigger = false;
            }
        }
        else
        {
            transform.localScale -= (scaleChange * Time.deltaTime);
            if (transform.localScale.x < smallest)
            {
                bigger = true;
            }
        }        
    }
}
