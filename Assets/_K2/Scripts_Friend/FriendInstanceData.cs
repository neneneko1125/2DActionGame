using UnityEngine;
using System;

public class FriendInstanceData
{
    public FriendData baseData; //元のデータ
    public int currentHP;
    public int currentLv;
    public int currentEXP;

    public event Action OnChangeLvEXP;

    /// <summary>
    /// コンストラクタ
    /// MonoBehaviourだとコンストラクタは使えない
    /// </summary>
    /// <param name="data"></param>
    public FriendInstanceData(FriendData data)
    {
        baseData = data;
        currentHP = data.MaxHP;
        currentLv = 1;
        currentEXP = 0;
    }

    public void AddExp(int exp)
    {
        if (currentHP <= 0) return;

        currentEXP += exp;
        OnChangeLvEXP?.Invoke();

        while (currentEXP >= NeedExp())
        {
            currentEXP -= NeedExp();
            OnChangeLvEXP?.Invoke();
            LevelUp();
        }

    }

    private void LevelUp()
    {
        SEManager.Instance.SELvUp();
        currentLv++;
        currentHP = baseData.MaxHP + currentLv * baseData.PlusHP;
        OnChangeLvEXP?.Invoke();
    }

    public int NeedExp()
    {
        return Mathf.RoundToInt(baseData.Exp_n * Mathf.Pow(currentLv, baseData.Exp_m));
    }
}
