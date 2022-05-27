using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SituationManager : MonoBehaviour
{
    public List<Character> Actors;
    public GameObject[] SituationSlot;

    [SerializeField]
    private Image WatchSituationColor;
    [SerializeField]
    private Image SituationColorUI;
    [SerializeField]
    private Transform playerfist;
    [SerializeField]
    private Transform[] fistPosition;

    private float _FistMovingSpeed = 10;
    private int _FistMovingTarget;

    // Start is called before the first frame update
    void Awake()
    {

    }
    private void Start()
    {
        _FistMovingTarget = 0;
    }
    // Update is called once per frame
    void Update()
    {
        WatchSituationColor.color = GetSituationColor();
        SituationColorUI.color = GetSituationColor();
        fistMoving(fistPosition[_FistMovingTarget], _FistMovingSpeed);
        Watch_Manage();
        foreach (Character tempCha in Actors)
        {
            tempCha.SituationManage();
            tempCha.NutritionManage();
            tempCha.SituationTableManage();
        }
    }
    private void Watch_Manage()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _FistMovingTarget = (_FistMovingTarget == 0) ? 1 : 0;
        }

        if(_FistMovingTarget == 1)
        {
            Watch_PresentSituation();
        }
        else
        {
            Watch_HideSituation();
        }
    }
    private void Watch_HideSituation()
    {
        foreach (GameObject go in SituationSlot)
        {
            go.SetActive(false);
        }
    }
    private void Watch_PresentSituation()
    {
        Watch_HideSituation();
        foreach(Character.Sickness s in Actors[0].Situations)
        {
            SituationSlot[(int)s].SetActive(true);
        }
    }

    private void fistMoving(Transform target, float movingSpeed)
    {
        playerfist.position = Vector3.Lerp(playerfist.position, target.position, Time.deltaTime * movingSpeed);
        playerfist.rotation = Quaternion.Lerp(playerfist.rotation, target.rotation, Time.deltaTime * movingSpeed);
    }

    private Color GetSituationColor()
    {
        float redScale = Color.green.r - Color.black.r;
        float greenScale = Color.green.g - Color.black.g;
        float blueScale = Color.green.b - Color.black.b;
        float rate = Actors[0].Constitution / 1500;
        return new Color(Color.black.r + redScale * rate, Color.black.g + greenScale * rate, Color.black.b + blueScale * rate, 1);
    }
}
