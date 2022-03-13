using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMainScript : MonoBehaviour
{
    public string jsonAddress = "DialogText\\example";
    public string language = "zh_CN";
    public bool test = false;

    private int id;
    private List<Dialog> DialogList;
    private Text dText;
    private Text nText;
    private Transform thisTrans;
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        ParseTextJSON pJson = new ParseTextJSON();
        DialogList = pJson.ParseDialogJSON(jsonAddress, language);
        thisTrans = transform;
        dText = GameObject.Find("DialogText").GetComponent<Text>();
        nText = GameObject.Find("ObjectNameText").GetComponent<Text>();
        thisTrans.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (id + 1 > DialogList.Count)
        {
            thisTrans.gameObject.SetActive(false);
        }
        else
        {
            dText.text = DialogList[id].DialogText;
            nText.text = DialogList[id].ObjectNameText;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            id++;
        }

    }
}
