using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// 2
/// FriendDataを1体分保持する
/// クリックされたらOrganizationManagerに「このキャラを選択していいか」を問い合わせる
/// </summary>
public class FriendButton : MonoBehaviour
{
    [Header("名前のテキスト")]
    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("レベルのテキスト")]
    [SerializeField] private TextMeshProUGUI _hpText;

    [Header("ボタン")]
    [SerializeField] private Button _button;

    [Header("アイコン")]
    [SerializeField] private Image _icon;

    [Header("タイプ")]
    [SerializeField] private TextMeshProUGUI _type;


    /// <summary>
    /// AllFriendsUIManagerで呼び出される
    /// </summary>
    /// <param name="data"></param>
    /// <param name="onClick"></param>
    public void Initialize(FriendData data, Action onClick)
    {
        _nameText.text = data.Name;   //SOから名前のデータ入手
        _hpText.text = data.MaxHP.ToString();   //SOから最大HPを入手
        _icon.sprite = data.Icon;  //SOからSpriteを取得してImageに変換
        _type.text = data.Type.ToString();

        //イベントで選択されたことをAllFriendsUIManagerに知らせる
        _button.onClick.AddListener(() => onClick?.Invoke());

    }
}