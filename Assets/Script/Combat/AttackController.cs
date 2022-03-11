using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackController : MonoBehaviour
{
    public Camera cam;
    public Material beforeAttack;
    public Material afterAttack;
    private float Angle = 180.0f;
    public float thick = 3.0f;
    public float innerRadius = 0.0f;
    public float AttackDelay = 0.15f;
    public int segmentsCount = 2;

    private GameObject AttackRange;
    public bool isTriggered;
    public float attackCooldown;
    private bool isCooldowned;

    public Vector3 getMouseVectorRelativeToObject(Camera c,Transform t)
    {
        Vector3 screenPos = c.WorldToScreenPoint(transform.position);
        Vector3 mousePosOnScreen = Input.mousePosition;
        mousePosOnScreen.z = screenPos.z;
        Vector3 mousePosInWorld = c.ScreenToWorldPoint(mousePosOnScreen);
        Vector3 mouseVector = new Vector3(mousePosInWorld.x - t.position.x, 0, mousePosInWorld.z - t.position.z).normalized;
        //Debug.DrawLine(t.position, mousePosInWorld, Color.cyan);
        return mouseVector;
    }
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        isCooldowned = true;
        AttackRange = _createAttackObject();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = getMouseVectorRelativeToObject(cam, transform);
        AttackRange.transform.position = transform.position;
        AttackRange.transform.forward = target;
        if (!isTriggered)
        {
            AttackRange.GetComponent<Renderer>().material = beforeAttack;
            if (Input.GetMouseButtonDown(0) && isCooldowned)
            {
                isTriggered = true;
                isCooldowned = false;
                Invoke("_turnoffFlag", AttackDelay);
                Invoke("_Cooldowned", attackCooldown);
            }
        }
        else
        {
            AttackRange.GetComponent<Renderer>().material = afterAttack;
        }
    }
    private void _turnoffFlag()
    {
        isTriggered = false;
    }

    private void _Cooldowned()
    {
        isCooldowned = true;
    }
    private GameObject _createAttackObject()
    {
        GameObject go;
        go = DrawAttackRange.initiateSector(transform.localScale.x + thick + innerRadius, transform.localScale.x + innerRadius, Angle, segmentsCount);
        go.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
        go.tag = "AttackRange";
        go.AddComponent<MeshCollider>();
        go.GetComponent<MeshCollider>().convex = true;
        go.GetComponent<MeshCollider>().isTrigger = true;
        go.GetComponent<MeshCollider>().sharedMesh = go.GetComponent<MeshFilter>().mesh;
        return go;
    }
}
