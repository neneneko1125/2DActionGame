using UnityEngine;

/// <summary>
/// 全キャラ一覧を生成・管理する
/// OrganizationManager.AllFriendsを読む
/// FriendButtonを人数分生成
/// FriendButtonにFriendDataを渡す
/// </summary>
public class AllFriendsUIManager : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private FriendButton _buttonPrefab;

    private void Start()
    {
        //全てのFriendを調べる
        foreach (var data in OrganizationManager.Instance.AllFriends)
        {
            Debug.Log("Friendボタン生成");

            //_contentを親にしてFriendButtonを生成する
            var btn = Instantiate(_buttonPrefab, _content);

            btn.Initialize(data, () => OnFriendClicked(data));

        }
    }

    private void OnFriendClicked(FriendData data)
    {
        //OrganizationManagerで既にチームに含まれてるか確認する
        //含まれていなければデータを追加してから
        if (OrganizationManager.Instance.TryAddFriend(data))
        {
            //出撃UIを更新
            SelectedFriendsUIManager.Instance.Refresh();
            Debug.Log("出撃枠へのデータの追加に成功しました");
        }
    }

}
