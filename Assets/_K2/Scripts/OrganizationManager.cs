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
        // Player
        CharInstanceManager.Instance.SetPlayer(new PlayerInstanceData(_playerData));

        // Friends
        var list = new List<FriendInstanceData>();

        foreach (var data in SelectedFriends)
            list.Add(new FriendInstanceData(data));

        CharInstanceManager.Instance.SetFriends(list);
    }



}
