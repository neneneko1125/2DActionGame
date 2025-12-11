using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    //最大HP
    [SerializeField] private int _maxHP = 30;

    //現在のHP
    private int _nowHP;

    //経験値
    [SerializeField] private int _exp;

    //無敵中ならtrue
    private bool _isInvincible = false;

    //被弾した後の無敵の時間
    [SerializeField] private float _invincibleTime = 1.0f;

    //一回の点滅の時間
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private Image _hpBarImage;

    [SerializeField] private SpriteRenderer _sr;

    //死亡時にプレイヤーの味方に知らせる
    public System.Action OnDead;


    void Start()
    {
        //最初に現在のHPを最大HPにする
        _nowHP = _maxHP;
    }

    /// <summary>
    /// HPを減らす
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public IEnumerator ReduceHP(int damage)
    {
        //無敵中なら
        if (_isInvincible)
        {
            //このメソッドをぬける
            yield break;
        }

        _nowHP -= damage;

        //SEManager.Instance.SEDamage();

        //HPが0以下になったら
        if (_nowHP <= 0)
        {
            //プレイヤーの味方に知らせる
            OnDead?.Invoke();

            //プレイヤーの経験値を追加する
            PlayerLvEXP.Instance.AddExp(_exp);

            Destroy(gameObject);
        }

        StartCoroutine(BlinkInvincible());
        UpdateHPBar();
    }

    /// <summary>
    /// 無敵になったときの点滅
    /// </summary>
    /// <returns></returns>
    private IEnumerator BlinkInvincible()
    {
        _isInvincible = true;

        float timer = 0f;

        while (timer < _invincibleTime)
        {
            //透明不透明の切り替え
            _sr.enabled = !_sr.enabled;
            yield return new WaitForSeconds(_blinkIntervalTime);
            timer += _blinkIntervalTime;
        }

        //最後は必ず不透明になるようにする
        _sr.enabled = true;

        _isInvincible = false;
    }


    private void UpdateHPBar()
    {
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = (float)_nowHP / _maxHP;
        }
    }
}
