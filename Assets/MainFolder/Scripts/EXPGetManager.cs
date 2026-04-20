using UnityEngine;

public class EXPGetManager : MonoBehaviour
{
    public static EXPGetManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// EnemyHPで呼び出される
    /// 敵を倒したときの経験値量はEnemyHPで管理している
    /// </summary>
    /// <param name="exp"></param>
    public void AddExpToAll(int exp)
    {
        //Playerがnullじゃなければ、Playerに経験値を与える
        CharacterInstanceManager.Instance.PlayerInstanceData?.AddExp(exp);
        
        //それぞれのFriendがnullじゃなければ、Friendたちに経験値を与える
        foreach (var f in CharacterInstanceManager.Instance.FriendsInstanceDataList)
        {
            f?.AddExp(exp);
        }
            
    }
}
