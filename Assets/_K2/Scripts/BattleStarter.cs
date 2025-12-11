using System;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    void Start()
    {
        var friends = OrganizationManager.Instance.SelectedFriends;

        Debug.Log($"SelectedFriends count = {friends.Count}");


        for (int i = 0; i < friends.Count; i++)
        {
            FriendData friend = friends[i];

            // 変動情報を作成
            FriendInstanceData instance = new FriendInstanceData(friend);

            // 画面にキャラを生成
            GameObject obj = Instantiate(friend.ActionPrefab);

            // FriendHP へ (インスタンスデータ + UI index) を渡す
            FriendHP hp = obj.GetComponent<FriendHP>();

            if(hp != null) 
                hp.Initialize(instance, i);

        }


    }
}
