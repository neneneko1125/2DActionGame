using UnityEngine;

public class EXPGetManager : MonoBehaviour
{
    public static EXPGetManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// EnemyHPで呼び出される
    /// 敵を倒したときの経験値量はEnemyHPで管理している
    /// </summary>
    /// <param name="exp"></param>
    public void AddExpToAll(int exp)
    {
        var ch = CharInstanceManager.Instance;

        //Playerがnullじゃなければ、Playerに経験値を与える
        ch.Player?.AddExp(exp);
        
        //それぞれのFriendがnullじゃなければ、Friendたちに経験値を与える
        foreach (var f in ch.Friends)
        {
            f?.AddExp(exp);
        }
            
    }
}
