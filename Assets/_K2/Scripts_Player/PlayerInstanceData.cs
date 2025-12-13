using UnityEngine;
using System;

public class PlayerInstanceData
{
    public PlayerData baseData;  //SO

    //ƒQ[ƒ€’†‚É•Ï‰»‚·‚é‚à‚Ì‚Í‚±‚±‚ÅŠÇ—
    public int currentHP;
    public int currentLv;
    public int currentEXP;

    public event Action OnLvUp;
    public event Action OnExpChanged;

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
        Debug.Log("PlayerInstanceData‚ÌcurrentEXP‚ª" + currentEXP + "‚É‚È‚Á‚½");
        OnExpChanged?.Invoke();

        while (currentEXP >= NeedExp())
        {
            currentEXP -= NeedExp();
            OnExpChanged?.Invoke();
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLv++;
        currentHP = baseData.MaxHP + currentLv * baseData.PlusHP;
        OnLvUp?.Invoke();
    }

    public int NeedExp()
    {
        return Mathf.RoundToInt(baseData.Exp_n * Mathf.Pow(currentLv, baseData.Exp_m));
    }
}
