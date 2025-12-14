using UnityEngine;
using System;

/// <summary>
/// ゲーム中に変化するものはここで管理
/// </summary>
public class PlayerInstanceData
{
    public PlayerData baseData;  //SO

    public int currentHP;   //現在HP
    public int currentLv;   //現在レベル
    public int currentEXP;  //現在経験値

    public event Action OnChangeLvEXP;     //レベルか経験値が変化したときのイベント

    /// <summary>
    /// コンストラクタ
    /// OrganizationManagerで呼び出される
    /// </summary>
    /// <param name="data"></param>
    public PlayerInstanceData(PlayerData data)
    {
        baseData = data;
        currentHP = data.MaxHP;
        currentLv = 1;
        currentEXP = 0;
    }

    /// <summary>
    /// 経験値を追加する
    /// GetEXPManagerから呼び出されている
    /// </summary>
    /// <param name="exp"></param>
    public void AddExp(int exp)
    {
        if (currentHP <= 0) return;
        
        currentEXP += exp;

        //InstanceDataにEXPが追加されたことを知らせる
        OnChangeLvEXP?.Invoke();

        //もし現在の経験値が必要経験値以上ならば
        while (currentEXP >= NeedExp())
        {
            //現在の経験値リセット
            currentEXP -= NeedExp();
            
            LevelUp();
        }
    }

    /// <summary>
    /// レベルアップするメソッド
    /// </summary>
    private void LevelUp()
    {
        SEManager.Instance.SELvUp();

        currentLv++;

        //現在のHPを現在のレベルに応じた最大HPに更新する
        currentHP = baseData.MaxHP + currentLv * baseData.PlusHP;

        //InstanceDataにレベルが上がったことを知らせる
        OnChangeLvEXP?.Invoke();
    }

    /// <summary>
    /// 現在のレベルに応じて必要経験値を返す
    /// </summary>
    /// <returns></returns>
    public int NeedExp()
    {
        //Pow:累乗を意味する　currentLvのbaseData.Exp_m乗
        return Mathf.RoundToInt(baseData.Exp_n * Mathf.Pow(currentLv, baseData.Exp_m));
    }
}
