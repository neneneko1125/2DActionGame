using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    [Header("最大HP")]
    [SerializeField] private int _maxHP = 30;

    //現在のHP
    private int _currentHP;

    [Header("経験値")]
    [SerializeField] private int _exp;

    [Header("被弾した後の無敵の時間")]
    [SerializeField] private float _invincibleTime = 1.0f;

    [Header("一回の点滅の時間")]
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [Header("HPバーの画像")]
    [SerializeField] private Image _hpBarImage;

    [Header("ドロップアイテム")]
    [SerializeField] private GameObject _dropItem;

    [Header("敵のBody側をアタッチ")]
    [SerializeField] private SpriteRenderer _sr;

    //無敵中ならtrue
    private bool _isInvincible = false;

    void Start()
    {
        //最初に現在のHPを最大HPにする
        _currentHP = _maxHP;
    }

    /// <summary>
    /// HPを減らす
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public IEnumerator ReduceHP(int damage, bool isCritical)
    {
        //無敵中なら
        if (_isInvincible)
        {
            //このメソッドをぬける
            yield break;
        }

        //HPを減らす
        _currentHP -= damage;

        //会心の一撃なら
        if (isCritical)
        {
            //黄色の文字で受けたダメージを表示する
            DamageAndHealTextSpawn.Instance.SpawnCriticalDamageText(transform.position, damage);
        }
        else
        {
            //白文字で受けたダメージ数を表示する
            DamageAndHealTextSpawn.Instance.SpawnDamageTextEnemy(transform.position, damage);
        }
        

        //HPが0以下になったら
        if (_currentHP <= 0)
        {
            if(EXPGetManager.Instance != null)
            {
                //プレイヤーたちに経験値を配る
                EXPGetManager.Instance.AddExpToAll(_exp);
            }

            if(_dropItem != null)
            {
                //アイテムを生成
                Instantiate(_dropItem, transform.position, Quaternion.identity);
            }
                
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

        //元の色
        Color defaultColor = _sr.color;

        while (timer < _invincibleTime)
        {
            //今不透明(アルファ値1)なら
            if (_sr.color.a == 1.0f)
            {
                //透明にする
                _sr.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.0f);
            }
            else
            {
                //元に戻す
                _sr.color = defaultColor;
            }
            yield return new WaitForSeconds(_blinkIntervalTime);
            timer += _blinkIntervalTime;
        }

        //最後は必ず不透明に戻す
        _sr.color = defaultColor;
        _isInvincible = false;
    }


    private void UpdateHPBar()
    {
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = (float)_currentHP / _maxHP;
        }
    }
}
