using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMainScript : MonoBehaviour
{
    public string jsonAddress = "DialogText\\example";
    public string language = "zh_CN";

    private float id;
    private int index;
    private List<Dialog> DialogList;
    private Text dText;
    private Text nText;
    private GameObject namePanel;
    private Transform thisTrans;
    private ParseTextJSON pJson;
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        index = 0;
        thisTrans = transform;
        dText = GameObject.Find("DialogText").GetComponent<Text>();
        nText = GameObject.Find("ObjectNameText").GetComponent<Text>();
        namePanel = GameObject.Find("ObjectPanel");
        DialogList = ParseTextJSON.ParseDialogJSON(jsonAddress, language);
    }

    // Update is called once per frame
    void Update()
    {
        index = (int)id;
        if(DialogList != null)
        {
            if (id + 1 > DialogList.Count)
            {
                thisTrans.gameObject.SetActive(false);
            }
            else
            {
                namePanel.SetActive(DialogList[index].nameExist);
                dText.text = DialogList[index].DialogText;
                nText.text = DialogList[index].ObjectNameText;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                id++;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                id = id + 1 * Time.deltaTime * 50;
            }
        }
    }
}
