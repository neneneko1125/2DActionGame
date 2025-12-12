using UnityEngine;
using System.Collections.Generic;

public class EXPGetManager : MonoBehaviour
{
    public static EXPGetManager Instance;

    public PlayerInstanceData Player;
    public List<FriendInstanceData> Friends = new List<FriendInstanceData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Enemy から呼ばれる
    public void AddExpToAll(int exp)
    {
        // プレイヤー
        if(Player != null)
        {
            Debug.Log("EXPGetManagerでPlayerのAddExpを呼び出した");
            Player.AddExp(exp);
        }

        // 全フレンド
        foreach (var f in Friends)
        {
            if(f != null)
            {
                Debug.Log("EXPGetManagerでFriendのAddExpを呼び出した");
                f.AddExp(exp);
            }
        }
    }
}
