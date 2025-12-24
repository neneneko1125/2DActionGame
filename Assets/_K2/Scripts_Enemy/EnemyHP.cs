using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    [SerializeField, Header("最大HP")] private int _maxHP = 30;

    //現在のHP
    private int _currentHP;

    [SerializeField, Header("経験値")] private int _exp;

    //無敵中ならtrue
    private bool _isInvincible = false;

    [SerializeField, Header("被弾した後の無敵の時間")] private float _invincibleTime = 1.0f;

    [SerializeField, Header("一回の点滅の時間")] private float _blinkIntervalTime = 0.1f;

    [SerializeField, Header("HPバーの画像")] private Image _hpBarImage;

    [SerializeField, Header("ドロップアイテム")] private GameObject _dropItem;

    [SerializeField, Header("敵のBody側をアタッチ")] private SpriteRenderer _sr;

    //死亡時にプレイヤーの味方に知らせる
    public System.Action OnDead;


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
    public IEnumerator ReduceHP(int damage)
    {
        //無敵中なら
        if (_isInvincible)
        {
            //このメソッドをぬける
            yield break;
        }

        _currentHP -= damage;

        //SEManager.Instance.SEDamage();

        DamageTextSpawn.Instance.SpawnDamageTextEnemy(transform.position, damage);

        //HPが0以下になったら
        if (_currentHP <= 0)
        {
            //プレイヤーの味方に知らせる
            OnDead?.Invoke();

            if(EXPGetManager.Instance != null)
            {
                EXPGetManager.Instance.AddExpToAll(_exp);
            }
            else
            {
                Debug.Log("EXPGetManagerのInstanceがnull");
            }

            if(_dropItem != null)
            {
                //コインを生成
                Instantiate(_dropItem, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("何も落とさなかった");
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
            _hpBarImage.fillAmount = (float)_currentHP / _maxHP;
        }
    }
}
