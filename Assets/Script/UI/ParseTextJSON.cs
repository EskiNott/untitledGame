using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;
public class ParseTextJSON
{

    static public List<Dialog> ParseDialogJSON(string jsonAddress,string language)
    {
        List<Dialog> Dialogs = new List<Dialog>();
        TextAsset ta = Resources.Load<TextAsset>(jsonAddress);
        JSONObject j = new JSONObject(ta.text);
        
        string tempName;
        string tempDialog;
        bool tempExist;
        foreach (JSONObject temp in j.list)
        {
            int tempID = temp["id"].intValue;
            tempExist = temp["nameExist"].boolValue;
            tempName = temp[language]["ObjectNameText"].stringValue;
            tempDialog = temp[language]["DialogText"].stringValue;
            Dialog d = new Dialog();
            d.id = tempID;
            d.nameExist = tempExist;
            d.ObjectNameText = tempName;
            d.DialogText = tempDialog;
            Dialogs.Add(d);
        }
        return Dialogs;
    }

    static public List<Resource> ParseResourceListJSON(string jsonAddress)
    {
        List<Resource> BP = new List<Resource>();
        TextAsset ta = Resources.Load<TextAsset>(jsonAddress);
        JSONObject j = new JSONObject(ta.text);

        foreach (JSONObject temp in j.list)
        {
            int tempID = temp["id"].intValue;
            string tempPrefabName = temp["prefabName"].stringValue;
            string tempName = temp["Name"].stringValue;
            Resource.resourceType tempType;
            switch (temp["Type"].stringValue)
            {
                case "food":
                    tempType = Resource.resourceType.food;
                    break;
                case "drink":
                    tempType = Resource.resourceType.drink;
                    break;
                case "medicine":
                    tempType = Resource.resourceType.medicine;
                    break;
                case "equipment":
                    tempType = Resource.resourceType.equipment;
                    break;
                case "gun":
                    tempType = Resource.resourceType.gun;
                    break;
                case "meeleWeapon":
                    tempType = Resource.resourceType.meeleWeapon;
                    break;
                case "questItem":
                    tempType = Resource.resourceType.questItem;
                    break;
                case "tool":
                    tempType = Resource.resourceType.tool;
                    break;
                default:
                    tempType = Resource.resourceType.food;
                    break;
            }
            int tempAmount = temp["Amount"].intValue;
            float tempMass = temp["Mass"].floatValue;
            float tempVolume = temp["Volume"].floatValue;
            float tempVitamin = temp["Vitamin"].floatValue;
            float tempWater = temp["Water"].floatValue;
            float tempProtein = temp["Protein"].floatValue;
            float tempCarbohydrate = temp["Carbohydrate"].floatValue;
            float tempPest = temp["Pest"].floatValue;

            Resource r = new Resource();
            r.id = tempID;
            r.prefabName = tempPrefabName;
            r.Name = tempName;
            r.type = tempType;
            r.Amount = (uint)tempAmount;
            r.Mass = tempMass;
            r.Volume = tempVolume;
            r.Vitamin = tempVitamin;
            r.Water = tempWater;
            r.Protein = tempProtein;
            r.Carbohydrate = tempCarbohydrate;
            r.Pest = tempPest;
            BP.Add(r);
        }
        return BP;
    }
}
