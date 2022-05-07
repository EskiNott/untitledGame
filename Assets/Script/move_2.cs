using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class move_2 : MonoBehaviour
{
    public float speed;
    public float left_x;
    public float right_x;

    public GameObject red;

    private bool movingRight = true;
    private float isRed;
    private float isBlue;
    int flag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.localPosition.x >= right_x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.localPosition.x <= left_x)
            {
                movingRight = true;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            speed = 0;
            flag = Judge();
            Debug.Log(flag);
            gameObject.SetActive(false);
        }

    }
    int Judge()
    {
        GameObject child1 = GameObject.Find("red");
        GameObject child2 = GameObject.Find("blue");
        GameObject child3 = GameObject.Find("needle");
        isRed = System.Math.Abs(child3.transform.localPosition.x - child1.transform.localPosition.x);
        isBlue = System.Math.Abs(child3.transform.localPosition.x - child2.transform.localPosition.x);
        if (isRed <= child1.transform.localScale.x / 2)
        {
            return 1;
        }
        else if (isBlue <= child2.transform.localScale.x / 2)
        {
            return 0;
        }
        else
        {
            return -1;
        }

    }
}
