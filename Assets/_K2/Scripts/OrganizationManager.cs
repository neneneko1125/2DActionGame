using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
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

}
