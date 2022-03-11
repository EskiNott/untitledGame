using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackController : MonoBehaviour
{
    public Camera cam;
    public Material beforeAttack;
    public Material afterAttack;
    public float Angle = 60.0f;
    public float Radius = 3.0f;

    private GameObject AttackRange;
    private bool isTriggered;
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
        AttackRange = DrawAttackRange.initiateSector(Radius, transform.localScale.x, Angle, 2);
        AttackRange.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
        AttackRange.tag = "AttackRange";
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
                Invoke("_turnoffFlag", 0.15f);
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
}
