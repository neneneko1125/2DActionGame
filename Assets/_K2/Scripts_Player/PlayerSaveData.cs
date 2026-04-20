using System;
using UnityEngine;

/// <summary>
/// Jsonで保存する場合Serializableが必要
/// InstanceDataの数値を記録するための変数をまとめたクラス
/// </summary>
[Serializable]

public class PlayerSaveData 
{
    public int hp;
    public int lv;
    public int exp;
}


public static class PlayerSaveManager
{
    const string KEY = "PLAYER_SAVE";

    /// <summary>
    /// InstanceDataの情報をセーブする
    /// </summary>
    /// <param name="instance"></param>
    public static void Save(PlayerInstanceData instance)
    {
        //この書き方は一番下の説明を参考にしてください
        var data = new PlayerSaveData
        {
            hp = instance.currentHP,
            lv = instance.currentLv,
            exp = instance.currentEXP
        };

        //PlayerSaveDataをJSON文字列に変換してPlayerPrefsに保存
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }


    /// <summary>
    /// セーブデータをロードする
    /// </summary>
    /// <param name="instance"></param>
    public static void Load(PlayerInstanceData instance)
    {
        //Keyがない、セーブデータがない場合はreturnする
        if (!PlayerPrefs.HasKey(KEY)) return;

        //PlayerPrefs.GetString(KEY):JSON文字列を取り出している
        var data = JsonUtility.FromJson<PlayerSaveData>(PlayerPrefs.GetString(KEY));

        //InstanceDataにセーブデータを代入(ロード)
        instance.currentHP = data.hp;
        instance.currentLv = data.lv;
        instance.currentEXP = data.exp;
    }
}

/*
 * var data = new PlayerSaveData();
 * data.lv = instance.currentLv;
 * data.exp = instance.currentEXP;
 * と同じ意味
 */

