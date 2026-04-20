using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jsonで保存する場合Serializableが必要
/// InstanceDataの数値を記録するための変数をまとめたクラス
/// </summary>
[Serializable]
public class FriendSaveData
{
    public string friendId; //SO識別用
    public int hp;
    public int lv;
    public int exp;
}

/// <summary>
/// ラッパークラス
/// 今の出撃メンバー(編成)を保存するためのクラス
/// </summary>
[Serializable]
public class FriendSaveList
{
    public List<FriendSaveData> friends = new();
}

public static class FriendSaveManager
{
    //キーをキャラ名ごとにユニークにする
    private static string GetKey(string charName) => "FRIEND_SAVE_" + charName;

    //単体セーブ(今まではリスト単位だった)
    public static void Save(FriendInstanceData instance)
    {
        var data = new FriendSaveData
        {
            friendId = instance.Data.name,
            hp = instance.currentHP,
            lv = instance.currentLv,
            exp = instance.currentEXP
        };
        PlayerPrefs.SetString(GetKey(instance.Data.name), JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    //リストではなく単体でロードする　これでFriendSaveData.Load(this);という呼び方ができる
    public static void Load(FriendInstanceData instance)
    {
        string key = GetKey(instance.Data.name);
        if (!PlayerPrefs.HasKey(key)) return;

        var saved = JsonUtility.FromJson<FriendSaveData>(PlayerPrefs.GetString(key));
        instance.currentHP = saved.hp;
        instance.currentLv = saved.lv;
        instance.currentEXP = saved.exp;
    }

    public static void DeleteAllFriendData(List<FriendData> allFriends)
    {
        foreach (var data in allFriends)
        {
            // 各キャラ固有のキーを削除
            PlayerPrefs.DeleteKey("FRIEND_SAVE_" + data.name);
        }
    }
}

/*
public static class FriendSaveManager
{
    const string KEY = "FRIEND_SAVE";

    /// <summary>
    /// InstanceDataの情報をセーブする
    /// List単位でセーブ
    /// </summary>
    public static void Save(List<FriendInstanceData> list)
    {
        var saveList = new FriendSaveList();

        foreach (var f in list)
        {
            //セーブリストにSOデータ(元データ)からIDを保存、さらにレベルと経験値を保存
            saveList.friends.Add(new FriendSaveData
            {
                friendId = f.Data.name,
                hp = f.currentHP,
                lv = f.currentLv,
                exp = f.currentEXP
            });
        }

        //FriendSaveDataをJSON文字列に変換してPlayerPrefsに保存
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(saveList));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// InstanceDataの情報をセーブする
    /// 単体でセーブ
    /// </summary>
    public static void Save(FriendInstanceData instance)
    {

        //この書き方は一番下の説明を参考にしてください
        var data = new FriendSaveData
        {
            hp = instance.currentHP,
            lv = instance.currentLv,
            exp = instance.currentEXP
        };

        //FriendSaveDataをJSON文字列に変換してPlayerPrefsに保存
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// InstanceDataにセーブデータを代入(ロード)
    /// </summary>
    /// <param name="list"></param>
    public static void Load(List<FriendInstanceData> list)
    {
        //Keyがない、セーブデータがない場合はreturnする
        if (!PlayerPrefs.HasKey(KEY)) return;

        //PlayerPrefs.GetString(KEY):JSON文字列を取り出している
        var saveList = JsonUtility.FromJson<FriendSaveList>(PlayerPrefs.GetString(KEY));

        //保存しているフレンドを一人ずつ処理
        foreach (var saved in saveList.friends)
        {
            //現在存在しているフレンド一覧からIDが一致したフレンドを探している
            var inst = list.Find(f => f.Data.name == saved.friendId);
            if (inst == null) continue;

            //InstanceDataにセーブデータを代入(ロード)
            inst.currentHP = saved.hp;
            inst.currentLv = saved.lv;
            inst.currentEXP = saved.exp;

            inst.NotifyLvExpChanged();
        }
    }
}
*/

