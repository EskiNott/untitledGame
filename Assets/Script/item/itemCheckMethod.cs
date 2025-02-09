using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCheckMethod : MonoBehaviour
{
    //物体变换结构逻辑定义
    //该变量itemTrans下的对象拆分为不同顺序变换，且支持相同层级顺序的对象变换
    //同一itemsub下所有步骤可同时进行，itemtrans下大步骤分步进行
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
        public scheduleSitu ScheduleSituation = scheduleSitu.initial; //记录物体运动状态
        [HideInInspector]
        public bool View; //预览物体变换和转动的结果 未实现
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
    private item myItem;

    public ItemTransSub _tempITS;
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
        cam = GameObject.Find("ItemManager").GetComponent<investigateMenuManager>().cam;
    }
    private void Update()
    {
        if (myItem.thisInvestigate && Schedule < itemTrans.Length)
        {
            if (!isSubFinish(itemTrans[Schedule].itemTransStep))
            {
                foreach(ItemTransSub its in itemTrans[Schedule].itemTransStep)
                {
                    if(its.ScheduleSituation != scheduleSitu.finish)
                    {
                        its.go.GetComponent<OutlinePointerEvent>().enabled = true;
                        if (PointerEvent(its))
                        {
                            if (its.ScheduleSituation == scheduleSitu.initial)
                            {
                                InitializeSubTrans(its);
                                its.ScheduleSituation = scheduleSitu.working;
                            }
                            else if (_tempITS.go != null && its.ScheduleSituation == scheduleSitu.working)
                            {
                                PerformTransformation(_tempITS);
                            }
                        }
                    }
                }
            }
            else
            {
                itemTrans[Schedule].isFinished = true;
                Schedule++;
            }
        }
    }
    private void InitializeSubTrans(ItemTransSub iTS)
    {
        iTS.oPosition = iTS.go.GetComponent<Transform>().localPosition;
        iTS.oRotation = iTS.go.GetComponent<Transform>().localRotation.eulerAngles;

    }

    //该函数功能尚未分离，包含描边，检测点击和检测状态的功能
    //返回的是 点击鼠标左键并处于working状态
    private bool PointerEvent(ItemTransSub its)
    {
        bool isClick = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == its.go
                && its.ScheduleSituation == scheduleSitu.finish)
            {
                its.go.GetComponent<OutlinePointerEvent>().enabled = false;
                its.go.GetComponent<Outline>().enabled = false;
            }

            if (hit.collider.gameObject == its.go
                && its.ScheduleSituation != scheduleSitu.finish
                && Input.GetMouseButtonDown(0))
            {
                _tempITS = its;
                isClick = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _tempITS = null;
            }
            else if (_tempITS != null && Input.GetMouseButton(0)) 
            {
                isClick = true;
            }

        }
        return isClick;
    }

    //开始改变transform
    private void PerformTransformation(ItemTransSub iTS)
    {
        Transform _goTrans = iTS.go.GetComponent<Transform>();
        _goTrans.localPosition = Vector3.Lerp(_goTrans.localPosition, iTS.oPosition + iTS.Position, Time.deltaTime * (iTS.PosSpeed + 2));
        _goTrans.localRotation = Quaternion.Lerp(_goTrans.localRotation, Quaternion.Euler(iTS.oRotation + iTS.Rotation), Time.deltaTime * iTS.RotSpeed);
        if (isFinishVector(_goTrans.localPosition, iTS.oPosition + iTS.Position) && isFinishQuaternion(_goTrans.localRotation, Quaternion.Euler(iTS.oRotation + iTS.Rotation)))
        {
            iTS.ScheduleSituation = scheduleSitu.finish;
            _goTrans.gameObject.GetComponent<Outline>().enabled = false;
            _goTrans.gameObject.GetComponent<OutlinePointerEvent>().enabled = false;
        }
    }

    //检测某一最小分步骤是否完成
    static public bool isFinishQuaternion(Quaternion now, Quaternion target)
    {
        bool finish = false;
        Vector3 tempV = new Vector3(1, 1, 1);
        if (Vector3.Angle(now * tempV, target * tempV) < 0.5f)
        {
            finish = true;
        }
        return finish;
    }
    static public bool isFinishVector(Vector3 now, Vector3 target)
    {
        bool finish = false;
        if ((Math.Abs(now.x - target.x) < 0.05f) && (Math.Abs(now.y - target.y) < 0.05f) && (Math.Abs(now.z - target.z) < 0.05f)) 
        {
            finish = true;
        }
        return finish;
    }

    //检测某一分步是否完成
    private bool isSubFinish(ItemTransSub[] sub)
    {
        bool isFinish = true;
        foreach(ItemTransSub its in sub)
        {
            if(its.ScheduleSituation != scheduleSitu.finish)
            {
                isFinish = false;
                break;
            }
        }
        return isFinish;
    }
}
