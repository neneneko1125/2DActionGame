using UnityEngine;

public class CharacterInitializer : MonoBehaviour
{
    void Start()
    {
        SetFriendsData();
        SetPlayerData();
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


            //FriendATKObjectへインスタンスデータを渡す
            FriendATKObject[] atkObjs = obj.GetComponentsInChildren<FriendATKObject>(true);
            if (atkObjs.Length == 0)
            {
                Debug.LogError("FriendATKObject が1つも見つかりません");
                return;
            }
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
        Debug.Log("Playerを生成");

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
