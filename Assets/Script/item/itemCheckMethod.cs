using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCheckMethod : MonoBehaviour
{
    //物体变换结构逻辑定义
    //该变量itemTrans下的对象拆分为不同顺序变换，且支持相同层级顺序的对象变换
    //ItemTransSub类内的gameobject均为某组合对象的部件
    [Serializable]
    public class ItemTransSub
    {
        public GameObject go; //物体对象
        public Vector3 Position; //物体变换的相对位置
        public Vector3 Rotation; //物体相对旋转
        [HideInInspector]
        public Vector3 oPosition; //变换前位置
        [HideInInspector]
        public Vector3 oRotation; //变换前旋转
        public float RotSpeed = 1.0f;
        public float PosSpeed = 1.0f;
        [HideInInspector]
        public bool isFinished;
        public bool View; //预览物体变换和转动的结果
    }

    [Serializable]
    public class ItemTransform
    {
        public bool isFinished;
        public ItemTransSub[] itemTransStep;
    }

    //变换逻辑使用变量
    public ItemTransform[] itemTrans;
    public int Schedule;

    private Camera cam;
    private int ScheduleSub;
    private item myItem;
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
                if (PointerEvent()) {
                    PerformTransformation(itemTrans[Schedule].itemTransStep[ScheduleSub]);
                    ScheduleSub++;
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
    private void InitializeSubTrans()
    {
        foreach(ItemTransform it in itemTrans)
        {
            foreach(ItemTransSub its in it.itemTransStep)
            {
                its.oPosition = its.go.GetComponent<Transform>().position;
                its.oRotation = its.go.GetComponent<Transform>().rotation.eulerAngles;
                its.isFinished = false;
            }
        }
    }
    private bool PointerEvent()
    {
        bool isClick = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject == itemTrans[Schedule].itemTransStep[ScheduleSub].go
                && !itemTrans[Schedule].itemTransStep[ScheduleSub].isFinished)
            {
                itemTrans[Schedule].itemTransStep[ScheduleSub].go.GetComponent<Outline>().enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    isClick = true;
                    itemTrans[Schedule].itemTransStep[ScheduleSub].go.GetComponent<Outline>().enabled = false;
                }
            }
        }
        return isClick;
    }
    private void PerformTransformation(ItemTransSub iTS)
    {
        if (!iTS.isFinished)
        {
            Transform _goTrans = iTS.go.GetComponent<Transform>();
            _goTrans.position = Vector3.Lerp(_goTrans.position, iTS.oPosition + iTS.Position * 0.1f, Time.deltaTime * iTS.PosSpeed);
            _goTrans.rotation = Quaternion.Euler(Vector3.Lerp(_goTrans.rotation.eulerAngles, iTS.oRotation + iTS.Rotation, Time.deltaTime * iTS.RotSpeed));
            iTS.isFinished = true;
        }
    }
}
