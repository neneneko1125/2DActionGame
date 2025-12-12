using UnityEngine;

public class PlayerInstanceData
{
    public PlayerData baseData;  //SO

    //ƒQ[ƒ€’†‚É•Ï‰»‚·‚é‚à‚Ì‚Í‚±‚±‚ÅŠÇ—
    public int currentHP;
    public int currentLv;
    public int currentEXP;


    public PlayerInstanceData(PlayerData data)
    {
        baseData = data;
        currentLv = 1;
        currentEXP = 0;
        currentHP = data.MaxHP;
    }

    public void AddExp(int exp)
    {
        currentEXP += exp;

        while (currentEXP >= NeedExp())
        {
            currentEXP -= NeedExp();
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLv++;
        currentHP = baseData.MaxHP + currentLv * baseData.PlusHP;
    }

    private int NeedExp()
    {
        return Mathf.RoundToInt(baseData.Exp_n * Mathf.Pow(currentLv, baseData.Exp_m));
    }
}
