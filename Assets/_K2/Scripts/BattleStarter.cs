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
        var friends = OrganizationManager.Instance.SelectedFriends;

        Debug.Log($"SelectedFriends count = {friends.Count}");


        for (int i = 0; i < friends.Count; i++)
        {
            FriendData friend = friends[i];

            //変動情報を作成
            FriendInstanceData instance = new FriendInstanceData(friend);

            //画面にキャラを生成
            GameObject obj = Instantiate(friend.ActionPrefab);

            //FriendHPへ(インスタンスデータ+インデックス)を渡す
            FriendHP hp = obj.GetComponent<FriendHP>();

            if (hp != null)
                hp.Initialize(instance, i);

        }
    } 

    private void SetPlayerData()
    {
        //変動情報を作成
        PlayerInstanceData instance = new PlayerInstanceData(_player);

        //画面にプレイヤーを生成
        GameObject obj = Instantiate(_player.ActionPrefab);

        //PlayerHPへインスタンスデータを渡す
        PlayerHP hp = obj.GetComponent<PlayerHP>();

        if (hp != null)
            hp.Initialize(instance);
    }
}
