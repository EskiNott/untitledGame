using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_click : MonoBehaviour
{
    private bool isClick = false;   //默认是不显示的

    public void Click()
    {
        if (isClick == false)
        {
            gameObject.SetActive(true);
            isClick = true;
        }
        else
        {
            gameObject.SetActive(false);
            isClick = false;
        }
    }

}


