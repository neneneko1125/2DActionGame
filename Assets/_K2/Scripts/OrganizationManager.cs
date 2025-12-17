using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// チーム編成するときの管理クラス
/// チーム全体が収納されているリストと
/// 出撃メンバーが収納されているリストで管理する
/// </summary>
public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager Instance { get; private set; }

    [SerializeField, Header("PlayerData(SO)")] private PlayerData _playerData;

    public List<FriendData> AllFriends = new List<FriendData>();   // 全キャラ
    public List<FriendData> SelectedFriends = new List<FriendData>(); // 出撃メンバー

    public const int MaxFriendCount = 3;

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

    public void ApplyOrganization()
    {
        //CharInstanceManagerのPlayerにInstanceDataを収納する
        //new PlayerInstanceData(_playerData):SOのデータを元に、Instanceデータを作成
        CharInstanceManager.Instance.SetPlayer(new PlayerInstanceData(_playerData));

        var list = new List<FriendInstanceData>();
        foreach (var data in SelectedFriends)
        {
            //new FriendInstanceData(data):SOのデータを元に、Instanceデータを作成
            list.Add(new FriendInstanceData(data));
        }
        //CharInstanceManagerのFriendsにInstanceDataを収納する
        CharInstanceManager.Instance.SetFriends(list);
    }



    public bool TryAddFriend(FriendData data)
    {
        if (SelectedFriends.Count >= MaxFriendCount)
        {
            Debug.Log("最大枠数を超えています");
            return false;
        }
            

        //既に出撃枠に含まれていたら
        if (SelectedFriends.Contains(data))
        {
            Debug.Log("既に出撃枠にいます");
            return false;
        }
            
        //出撃メンバーに加える
        SelectedFriends.Add(data);
        return true;
    }

    public void RemoveFriend(FriendData data)
    {
        SelectedFriends.Remove(data);
    }

    public bool IsSelected(FriendData data)
    {
        return SelectedFriends.Contains(data);
    }

}
