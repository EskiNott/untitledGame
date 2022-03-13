using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;
public class ParseTextJSON
{
    public List<Dialog> ParseDialogJSON(string jsonAddress,string language)
    {
        List<Dialog> Dialogs = new List<Dialog>();
        TextAsset ta = Resources.Load<TextAsset>(jsonAddress);
        JSONObject j = new JSONObject(ta.text);
        
        string tempName;
        string tempDialog;
        foreach (JSONObject temp in j.list)
        {
            int tempID = temp["id"].intValue;
            tempName = temp[language]["ObjectNameText"].stringValue;
            tempDialog = temp[language]["DialogText"].stringValue;
            Dialog d = new Dialog();
            d.id = tempID;
            d.ObjectNameText = tempName;
            d.DialogText = tempDialog;
            Dialogs.Add(d);
        }
        return Dialogs;
    }
}
