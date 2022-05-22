using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SituationManager : MonoBehaviour
{
    public GameObject[] Actors;
    public GameObject[] SituationSlot;
    [SerializeField]
    private Transform pmTrans;
    [SerializeField]
    private List<Character> crt;
    // Start is called before the first frame update
    void Awake()
    {
        pmTrans = transform;
        foreach(GameObject tempGo in Actors)
        {
            crt.Add(tempGo.GetComponent<Character>());
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach(Character tempCha in crt)
        {
            tempCha.SituationManage();
            tempCha.NutritionManage();
            if (Input.GetKeyDown(KeyCode.Tab) && tempCha.ID == 1)
            {
                presentSituationOnWatch(tempCha);
            }
        }
    }

    public void presentSituationOnWatch(Character cha)
    {
        foreach(Character.Sickness temp in cha.Situations)
        {

        }
    }
}
