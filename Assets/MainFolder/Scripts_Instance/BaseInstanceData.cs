using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInstanceData
{
    // 現在のデータ
    public int currentHP;
    public int currentLv;
    public int currentEXP;
    public float criticalProbability;


    //紐づいている実際のゲームオブジェクト
    //MonoBehaviourを継承していないから座標情報がない
    //ヒーラーが座標を知ることができるため、この変数が必要
    //また、敵側がプレイヤーと味方キャラの中でどのキャラが近いか調べるのにも使う
    public Transform CharacterTransform { get; set; }

    //FriendHPなどで使用 (UIの更新はSaveManagerじゃなくてCharDataUIManagerの役割)
    //ここでOnChangeLvEXPを発動→HP関連のクラスがそれを知る→HP関連のクラスがCharDataUIManagerのUI更新メソッドを呼び出す
    public event Action OnChangeLvEXP;

    //FriendAttackObject、PlayerAttackObjectで攻撃力の再調整のためのイベント
    public event Action OnChangeAttack;

    //子クラスによって参照するScriptableObjectが違うので、抽象プロパティにする
    protected abstract BaseCharacterData BaseData { get; }

    private readonly HashSet<BuffData> _activeBuffs = new();

    public int MaxHP => BaseData.MaxHP + (currentLv - 1) * BaseData.PlusHP;
    public int NeedExp => Mathf.RoundToInt(BaseData.Exp_n * Mathf.Pow(currentLv, BaseData.Exp_m));
    public float HPRatio => (float)currentHP / MaxHP; // HP割合(0～1)


    public void AddExp(int exp)
    {
        if (currentHP <= 0) return;
        currentEXP += exp;

        while (currentEXP >= NeedExp)
        {
            currentEXP -= NeedExp;
            LevelUp();
        }
        Save();
        OnChangeLvEXP?.Invoke();
    }

    protected virtual void LevelUp()
    {
        currentLv++;
        currentHP = MaxHP; // レベルアップで全回復
        OnChangeAttack?.Invoke();
        OnChangeLvEXP?.Invoke();
        SEManager.Instance.SELvUp();
        Save();
    }

    /// <summary>
    /// このキャラにバフを与える
    /// </summary>
    /// <param name="buff"></param>
    public void AddBuff(BuffData buff)
    {
        if (_activeBuffs.Add(buff))
        {
            OnChangeAttack?.Invoke();
        }
    }

    /// <summary>
    /// バフを解除する
    /// </summary>
    /// <param name="buff"></param>
    public void RemoveBuff(BuffData buff)
    {
        if (_activeBuffs.Remove(buff))
        {
            OnChangeAttack?.Invoke();
        }
    }

    /// <summary>
    /// 攻撃用オブジェクトから呼び出される
    /// バフの分はここで計算する
    /// </summary>
    /// <param name="baseAttack"></param>
    /// <returns></returns>
    public int GetBuffAttack(int baseAttack)
    {
        int newAttack = baseAttack;

        foreach (var buff in _activeBuffs)
        {
            //ここで実際にバフを受けに行く
            newAttack = buff.ModifyAttack(newAttack);
        }

        return newAttack;
    }

    /// <summary>
    /// HPスクリプトから呼び出される
    /// バフの分はここで計算する
    /// </summary>
    /// <param name="baseDamage"></param>
    /// <returns></returns>
    public int GetBuffDamegeCut(int baseDamage)
    {
        int newDamage = baseDamage;

        foreach(var buff in _activeBuffs)
        {
            newDamage = buff.ModifyDamage(newDamage);
        }

        return newDamage;
    }


    //セーブ処理はPlayerとFriendでクラスが違うので抽象化
    //子クラスがSaveをもってきてくれる
    protected abstract void Save();

 
    
}