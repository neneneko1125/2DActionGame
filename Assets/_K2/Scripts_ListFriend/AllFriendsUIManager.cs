using UnityEngine;

/// <summary>
/// 1
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
            //_contentを親にしてFriendButtonを生成する
            var btn = Instantiate(_buttonPrefab, _content);

            //FriendButtonのInitializeを呼び出す
            btn.Initialize(data, () => OnFriendClicked(data));

        }
    }

    /// <summary>
    /// 実質、FriendButtonから呼び出される
    /// FriendButtonがイベントで選ばれたことを知らせて
    /// このメソッドを実行する
    /// </summary>
    /// <param name="data"></param>
    private void OnFriendClicked(FriendData data)
    {
        //OrganizationManagerで既にチームに含まれてるか確認する
        //含まれていなければデータを追加してから
        if (OrganizationManager.Instance.TryAddFriend(data))
        {
            //出撃UIを更新
            SelectedFriendsUIManager.Instance.RefreshSlotUI();
       
        }
    }

}
