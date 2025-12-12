using System.Collections.Generic;
using UnityEngine;

public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager Instance;

    [SerializeField] private PlayerData _playerData;
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
        EXPGetManager.Instance.Player = new PlayerInstanceData(_playerData);

        // Friends
        foreach (var data in SelectedFriends)
        {
            EXPGetManager.Instance.Friends.Add(new FriendInstanceData(data));
        }
    }
}
