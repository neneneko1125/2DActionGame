using UnityEngine;
using System.Collections;

/// <summary>
/// 現在のHP,最大HPはFriendInstanceDataで管理
/// </summary>
public class FriendHP : MonoBehaviour
{
    private FriendInstanceData _instance;   //インスタンスデータを収納

    private int _index;
    private CharDataUIManager _ui;

    private bool _isInvincible = false;    //無敵時間にtrue
    [SerializeField, Header("無敵時間")] private float _invincibleTime = 1.0f;
    [SerializeField, Header("点滅時間")] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private SpriteRenderer _sr;

    /// <summary>
    /// CharacterInitializerでゲーム開始時に呼び出される
    /// インデックスもここで取得
    /// </summary>
    /// <param name="data"></param>
    /// <param name="uiIndex"></param>
    public void Initialize(FriendInstanceData data, int uiIndex)
    {
        //インスタンスデータを収納
        _instance = data;
        //インデックスを収納
        _index = uiIndex;
        _ui = FindAnyObjectByType<CharDataUIManager>();

        //OnChangeLvEXP が発生したらUpdateHPLvEXPUIを実行されるようにする
        _instance.OnChangeLvEXP += UpdateHPLvEXPUI;

        UpdateHPLvEXPUI();
    }

    /// <summary>
    /// HPを減らす
    /// ダメージを受けた時にATKObjectで呼び出される
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public IEnumerator ReduceHP(int damage)
    {
        //無敵ならメソッドをぬける
        if (_isInvincible) yield break;

        _instance.currentHP -= damage;   //HPを減らす

        SEManager.Instance.SEDamage();

        //HPが0以下になったら
        if (_instance.currentHP <= 0)
        {
            //UIのために0にする(マイナスにならないように)
            _instance.currentHP = 0;
            Destroy(gameObject);
        }

        StartCoroutine(BlinkInvincible());
        UpdateHPUI();
    }

    /// <summary>
    /// 回復
    /// </summary>
    /// <param name="healAmount"></param>
    private void Heal(int healAmount)
    {
        //現在のレベルから最大HPを計算
        int maxHP = _instance.baseData.MaxHP + (_instance.currentLv - 1) * _instance.baseData.PlusHP;

        //HPが0以下でなければ回復
        if (_instance.currentHP <= 0) _instance.currentHP += healAmount;

        //最大HPを超えないように
        _instance.currentHP = Mathf.Clamp(_instance.currentHP, 0, maxHP);

        UpdateHPUI();
    }


    /// <summary>
    /// 無敵になったときの点滅
    /// </summary>
    /// <returns></returns>
    private IEnumerator BlinkInvincible()
    {
        _isInvincible = true;
        float timer = 0f;

        //タイマーが指定時間より経過してなければ無敵継続
        while (timer < _invincibleTime)
        {
            _sr.enabled = !_sr.enabled;     //点滅
            yield return new WaitForSeconds(_blinkIntervalTime);
            timer += _blinkIntervalTime;
        }

        //最後は必ず不透明にする
        _sr.enabled = true;
        _isInvincible = false;
    }

    /// <summary>
    /// HPのUIを更新
    /// </summary>
    private void UpdateHPUI()
    {
        _ui.UpdateHPUIOfFriend(_index, _instance.currentHP, MaxHP());
    }

    /// <summary>
    /// LvとEXPのUIを更新
    /// </summary>
    private void UpdateLvEXPUI()
    {
        _ui.UpdateLvEXPUIOfFriend(_index, _instance.currentLv, _instance.currentEXP, _instance.NeedExp());
    }

    /// <summary>
    /// イベント発生時などにこれを呼び出す
    /// HP、Lv、EXP全て更新する
    /// </summary>
    private void UpdateHPLvEXPUI()
    {
        UpdateHPUI();
        UpdateLvEXPUI();
    }

    /// <summary>
    /// 現在のレベルから最大HPを計算して返す
    /// </summary>
    /// <returns></returns>
    private int MaxHP()
    {
        return _instance.baseData.MaxHP + (_instance.currentLv - 1) * _instance.baseData.PlusHP;
    }

}
