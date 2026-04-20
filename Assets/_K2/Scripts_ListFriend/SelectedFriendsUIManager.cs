using UnityEngine;

/// <summary>
/// 3
/// 出撃メンバー全体の管理
/// SelectedSlot をまとめて管理
/// 追加できるか判断
/// 解除・入れ替え処理
/// </summary>
public class SelectedFriendsUIManager : MonoBehaviour
{
    public static SelectedFriendsUIManager Instance;

    [Header("出撃メンバーのスロット")]
    [SerializeField] private SelectedSlot[] _slots;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 出撃枠UIを更新する
    /// 新しいメンバーを追加するとき、ゲーム起動したとき、削除ボタンが押されたときに呼び出される
    /// </summary>
    public void RefreshSlotUI()
    {
        //現在出撃枠にいるメンバーたち
        var list = OrganizationManager.Instance.SelectedFriends;

        for (int i = 0; i < _slots.Length; i++)
        {
            //出撃枠が残ってるか確認
            //枠がまだ残ってた場合
            if (i < list.Count) 
            {
                //出撃枠UIをセットしにいく
                _slots[i].Set(list[i]);
            }
            //枠がいっぱいだった場合
            else
            {
                //UIデータを削除
                //これがないといないはずのメンバーのUIが残り続けてしまう
                _slots[i].Clear();
            }
               
        }
    }
}
