using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
有关角色症状situation的描述:
01:正常
02:流血
03:骨折
04:饥饿
05:口渴
06:辐射病
07:坏血症
08:腹泻
09:夜盲症
10:凝血障碍
11:骨质疏松
12:反胃
13:发热
14:失血性休克
15:疲劳
*/

[System.Serializable]
public class Character : MonoBehaviour
{
    [Header("角色名")]
    public string CharacterName;

    [Header("角色ID")]
    public uint ID;

    [Header("体质")] // 所有状态最高数值
    public float Constitution = 1000;
    [Header("胃容量")]
    public float StomachVolume = 500;
    [Header("维生素")]
    public float Vitamin = 500;
    [Header("水")]
    public float Water = 500;
    [Header("蛋白质")]
    public float Protein = 500;
    [Header("糖类")]
    public float Carbohydrate = 500;
    [Header("血液量")]
    public float Blood = 1000;
    [Header("有害生物量")]
    public float Pest = 0;
    [Header("细胞健康度")]
    public float CellHealth = 1000;
    [Header("更新速度")]
    public float UpdateSpeed = 5.0f;

    public List<Sickness> Situations;
    public float[] sickTable = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public enum Sickness
    {
        Normal,
        Bleed,
        Fracture,
        Hunger,
        Thirst,
        RadiationSickness,
        Scurvy, //坏血症
        Diarrhoea, //腹泻
        NightBlindness,
        CoagulationDisorder, //凝血障碍
        Osteoporosis, //骨质疏松
        Nausea, //反胃
        Fever,
        Shock, //休克
        Fatigue, //疲劳
        Death
    }

    //Nutrition 管理
    public void NutritionManage()
    {
        if (!IsStateExist(Sickness.Death))
        {
            ConstitutionManage();
            StomachVolumeManage();
            VitaminManage();
            WaterManage();
            ProteinManage();
            CarbohydrateManage();
            BloodManage();
            PestManage();
            CellHealthManage();
        }
    }
    //不包含状态对齐影响
    //也就是正常状态下数值随时间发生的变化
    private void ConstitutionManage()
    {
        _NutritionMaxCheck(ref Constitution);
        if (Pest > Constitution * 0.99
            || CellHealth < Constitution * 0.01
            || StomachVolume < Constitution * 0.01
            || Vitamin < Constitution * 0.01
            || Water < Constitution * 0.01
            || Protein < Constitution * 0.01
            || Carbohydrate < Constitution * 0.01
            || Blood < Constitution * 0.01
            ) 
        {
            ChangePerSecond(ref Constitution, -50f);
        }
        else if(Pest > Constitution*0.4 || CellHealth < Constitution* 0.3)
        {
            ChangePerSecond(ref Constitution, -5f);
        }
        else 
        {
            ChangePerSecond(ref Constitution, 0.05f);
        }

    }

    private void StomachVolumeManage()
    {
        _NutritionMaxCheck(ref StomachVolume);
        if (StomachVolume > Constitution * 0.35)
        {
            ChangePerSecond(ref StomachVolume, -0.5f);
        }
        else
        {
            ChangePerSecond(ref StomachVolume, -0.25f);
        }

    }
    private void VitaminManage()
    {
        _NutritionMaxCheck(ref Vitamin);
        if (StomachVolume > Constitution * 0.35)
        {
            ChangePerSecond(ref Vitamin, -0.5f);
        }
        else
        {
            ChangePerSecond(ref Vitamin, -0.25f);
        }
    }

    private void WaterManage()
    {
        _NutritionMaxCheck(ref Water);
        if(Water > Constitution * 0.95)
        {
            ChangePerSecond(ref Water, -10f);
        }else if (Water < Constitution * 0.1)
        {
            ChangePerSecond(ref Water, -0.25f);
        }
        else
        {
            ChangePerSecond(ref Water, -0.5f);
        }
    }

    private void ProteinManage()
    {
        _NutritionMaxCheck(ref Protein);
        ChangePerSecond(ref Protein, -0.2f);
    }

    private void CarbohydrateManage()
    {
        _NutritionMaxCheck(ref Carbohydrate);
        if (Carbohydrate > Constitution * 0.9)
        {
            ChangePerSecond(ref Carbohydrate, -10f);
        }
        else if (Carbohydrate < Constitution * 0.1)
        {
            ChangePerSecond(ref Carbohydrate, -0.25f);
        }
        else
        {
            ChangePerSecond(ref Carbohydrate, -0.2f);
        }
    }

    private void BloodManage()
    {
        _NutritionMaxCheck(ref Blood);
        if (Carbohydrate > Constitution*0.1 
            && Protein > Constitution*0.1)
        {
            ChangePerSecond(ref Blood, 1f);
        }
    }

    private void PestManage()
    {
        _NutritionMaxCheck(ref Pest);
        if (Carbohydrate > Constitution * 0.3
            && Protein > Constitution * 0.3)
        {
            ChangePerSecond(ref Pest, -1f);
        }
    }

    private void CellHealthManage()
    {
        _NutritionMaxCheck(ref CellHealth);
        ChangePerSecond(ref CellHealth, 0.5f);
        if (Blood < Constitution * 0.4) 
        {
            ChangePerSecond(ref CellHealth, -2f);
        }
        if (Water < Constitution * 0.1
            || Carbohydrate < Constitution * 0.1
            || Vitamin < Constitution * 0.1) 
        {
            ChangePerSecond(ref CellHealth, -2f);
        }
    }

    //状态管理
    public void SituationManage()
    {
        if (!IsStateExist(Sickness.Death))
        {
            //状态添加
            if (Constitution <= 0)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Death], +25f); ; //死亡唯一入口
            }

            if (StomachVolume > Constitution * 0.9
                || Vitamin > Constitution * 0.95
                || Water > Constitution * 0.9
                || Carbohydrate < Constitution * 0.1)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Nausea], +25f);
            }

            if (StomachVolume < Constitution * 0.15)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Hunger], +25f);
            }

            if (Vitamin > Constitution * 0.95)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Diarrhoea], +25f);
            }

            if (Vitamin > Constitution * 0.95)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.CoagulationDisorder], +25f);
            }

            if (Vitamin < Constitution * 0.15)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Scurvy], +25f);
            }

            if (Vitamin < Constitution * 0.15)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.NightBlindness], +25f);
            }

            if (Vitamin < Constitution * 0.15)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Fatigue], +25f);
            }

            if (Water < Constitution * 0.1
                || Blood < Constitution * 0.3)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Shock], +25f);
            }

            if (Pest > Constitution * 0.3)
            {
                ChangePerSecond(ref sickTable[(int)Sickness.Fever], +25f);
            }


            //状态效果
            if (IsStateExist(Sickness.Bleed))
            {
                ChangePerSecond(ref Protein, -3f);
                ChangePerSecond(ref Blood, -5f);
            }
            if (IsStateExist(Sickness.Fracture))
            {
                ChangePerSecond(ref Protein, -3f);
            }
            if (IsStateExist(Sickness.Hunger))
            {
                ChangePerSecond(ref CellHealth, -1f);
            }
            if (IsStateExist(Sickness.Thirst))
            {
                ChangePerSecond(ref CellHealth, -1f);
            }
            if (IsStateExist(Sickness.RadiationSickness))
            {
                ChangePerSecond(ref Protein, -9f);
                ChangePerSecond(ref Blood, -3f);
                ChangePerSecond(ref Pest, -10f);
                ChangePerSecond(ref CellHealth, -10f);
                ChangePerSecond(ref sickTable[(int)Sickness.CoagulationDisorder], +25f);
                ChangePerSecond(ref sickTable[(int)Sickness.Osteoporosis], +25f);
            }
            if (IsStateExist(Sickness.Scurvy))
            {
                ChangePerSecond(ref Blood, -3f);
            }
            if (IsStateExist(Sickness.Diarrhoea))
            {
                ChangePerSecond(ref Water, -2f);
                ChangePerSecond(ref Protein, -2f);
                ChangePerSecond(ref Vitamin, -2f);
                ChangePerSecond(ref StomachVolume, -5f);
            }
            if (IsStateExist(Sickness.NightBlindness))
            {

            }
            if (IsStateExist(Sickness.CoagulationDisorder))
            {
                ChangePerSecond(ref Protein, -2f);
                ChangePerSecond(ref Carbohydrate, -2f);
            }
            if (IsStateExist(Sickness.Osteoporosis))
            {

            }
            if (IsStateExist(Sickness.Nausea))
            {

            }
            if (IsStateExist(Sickness.Fever))
            {
                ChangePerSecond(ref Water, -5f);
                ChangePerSecond(ref Protein, -3f);
                ChangePerSecond(ref Carbohydrate, -5f);
                ChangePerSecond(ref Pest, -10f);
                ChangePerSecond(ref sickTable[(int)Sickness.Nausea], +25f);
            }
            if (IsStateExist(Sickness.Shock))
            {

            }
            if (IsStateExist(Sickness.Fatigue))
            {
                ChangePerSecond(ref CellHealth, -1f);
            }
        }
    }

    public void SituationTableManage()
    {
        for(int i = 0; i < sickTable.Length; i++)
        {
            if (sickTable[i] >= 100)
            {
                SituationAdd((Sickness)i);
            }
            else
            {
                SituationRemove((Sickness)i);
            }

            if(sickTable[i] > 110)
            {
                sickTable[i] = 110;
            }

            if (sickTable[i] > 0)
            {
                ChangePerSecond(ref sickTable[i], -5f);
            }
            else
            {
                sickTable[i] = 0;
            }
        }
    }

    // 基本函数
    public bool IsStateExist(Sickness s)
    {
        foreach (Sickness tempS in Situations)
        {
            if (tempS == s)
            {
                return true;
            }
        }
        return false;
    }

    public void SituationAdd(Sickness situ)
    {
        if (!IsStateExist(situ))
        {
            Situations.Add(situ);
        }
    }

    public void SituationRemove(Sickness situ)
    {
        if (IsStateExist(situ))
        {
            Situations.Remove(situ);
        }
    }

    private void ChangePerSecond(ref float Nutrition, float delta)
    {
        Nutrition = Nutrition + delta * Time.deltaTime * 0.01f * UpdateSpeed;
    }
    private void _NutritionMaxCheck(ref float Nutrition)
    {
        if (Nutrition > Constitution)
        {
            Nutrition = Constitution;
        }else if(Nutrition < 0)
        {
            Nutrition = 0;
        }
    }
}
