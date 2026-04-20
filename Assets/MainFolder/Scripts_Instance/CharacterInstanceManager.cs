using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2
/// InstanceData(ゲーム中のデータ)はここで管理している
/// InstanceDataがほしいときはこれを参照すること
/// </summary>
public class CharacterInstanceManager : MonoBehaviour
{
    public static CharacterInstanceManager Instance { get; private set; }
    
    //プレイヤーは一人だけだから、勝手にnewしてはいけない(管理しているのはOrganizationManager)
    public PlayerInstanceData PlayerInstanceData { get; private set; }

    //Friendsをnull状態にしないように、仲間を入れる箱だけ用意(newを使う)
    public List<FriendInstanceData> FriendsInstanceDataList { get; private set; } = new List<FriendInstanceData>();

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

    /// <summary>
    /// CharacterInstanceManagerのPlayerInstanceDataにInstanceDataを収納するためのメソッド
    /// OrganizationManagerから呼び出される
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(PlayerInstanceData player)
    {
        PlayerInstanceData = player;
    }

    /// <summary>
    /// CharInstanceManagerのFriendsInstanceDataListにInstanceDataを収納するためのメソッド
    /// OrganizationManagerから呼び出される
    /// </summary>
    /// <param name="friends"></param>
    public void SetFriends(List<FriendInstanceData> friends)
    {
        FriendsInstanceDataList = friends;
    }
}
