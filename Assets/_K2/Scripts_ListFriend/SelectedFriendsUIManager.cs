using UnityEngine;

/// <summary>
/// 出撃メンバー全体の管理
/// SelectedSlot をまとめて管理
/// 追加できるか判断
/// 解除・入れ替え処理
/// </summary>
public class SelectedFriendsUIManager : MonoBehaviour
{
    public static SelectedFriendsUIManager Instance;

    [SerializeField] private SelectedSlot[] _slots;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 出撃枠UIを更新する
    /// </summary>
    public void Refresh()
    {
        var list = OrganizationManager.Instance.SelectedFriends;

        for (int i = 0; i < _slots.Length; i++)
        {
            //出撃枠が残ってるか確認
            if (i < list.Count)
                _slots[i].Set(list[i]);
            else
                _slots[i].Clear();
        }
    }
}
