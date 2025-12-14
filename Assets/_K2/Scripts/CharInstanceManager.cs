using System.Collections.Generic;
using UnityEngine;

public class CharInstanceManager : MonoBehaviour
{
    public static CharInstanceManager Instance { get; private set; }
    
    public PlayerInstanceData Player { get; private set; }
    public List<FriendInstanceData> Friends { get; private set; } = new();

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
    /// CharInstanceManagerのPlayerにInstanceDataを収納するためのメソッド
    /// OrganizationManagerから呼び出される
    /// PlayerにはOrganizationManagerの_playerDataが収納される
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(PlayerInstanceData player)
    {
        Player = player;
    }

    /// <summary>
    /// CharInstanceManagerのFriendsにInstanceDataを収納するためのメソッド
    /// OrganizationManagerから呼び出される
    /// Friends(リスト)にはOrganizationManagerのSelectedFriends(リスト)が収納される
    /// </summary>
    /// <param name="friends"></param>
    public void SetFriends(List<FriendInstanceData> friends)
    {
        Friends = friends;
    }
}
