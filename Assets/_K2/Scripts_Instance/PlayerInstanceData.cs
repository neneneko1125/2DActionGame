using UnityEngine;
using System;

/// <summary>
/// ゲーム中に変化するものはここで管理
/// </summary>
public class PlayerInstanceData : BaseInstanceData
{
    public PlayerData Data { get; set; }
    protected override BaseCharacterData BaseData => Data;

    /// <summary>
    /// コンストラクタ
    /// OrganizationManagerで呼び出される
    /// </summary>
    /// <param name="data"></param>
    public PlayerInstanceData(PlayerData data)
    {
        Data = data;
        currentHP = data.MaxHP;
        currentLv = 1;
        currentEXP = 0;
        criticalProbability = data.CriticalProbability;
        PlayerSaveManager.Load(this);
        Save();
    }

    //親クラスで使う
    protected override void Save() => PlayerSaveManager.Save(this);
}
