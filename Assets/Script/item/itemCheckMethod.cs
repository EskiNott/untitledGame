using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCheckMethod : MonoBehaviour
{
    //����任�ṹ�߼�����
    //�ñ���itemTrans�µĶ�����Ϊ��ͬ˳��任����֧����ͬ�㼶˳��Ķ���任
    //ͬһitemsub�����в����ͬʱ���У�itemtrans�´���ֲ�����
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
        [HideInInspector]
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
    private item myItem;

    private ItemTransSub _tempITS;
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
                    if(its.ScheduleSituation == scheduleSitu.finish)
                    {
                        continue;
                    }
                    else
                    {
                        if (PointerEvent(its))
                        {
                            if (its.ScheduleSituation == scheduleSitu.initial)
                            {
                                InitializeSubTrans(its);
                                its.ScheduleSituation = scheduleSitu.working;
                            }
                            else if (its.ScheduleSituation == scheduleSitu.working)
                            {
                                PerformTransformation(its);
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
        iTS.ScheduleSituation = scheduleSitu.working;

    }

    //�ú���������δ���룬������ߣ�������ͼ��״̬�Ĺ���
    //���ص��� ���������������working״̬
    private bool PointerEvent(ItemTransSub its)
    {
        bool isClick = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == its.go
                && its.ScheduleSituation != scheduleSitu.finish)
            {
                its.go.GetComponent<Outline>().enabled = true;
            }
            else
            {
                its.go.GetComponent<Outline>().enabled = false;
            }

            if (hit.collider.gameObject == its.go
                && its.ScheduleSituation != scheduleSitu.finish
                && Input.GetMouseButton(0))
            {
                _tempITS = its;
                isClick = true;
            }

        }
        return isClick;
    }

    //��ʼ�ı�transform
    private void PerformTransformation(ItemTransSub iTS)
    {
        Transform _goTrans = iTS.go.GetComponent<Transform>();
        _goTrans.localPosition = Vector3.Lerp(_goTrans.localPosition, iTS.oPosition + iTS.Position, Time.deltaTime * (iTS.PosSpeed + 2));
        //_goTrans.localRotation = Quaternion.Euler(Vector3.Lerp(_goTrans.localRotation.eulerAngles, iTS.oRotation + iTS.Rotation, Time.deltaTime * iTS.RotSpeed));
        if (isFinish(_goTrans.localPosition, iTS.oPosition + iTS.Position))
        {
            iTS.ScheduleSituation = scheduleSitu.finish;
            _goTrans.gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    //���ĳһ��С�ֲ����Ƿ����
    private bool isFinish(Vector3 now, Vector3 target)
    {
        bool finish = false;
        if (now.x / target.x > 0.995f && now.y / target.y > 0.995f && now.z / target.z > 0.995f) 
        {
            finish = true;
        }
        return finish;
    }

    //���ĳһ�ֲ��Ƿ����
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
