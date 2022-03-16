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
        public Vector3 Position; //物体变换的位置
        public Vector3 Rotation; //物体转动角度
        private bool isFinished; //记录是否完成变换
        public bool Preview; //预览物体变换和转动的结果
    }
    [Serializable]
    public class ItemTransform
    {
        public ItemTransSub[] itemTransStep;
    }

    public ItemTransform[] itemTrans;

    //变换逻辑使用变量
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
