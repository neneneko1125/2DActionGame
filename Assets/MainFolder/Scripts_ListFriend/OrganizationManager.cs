using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ここが出発点　１
/// チーム編成するときの管理クラス
/// 
/// ＜仕事内容＞
/// 全メンバーリストから出撃メンバーリストを管理(SOデータで)
/// PlayerとFriendたちのSOデータを保持する
/// シーン遷移直前にSOデータをもとにInstanceDataを作成
/// 作成したInstanceDataをCharInstanceManagerに送る
/// SOデータを全メンバーリストから出撃メンバーリストに加える
/// 出撃枠が余ってるか、重複してないかチェックする
/// </summary>
public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager Instance { get; private set; }

    [Header("PlayerData(SO)")]
    [SerializeField] private PlayerData _playerData;

    public List<FriendData> AllFriends = new List<FriendData>();   // 全キャラ

    /// <summary>
    /// ここに入っているのはSOデータ InstanceDataではない
    /// </summary>
    public List<FriendData> SelectedFriends = new List<FriendData>(); // 出撃メンバー

    public const int MaxFriendCount = 3;    //最大出撃枠数


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

    /// <summary>
    /// MainSceneへの遷移や、ステージ移動(の直前)で呼ばれる
    /// ここでそれぞれのInstanceDataを作成し、コンストラクタを呼び出す
    /// コンストラクタではSOデータに書かれた初期値を代入した後、
    /// LoadしてからSaveすることによってステータス(残りHP,EXP,Lv)を受け継ぐ
    /// </summary>
    public void MakeAndSendInstanceData()
    {
        //PlayerのInstanceData作成
        PlayerInstanceData playerInstanceData = new PlayerInstanceData(_playerData);

        //InstanceDataをInstanceManagerに渡す
        CharacterInstanceManager.Instance.SetPlayer(playerInstanceData);

        //ここからFirned-------------------------------------------------------------------

        //FriendInstanceDataのリストを用意する
        var list = new List<FriendInstanceData>();

        foreach (var data in SelectedFriends)
        {
            //FriendInstanceDataを一匹ずつ作成
            FriendInstanceData instance = new FriendInstanceData(data);

            //InstanceDataが生成されるとリストに追加
            list.Add(instance);
        }

        //InstanceDataをリストごとInstanceManagerに渡す
        CharacterInstanceManager.Instance.SetFriends(list);
    }

    /// <summary>
    /// AllFriendUIManagerから呼ばれる
    /// ボタンを押したタイミングで確認＋編成に追加する
    /// ここで操作するのはSOデータで、InstanceDataではない
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool TryAddFriend(FriendData data)
    {
        if (SelectedFriends.Count >= MaxFriendCount)
        {
            Debug.Log("最大枠数を超えています");
            return false;
        }
            
        if (SelectedFriends.Contains(data))
        {
            Debug.Log("既に出撃枠にいます");
            return false;
        }
            
        //出撃メンバーに加える
        SelectedFriends.Add(data);
        return true;
    }

    /// <summary>
    /// SelectedButtonから呼ばれる
    /// 選択したFriendを出撃メンバーから外す
    /// </summary>
    /// <param name="data"></param>
    public void RemoveFriend(FriendData data)
    {
        SelectedFriends.Remove(data);
    }
}