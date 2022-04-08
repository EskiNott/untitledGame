using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
�йؽ�ɫ֢״situation������:
01:����
02:��Ѫ
03:����
04:����
05:�ڿ�
06:���䲡
07:��Ѫ֢
08:��к
09:ҹä֢
10:��Ѫ�ϰ�
11:��������
12:��θ
13:����
14:ʧѪ���ݿ�
15:ƣ��
*/

[System.Serializable]
public class Character : MonoBehaviour
{
    [Header("��ɫ��")]
    public string CharacterName;

    [Header("��ɫID")]
    public uint ID;

    [Header("����")] // ����״̬�����ֵ
    public float Constitution = 1000;
    [Header("θ����")]
    public float StomachVolume = 500;
    [Header("ά����")]
    public float Vitamin = 500;
    [Header("ˮ")]
    public float Water = 500;
    [Header("������")]
    public float Protein = 500;
    [Header("����")]
    public float Carbohydrate = 500;
    [Header("֬��")]
    public float Fat = 500;
    [Header("ѪҺ��")]
    public float Blood = 1000;
    [Header("�к�������")]
    public float Pest = 0;
    [Header("ϸ��������")]
    public float CellHealth = 1000;

    public List<Sickness> Situations;

    public enum Sickness
    {
        Normal,
        Bleed,
        Fracture,
        Hunger,
        Thirst,
        RadiationSickness,
        Scurvy, //��Ѫ֢
        Diarrhoea, //��к
        NightBlindness,
        CoagulationDisorder, //��Ѫ�ϰ�
        Osteoporosis, //��������
        Nausea, //��θ
        Fever,
        Shock, //�ݿ�
        Fatigue, //ƣ��
        Death
    }

    //Nutrition ����
    public void NutritionManage()
    {
        ConstitutionManage();
        StomachVolumeManage();
        VitaminManage();
        WaterManage();
        ProteinManage();
        CarbohydrateManage();
        FatManage();
        BloodManage();
        PestManage();
        CellHealthManage();
    }
    //������״̬����Ӱ��
    //Ҳ��������״̬����ֵ��ʱ�䷢���ı仯
    private void ConstitutionManage()
    {
        if(Pest > Constitution*0.4 || CellHealth < Constitution* 0.3)
        {
            ChangePerSecond(ref Constitution, -2f);
        }
        if(Fat > Constitution * 0.9)
        {
            ChangePerSecond(ref Constitution, -0.5f);
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
        if (Fat < Constitution * 0.1)
        {
            ChangePerSecond(ref Protein, -2f);
        }
        else
        {
            ChangePerSecond(ref Protein, -0.2f);
        }
    }

    private void CarbohydrateManage()
    {
        _NutritionMaxCheck(ref Carbohydrate);
        if (Carbohydrate > Constitution * 0.9)
        {
            ChangePerSecond(ref Carbohydrate, -10f);
        }
        else if (Carbohydrate < Constitution*0.1 && Fat > Constitution * 0.3)
        {
            ChangePerSecond(ref Carbohydrate, 8f);
        }else if (Carbohydrate < Constitution * 0.1)
        {
            ChangePerSecond(ref Carbohydrate, -0.25f);
        }
        else
        {
            ChangePerSecond(ref Carbohydrate, -0.2f);
        }
    }

    private void FatManage()
    {
        _NutritionMaxCheck(ref Fat);
        if (Carbohydrate > Constitution * 0.9)
        {
            ChangePerSecond(ref Fat, 2f);
        }else if ( Carbohydrate < Constitution * 0.1)
        {
            ChangePerSecond(ref Fat, -2f);
        }
        else
        {
            ChangePerSecond(ref Fat, -0.2f);
        }
    }

    private void BloodManage()
    {
        _NutritionMaxCheck(ref Blood);
        if (Carbohydrate > Constitution*0.1 
            && Protein > Constitution*0.1 
            && Fat > Constitution * 0.1)
        {
            ChangePerSecond(ref Blood, 1f);
        }
    }

    private void PestManage()
    {
        _NutritionMaxCheck(ref Pest);
        if (Carbohydrate > Constitution * 0.3
            && Protein > Constitution * 0.3
            && Fat > Constitution * 0.3)
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
            || Fat < Constitution * 0.1
            || Vitamin < Constitution * 0.1) 
        {
            ChangePerSecond(ref CellHealth, -2f);
        }
    }

    //״̬����
    public void SituationManage()
    {
        //״̬���
        if (Constitution <= 0)
        {
            SituationAdd(Sickness.Death); //����Ψһ���
        }

        if (StomachVolume > Constitution * 0.9
            || Vitamin > Constitution * 0.95
            || Water > Constitution * 0.9
            || Carbohydrate < Constitution * 0.1
            || Fat < Constitution * 0.1) 
        {
            SituationAdd(Sickness.Nausea);
        }

        if (StomachVolume < Constitution * 0.15)
        {
            SituationAdd(Sickness.Hunger);
        }

        if (Vitamin > Constitution * 0.95) 
        {
            SituationAdd(Sickness.Diarrhoea);
        }

        if (Vitamin > Constitution * 0.95)
        {
            SituationAdd(Sickness.CoagulationDisorder);
        }

        if(Vitamin < Constitution * 0.15)
        {
            SituationAdd(Sickness.Scurvy);
        }

        if(Vitamin < Constitution * 0.15)
        {
            SituationAdd(Sickness.NightBlindness);
        }

        if (Vitamin < Constitution * 0.15)
        {
            SituationAdd(Sickness.Fatigue);
        }

        if (Water < Constitution * 0.1
            || Blood < Constitution * 0.3) 
        {
            SituationAdd(Sickness.Shock);
        }

        if(Pest > Constitution * 0.2)
        {
            SituationAdd(Sickness.Fever);
        }
        //״̬�Ƴ�


        //״̬Ч��
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
            SituationAdd(Sickness.CoagulationDisorder);
            SituationAdd(Sickness.Osteoporosis);
        }
        if (IsStateExist(Sickness.Scurvy))
        {
            ChangePerSecond(ref Blood, -3f);
        }
        if (IsStateExist(Sickness.Diarrhoea))
        {
            ChangePerSecond(ref Water, -10f);
            ChangePerSecond(ref Protein, -3f);
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
            ChangePerSecond(ref Fat, -5f);
            ChangePerSecond(ref Pest, -3f);
            SituationAdd(Sickness.Nausea);
        }
        if (IsStateExist(Sickness.Shock))
        {

        }
        if (IsStateExist(Sickness.Fatigue))
        {
            ChangePerSecond(ref CellHealth, -1f);
        }
        if (IsStateExist(Sickness.Death))
        {

        }
    }

    // ��������
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
        Nutrition = Nutrition + delta * Time.deltaTime;
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
