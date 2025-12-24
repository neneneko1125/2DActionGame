using UnityEngine;
using System.Collections;

public class CharacterInitializer : MonoBehaviour
{
    void Start()
    {
        SetFriendsData();
        SetPlayerData();
    }

    private IEnumerator InitializeFriendsDelayed()
    {
        yield return new WaitForSeconds(0.1f); // 遅らせたい秒数
        SetFriendsData();
    }

    private void SetFriendsData()
    {
        var friends = CharInstanceManager.Instance.Friends;

        Debug.Log($"SelectedFriends count = {friends.Count}");

        for (int i = 0; i < friends.Count; i++)
        {
            FriendInstanceData instance = friends[i];

            // 画面にキャラを生成
            GameObject obj = Instantiate(instance.baseData.ActionPrefab);

            // FriendHPへインスタンスデータ + インデックスを渡す
            FriendHP hp = obj.GetComponent<FriendHP>();
            if (hp != null)
                hp.Initialize(instance, i);

            //Friend4は遠距離回復なので個別にインスタンスデータを扱う
            FriendBaseHeal friendHeal = obj.GetComponent<FriendBaseHeal>();
            if(friendHeal != null)
            {
                friendHeal.Initialize(instance);
            }

            //Friend5は遠距離攻撃なので個別にインスタンスデータを扱う
            Friend5ATK friend5ATK = obj.GetComponent<Friend5ATK>();
            if (friend5ATK != null)
            {
                friend5ATK.Initialize(instance);
            }


            //FriendATKObjectへインスタンスデータを渡す
            FriendATKObject[] atkObjs = obj.GetComponentsInChildren<FriendATKObject>(true);
            if (atkObjs.Length == 0) Debug.Log("FriendATKObject が1つも見つかりません");

            foreach (var atkObj in atkObjs)
            {
                if (atkObj != null)
                    atkObj.Initialize(instance);
            }


        }
    }



    private void SetPlayerData()
    {
        PlayerInstanceData instance = CharInstanceManager.Instance.Player;

        GameObject obj = Instantiate(instance.baseData.ActionPrefab);

        PlayerHP hp = obj.GetComponent<PlayerHP>();
        if (hp != null)
            hp.Initialize(instance);

        PlayerATKObject[] atkObjs = obj.GetComponentsInChildren<PlayerATKObject>(true);
        if (atkObjs.Length == 0)
        {
            Debug.LogError("PlayerATKObject が1つも見つかりません");
            return;
        }
        foreach (var atkObj in atkObjs)
        {
            if (atkObj != null)
                atkObj.Initialize(instance);
        }

    }

}
