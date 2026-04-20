using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 現在のHP,最大HPはPlayerInstanceDataで管理
/// </summary>
public class PlayerHP : BaseHP<PlayerInstanceData>
{
    private CharDataUIManager _ui;  
    private PlayerAttack _playerAttack;
    private PlayerInput _input;

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _input = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// CharacterInitializerでゲーム開始時に呼び出される
    /// 既にInstanceDataは作成されており、ステージ遷移前の
    /// セーブデータをロードして、重ねてセーブした後で呼び出されるため、
    /// UpdateHPLvEXPUIの呼び出しはコンストラクタ内で問題ない
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(PlayerInstanceData data)
    {
        //インスタンスデータを収納
        _instanceData = data;

        _ui = FindAnyObjectByType<CharDataUIManager>();

        //OnChangeLvEXPが発生したらUpdateHPLvEXPUIを実行されるようにする
        _instanceData.OnChangeLvEXP += UpdateHPLvEXPUI;

        CheckDead();
        UpdateHPLvEXPUI();
    }

    /// <summary>
    /// HPを減らす
    /// ダメージを受けた時にAttackObjectで呼び出される
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public override IEnumerator ReduceHP(int damage)
    {
        //無敵またはガード中ならメソッドをぬける
        if (_isInvincible || (_playerAttack != null && _input.IsGuarding))
        {
            yield break;
        }

        damage = _instanceData.GetBuffDamegeCut(damage);    //バフを受けに行く

        _instanceData.currentHP -= damage;  //HPを減らす

        Save(); //HPの減少を保存

        SEManager.Instance.SEDamage();

        DamageAndHealTextSpawn.Instance.SpawnDamageTextPlayerAndFriend(transform.position, damage);

        CheckDead();

        StartCoroutine(BlinkInvincible());

        UpdateHPUI();
    }

    /// <summary>
    /// プレイヤー側は死亡したときシーン遷移する必要がある
    /// </summary>
    protected override void CheckDead()
    {
        //HPが0以下になったら
        if (_instanceData.currentHP <= 0)
        {
            //UIのために0にする(マイナスにならないように)
            _instanceData.currentHP = 0;
            SceneManager.LoadScene("TitleScene");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// HPのUIを更新
    /// </summary>
    protected override void UpdateHPUI()
    {
        _ui.UpdateHPUIOfPlayer(_instanceData.currentHP, _instanceData.MaxHP);
    }

    /// <summary>
    /// LvとEXPのUIを更新
    /// </summary>
    protected override void UpdateLvEXPUI()
    {
        _ui.UpdateLvEXPUIOfPlayer(_instanceData.currentLv, _instanceData.currentEXP, _instanceData.NeedExp);
    }

    /// <summary>
    /// 親クラスで使ってるから消さないこと
    /// </summary>
    protected override void Save()
    {
        PlayerSaveManager.Save(_instanceData);
    }
}
