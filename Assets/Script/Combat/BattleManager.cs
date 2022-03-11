using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BattleManager : MonoBehaviour
{
    public Camera cam;
    private GameObject AttackRange;
    public Material beforeAttack;
    public Material afterAttack;
    public float Angle = 60.0f;
    public float Radius = 3.0f;
    private bool isTriggered;
    public float attackCooldown;
    private bool isCooldowned;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        isCooldowned = true;
        attackCooldown = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AttackRange = DrawAttackRange.DrawSectorSolid(cam, transform, transform.position, Angle, Radius);
        AttackRange.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
        AttackRange.tag = "AttackRange";
        if (!isTriggered)
        {
            AttackRange.GetComponent<Renderer>().material = beforeAttack;
            if (Input.GetMouseButtonDown(0) && isCooldowned)
            {
                isTriggered = true;
                isCooldowned = false;
                AttackRange.GetComponent<Renderer>().material = afterAttack;
                Invoke("_turnoffFlag", 10f);
                Invoke("_Cooldowned", attackCooldown);
            }
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
