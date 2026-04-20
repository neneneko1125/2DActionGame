using UnityEngine;

[CreateAssetMenu(menuName = "Buff/BuffData")]
public class BuffData : ScriptableObject
{
    //ここの値を調整バフの内容を決める//
    [Header("足し算バフ(例：攻撃力+[10])")]
    public int attackBonus;
    [Header("掛け算バフ(例：攻撃力 + 攻撃力*[120]/100)")]
    public float attackRate = 100f; 

    [Header("ダメージ軽減(例：[80]→80％軽減)")]
    [Range(0, 100f)]
    public float damageReductionRate;     


    /// <summary>
    /// それぞれのInstanceDataで呼び出される
    /// ここで実際に攻撃力のバフを与える
    /// 掛け算してから足し算
    /// </summary>
    /// <param name="baseAttack"></param>
    /// <returns></returns>
    public int ModifyAttack(int baseAttack)
    {
        int value = Mathf.RoundToInt(baseAttack + baseAttack * attackRate / 100);
        value += attackBonus;
        return value;
    }


    /// <summary>
    /// それぞれのInstanceDataで呼び出される
    /// ここで実際にダメージ軽減のバフを与える
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int ModifyDamage(int damage)
    {
        //例えば20％軽減で100ダメージもらったら、100*(100-20)/100 = 80になる計算
        return Mathf.RoundToInt(damage * (100f - damageReductionRate) / 100);
    }
}
