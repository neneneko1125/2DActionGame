using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Friend Data")]
public class FriendData : ScriptableObject
{
    [SerializeField] private string _friendName;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _lv;
    [SerializeField] private int _exp;
    [SerializeField] private GameObject _actionPrefab;
    [SerializeField] private Sprite _icon;

    [Header("Lvのm乗*n")]
    [SerializeField] private float _exp_n = 5f;
    [SerializeField] private float _exp_m = 1.5f;

    [Header("レベルアップしたときのHP上昇量")]
    [SerializeField] private int _plusHP = 5;

    //プロパティ 読み取り専用
    public string FriendName => _friendName;
    public int MaxHP => _maxHP;
    public int Level => _lv;
    public int Exp => _exp;
    public GameObject ActionPrefab => _actionPrefab;
    public Sprite Icon => _icon;
    public float Exp_n => _exp_n;
    public float Exp_m => _exp_m;
    public int PlusHP => _plusHP;
}
