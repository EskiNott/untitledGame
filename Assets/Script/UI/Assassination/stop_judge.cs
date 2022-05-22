using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_judge : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject needle;
    private float isRed;
    private float isBlue;
    int flag;
    
    private move redMove;
    private move blueMove;
    private move needleMove;
    private change_size redChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        stop();
    }
    void stop()
    {
        redMove = red.GetComponent<move>();
        blueMove = blue.GetComponent<move>();
        needleMove = needle.GetComponent<move>();
        redChange = red.GetComponent<change_size>();
        if (Input.GetKey(KeyCode.Space))
        {
            redMove.Speed = 0;
            blueMove.Speed = 0;
            needleMove.Speed = 0;
            redChange.scaleChange.x = 0;
            flag = Judge();
            Debug.Log(flag);
            gameObject.SetActive(false);
        }
    }
    int Judge()
    {
        isRed = System.Math.Abs(needle.transform.localPosition.x - red.transform.localPosition.x);
        isBlue = System.Math.Abs(needle.transform.localPosition.x - blue.transform.localPosition.x);

        if (isRed <= red.transform.localScale.x / 2)
        {
            return 1;
        }
        else if (isBlue <= blue.transform.localScale.x / 2)
        {
            return 0;
        }
        else
        {
            return -1;
        }

    }

}
