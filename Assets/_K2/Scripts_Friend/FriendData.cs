using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Friend Data")]
public class FriendData : ScriptableObject
{
    [SerializeField] private string _friendName;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _lv;
    [SerializeField] private GameObject _actionPrefab;
    [SerializeField] private Sprite _icon;

    //プロパティ 読み取り専用
    public string FriendName => _friendName;
    public int MaxHP => _maxHP;
    public int Level => _lv;
    public GameObject ActionPrefab => _actionPrefab;
    public Sprite Icon => _icon;
}
