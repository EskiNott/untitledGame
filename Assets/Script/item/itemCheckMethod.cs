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
        public Vector3 Position; //����任��λ��
        public Vector3 Rotation; //����ת���Ƕ�
        private bool isFinished; //��¼�Ƿ���ɱ任
        public bool Preview; //Ԥ������任��ת���Ľ��
    }
    [Serializable]
    public class ItemTransform
    {
        public ItemTransSub[] itemTransStep;
    }

    public ItemTransform[] itemTrans;

    //�任�߼�ʹ�ñ���
    public int Schedule;

    private Transform myTransform;
    private List<List<Transform>> goTrans;

    private void Start()
    {
        myTransform = transform;
        Schedule = 0;
    }
    private void Update()
    {
        performTransformation(itemTrans[0].itemTransStep[0]);
        if(Schedule != goTrans.Count)
        {

        }
    }

    private void performTransformation(ItemTransSub iTS)
    {
        Transform _goTrans = iTS.go.GetComponent<Transform>();
        _goTrans.position = iTS.Position;
        _goTrans.Rotate(Vector3.Lerp(_goTrans.rotation.eulerAngles,iTS.Rotation,Time.deltaTime), Space.Self);
    }
}
