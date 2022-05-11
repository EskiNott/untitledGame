using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float Speed;
    public float left_x;
    public float right_x;

    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (movingRight)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(right_x, transform.localPosition.y, 0), Speed * Time.deltaTime);

            if (transform.localPosition.x >= right_x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(left_x, transform.localPosition.y, 0), Speed * Time.deltaTime);
            if (transform.localPosition.x <= left_x)
            {
                movingRight = true;
            }
        }
    }
}
