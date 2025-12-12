using System;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    [SerializeField] private PlayerData _player;

    void Start()
    {
        SetFriendsData();
        SetPlayerData();
    }

    private void SetFriendsData()
    {
        var friends = EXPGetManager.Instance.Friends;

        Debug.Log($"SelectedFriends count = {friends.Count}");

        for (int i = 0; i < friends.Count; i++)
        {
            FriendInstanceData instance = friends[i];

            // 画面にキャラを生成
            GameObject obj = Instantiate(instance.baseData.ActionPrefab);

            // FriendHP へインスタンスデータ + インデックスを渡す
            FriendHP hp = obj.GetComponent<FriendHP>();

            if (hp != null)
                hp.Initialize(instance, i);
        }
    }



    private void SetPlayerData()
    {
        PlayerInstanceData instance = EXPGetManager.Instance.Player;

        GameObject obj = Instantiate(instance.baseData.ActionPrefab);

        PlayerHP hp = obj.GetComponent<PlayerHP>();
        hp.Initialize(instance);
    }

}
