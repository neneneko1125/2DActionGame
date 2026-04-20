
/// <summary>
/// 現在のHP,最大HPはFriendInstanceDataで管理
/// </summary>
public class FriendHP : BaseHP<FriendInstanceData>
{
    private int _index;
    private CharDataUIManager _ui;

    /// <summary>
    /// CharacterInitializerでゲーム開始時に呼び出される
    /// インデックスもここで取得
    /// </summary>
    /// <param name="data"></param>
    /// <param name="uiIndex"></param>
    public void Initialize(FriendInstanceData data, int uiIndex)
    {
        //インスタンスデータを収納
        _instanceData = data;
        //インデックスを収納
        _index = uiIndex;
        _ui = FindAnyObjectByType<CharDataUIManager>();

        //OnChangeLvEXPが発生したらUpdateHPLvEXPUIを実行されるようにする
        _instanceData.OnChangeLvEXP += UpdateHPLvEXPUI;

        CheckDead();
        UpdateHPLvEXPUI();
    }

    /// <summary>
    /// プレイヤー側は死亡したときシーン遷移する必要があるが
    /// こっちは普通でOK
    /// </summary>
    protected override void CheckDead()
    {
        //HPが0以下になったら
        if (_instanceData.currentHP <= 0)
        {
            //UIのために0にする(マイナスにならないように)
            _instanceData.currentHP = 0;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// HPのUIを更新
    /// </summary>
    protected override void UpdateHPUI()
    {
        _ui.UpdateHPUIOfFriend(_index, _instanceData.currentHP, _instanceData.MaxHP);
    }

    /// <summary>
    /// LvとEXPのUIを更新
    /// </summary>
    protected override void UpdateLvEXPUI()
    {
        _ui.UpdateLvEXPUIOfFriend(_index, _instanceData.currentLv, _instanceData.currentEXP, _instanceData.NeedExp);
    }

    /// <summary>
    /// 親クラスで使ってるから消さないこと
    /// </summary>
    protected override void Save()
    {
        FriendSaveManager.Save(_instanceData);
    }
}
