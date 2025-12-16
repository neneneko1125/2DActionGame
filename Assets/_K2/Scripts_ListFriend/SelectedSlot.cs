using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 出撃枠一つを管理
/// 最大3つ存在する(今後増えるかも)
/// </summary>
public class SelectedSlot : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Button _removeButton;

    private FriendData _data;

    /// <summary>
    /// SelectedFriendsUIManager.Refreshから呼ばれる
    /// </summary>
    /// <param name="data"></param>
    public void Set(FriendData data)
    {
        _data = data;
        _nameText.text = data.FriendName;

        //以前登録されていたクリック処理を全部消す
        _removeButton.onClick.RemoveAllListeners();

        //出撃メンバーからこのキャラを削除 出撃枠UIを更新
        _removeButton.onClick.AddListener(() =>
        {
            OrganizationManager.Instance.RemoveFriend(_data);
            SelectedFriendsUIManager.Instance.Refresh();
        });
    }

    /// <summary>
    /// データを削除
    /// </summary>
    public void Clear()
    {
        _data = null;
        _nameText.text = "";
    }
}
