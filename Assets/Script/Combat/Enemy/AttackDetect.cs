using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetect : MonoBehaviour
{
    public bool triggered;
    public Material materialBeingAttack;
    private Material originalMat;
    private GameObject myself;
    private GameObject player;

    public bool test = false;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        myself = GetComponent<Transform>().gameObject;
        originalMat = myself.GetComponent<Renderer>().material;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        test = true;
        if (player.GetComponent<AttackController>().isTriggered && !triggered)
        {
            triggered = true;
            beingAttack(myself);
            Invoke("_turnOffTrigger", player.GetComponent<AttackController>().AttackDelay);
        }
        else
        {
            triggered = false;
        }
    }

    private void _turnOffTrigger()
    {
        myself.gameObject.GetComponent<Renderer>().material = originalMat;
        triggered = false;
    }

    private void beingAttack(GameObject go)
    {

        myself.GetComponent<Renderer>().material = materialBeingAttack;
    }
}
