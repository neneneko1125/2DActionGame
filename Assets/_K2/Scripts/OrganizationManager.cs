using System.Collections.Generic;
using UnityEngine;

public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager Instance;

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
}
