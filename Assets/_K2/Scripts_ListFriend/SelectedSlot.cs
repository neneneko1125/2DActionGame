using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 4
/// 出撃枠一つを管理
/// 最大3つ存在する(今後増えるかも)
/// </summary>
public class SelectedSlot : MonoBehaviour
{
    
    [Header("名前")]
    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("HPの飾り")]
    [SerializeField] private TextMeshProUGUI _hpText_Deco;

    [Header("HPテキスト")]
    [SerializeField] private TextMeshProUGUI _hpText;

    [Header("アイコン")]
    [SerializeField] private Image _icon;

    [Header("編成から外すボタン")]
    [SerializeField] private Button _removeButton;

    private FriendData _data;

    private void Start()
    {
        SelectedFriendsUIManager.Instance.RefreshSlotUI();
    }

    /// <summary>
    /// SelectedFriendsUIManager.Refreshから呼ばれる
    /// 出撃枠のUIを更新しているのは実質Refreshと言うよりこのメソッド
    /// </summary>
    /// <param name="data"></param>
    public void Set(FriendData data)
    {
        _data = data;
        _hpText_Deco.text = "HP";
        _nameText.text = data.Name;
        _hpText.text = data.MaxHP.ToString();

        _icon.enabled = true;
        _icon.sprite = data.Icon; 


        //以前登録されていたクリック処理を全部消す AddListenerは上書きじゃなくて追加だから
        _removeButton.onClick.RemoveAllListeners();

        //RemoveButtonにOnClickの登録をする(つまり出撃枠に何もいないときにRemoveを押しても何も起こらない)
        //出撃メンバーからこのキャラを削除 出撃枠UIを更新
        _removeButton.onClick.AddListener(() =>
        {
            //出撃メンバーから削除
            OrganizationManager.Instance.RemoveFriend(_data);

            //出撃枠UIの更新
            SelectedFriendsUIManager.Instance.RefreshSlotUI();
        });
    }

    /// <summary>
    /// データを削除
    /// </summary>
    public void Clear()
    {
        _data = null;
        _nameText.text = "";
        _hpText_Deco.text = "";
        _hpText.text = "";
        _icon.enabled = false;
        _icon.sprite = null;
    }
}
