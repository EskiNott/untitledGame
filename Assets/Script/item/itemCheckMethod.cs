using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCheckMethod : MonoBehaviour
{
    //����任�ṹ�߼�����
    //�ñ���itemTrans�µĶ�����Ϊ��ͬ˳��任����֧����ͬ�㼶˳��Ķ���任
    //ItemTransSub���ڵ�gameobject��Ϊĳ��϶���Ĳ���
    [Serializable]
    public class ItemTransSub
    {
        public GameObject go; //�������
        public Vector3 Position; //����任�����λ��
        public Vector3 Rotation; //���������ת
        [HideInInspector]
        public Vector3 oPosition; //�任ǰλ��
        [HideInInspector]
        public Vector3 oRotation; //�任ǰ��ת
        public float RotSpeed = 1.0f;
        public float PosSpeed = 1.0f;
//        [HideInInspector]
        public scheduleSitu ScheduleSituation = scheduleSitu.initial; //��¼�����˶�״̬
        public bool View; //Ԥ������任��ת���Ľ��
    }

    [Serializable]
    public class ItemTransform
    {
        public bool isFinished;
        public ItemTransSub[] itemTransStep;
    }

    //�任�߼�ʹ�ñ���
    public ItemTransform[] itemTrans;
    public int Schedule;

    private Camera cam;
    private int ScheduleSub;
    private item myItem;
    private bool start;
    public enum scheduleSitu
    {
        initial,
        working,
        finish
    }

    private void Start()
    {
        myItem = GetComponent<item>();
        Schedule = 0;
        ScheduleSub = 0;
        cam = GameObject.Find("ItemManager").GetComponent<investigateMenuManager>().cam;
    }
    private void Update()
    {
        if (myItem.thisInvestigate && Schedule < itemTrans.Length)
        {
            if (ScheduleSub < itemTrans[Schedule].itemTransStep.Length)
            {
                if (PointerEvent()) 
                {
                    if(itemTrans[Schedule].itemTransStep[ScheduleSub].ScheduleSituation == scheduleSitu.initial)
                    {
                        InitializeSubTrans(itemTrans[Schedule].itemTransStep[ScheduleSub]);
                    }else if(itemTrans[Schedule].itemTransStep[ScheduleSub].ScheduleSituation == scheduleSitu.working)
                    {
                        PerformTransformation(itemTrans[Schedule].itemTransStep[ScheduleSub]);
                    }
                }
            }
            else
            {
                itemTrans[Schedule].isFinished = true;
                Schedule++;
                ScheduleSub = 0;
            }
        }
    }
    private void InitializeSubTrans(ItemTransSub iTS)
    {
        iTS.oPosition = iTS.go.GetComponent<Transform>().localPosition;
        iTS.oRotation = iTS.go.GetComponent<Transform>().localRotation.eulerAngles;
        iTS.ScheduleSituation = scheduleSitu.working;

    }

    //�ú���������δ���룬������ߣ�������ͼ��״̬�Ĺ���
    //���ص��� ���������������working״̬
    private bool PointerEvent()
    {
        bool isClick = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject == itemTrans[Schedule].itemTransStep[ScheduleSub].go
                && itemTrans[Schedule].itemTransStep[ScheduleSub].ScheduleSituation != scheduleSitu.finish)
            {
                itemTrans[Schedule].itemTransStep[ScheduleSub].go.GetComponent<Outline>().enabled = true;
            }
            else
            {
                itemTrans[Schedule].itemTransStep[ScheduleSub].go.GetComponent<Outline>().enabled = false;
            }

            if (hit.collider.gameObject == itemTrans[Schedule].itemTransStep[ScheduleSub].go
                && itemTrans[Schedule].itemTransStep[ScheduleSub].ScheduleSituation != scheduleSitu.finish
                && Input.GetMouseButton(0))
            {
                isClick = true;
            }
            
        }
        return isClick;
    }
    private void PerformTransformation(ItemTransSub iTS)
    {
        Transform _goTrans = iTS.go.GetComponent<Transform>();
        _goTrans.localPosition = Vector3.Lerp(_goTrans.localPosition, iTS.oPosition + iTS.Position, Time.deltaTime * iTS.PosSpeed);
        //_goTrans.localRotation = Quaternion.Euler(Vector3.Lerp(_goTrans.localRotation.eulerAngles, iTS.oRotation + iTS.Rotation, Time.deltaTime * iTS.RotSpeed));
        if (_goTrans.localPosition == iTS.oPosition + iTS.Position)  
        {
            ScheduleSub++;
        }
    }
}
