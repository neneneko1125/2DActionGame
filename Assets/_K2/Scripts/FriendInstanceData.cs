using UnityEngine;
using System;

public class FriendInstanceData
{
    public FriendData baseData; //元のデータ
    public int currentHP;
    public int currentLv;
    public int currentEXP;

    public event Action OnLvUp;
    public event Action OnExpChanged;

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
        currentEXP += exp;
        Debug.Log("FriendInstanceDataのcurrentEXPが" + currentEXP + "になった");
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
