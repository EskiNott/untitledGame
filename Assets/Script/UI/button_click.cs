using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_click : MonoBehaviour
{
    private bool isClick = false;   //Ĭ���ǲ���ʾ��

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


