using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SituationManager : MonoBehaviour
{
    [SerializeField]
    private Transform pm;
    [SerializeField]
    private Character crt;
    // Start is called before the first frame update
    void Start()
    {
        pm = transform;
    }

    // Update is called once per frame
    void Update()
    {
        crt.SituationCheck();
        crt.SerializeHashSet();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            presentSituationOnWatch(crt);
        }
    }

    public void presentSituationOnWatch(Character cha)
    {
        foreach(Character.Sickness temp in cha.Situations)
        {

        }
    }
}
