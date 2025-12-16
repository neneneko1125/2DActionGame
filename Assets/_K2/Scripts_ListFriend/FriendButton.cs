using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// FriendDataを1体分保持する
/// クリックされたらOrganizationManagerに「このキャラを選択していいか」を問い合わせる
/// </summary>
public class FriendButton : MonoBehaviour
{
    [SerializeField, Header("名前のテキスト")] private Text _nameText;
    [SerializeField, Header("ボタン")] private Button _button;

    private FriendData _data;

    /// <summary>
    /// AllFriendsUIManagerで呼び出される
    /// </summary>
    /// <param name="data"></param>
    /// <param name="onClick"></param>
    public void Initialize(FriendData data, Action onClick)
    {
        _data = data;   //SOのデータ
        _nameText.text = data.FriendName;   //SOから名前のデータ入手

        //イベントで選択されたことを知らせる
        _button.onClick.AddListener(() => onClick?.Invoke());
    }
}
