using UnityEngine;

public enum CharacterType
{
    Attacker,
    Healer,
    Buffer
}

public abstract class BaseCharacterData : ScriptableObject
{
    [Header("名前")]
    public string Name;

    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;

    [Header("アイコン")]
    [SerializeField] private Sprite _icon;

    [Header("タイプ")]
    [SerializeField] private CharacterType _type;

    [Header("最大HP")]
    [SerializeField] private int _maxHP;

    [Header("会心率")]
    [SerializeField] private float _criticalProbability;

    [Header("レベルアップ時のHP上昇量")]
    [SerializeField] private int _plusHP = 5;

    [Header("経験値計算用 (n * Lv^m)")]
    [SerializeField] private float _exp_n = 5f;
    [SerializeField] private float _exp_m = 1.5f;

    //プロパティ(外部から読み取る用)

    public GameObject Prefab => _prefab;
    public Sprite Icon => _icon;
    public CharacterType Type => _type;
    public int MaxHP => _maxHP;
    public float CriticalProbability => _criticalProbability;
    public int PlusHP => _plusHP;
    public float Exp_n => _exp_n;
    public float Exp_m => _exp_m;
}