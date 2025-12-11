using UnityEngine;

[CreateAssetMenu(menuName = "Character Data")]
public class FriendData : ScriptableObject
{
    public string friendName;
    public int maxHP;
    public int level;
    public Sprite faceIcon;
    public GameObject actionPrefab; // アクションシーンで使うプレハブ
}
