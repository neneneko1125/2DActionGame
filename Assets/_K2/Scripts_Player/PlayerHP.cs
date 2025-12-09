using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public int MaxHP = 100;
    public int HP;

    private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1.0f;
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private Image _hpBarImage;
    [SerializeField] private TextMeshProUGUI _hpText; 

    [SerializeField] private SpriteRenderer _sr;
    private PlayerATK _playerATK;


    private void Awake()
    {
        _playerATK = GetComponent<PlayerATK>();
    }


    void Start()
    {
        HP = MaxHP;

        //最初にTextとBarを初期化
        UpdateHPUI();
    }


    void Update()
    {

    }

    public IEnumerator ReduceHP(int damage)
    {
        if (_isInvincible || (_playerATK != null &&_playerATK.IsGuard))
        {
            yield break;
        }

        HP -= damage;
        SEManager.Instance.SEDamage();

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(BlinkInvincible());
        UpdateHPUI();
    }

    /// <summary>
    /// レベルアップしたときに呼ばれる
    /// </summary>
    /// <param name="plusHP"></param>
    /// <param name="healAmount"></param>
    public void LvUpHP(int plusHP, int healAmount)
    {
        MaxHP += plusHP;   //最大HPの更新
        Heal(healAmount);      //回復メソッド
    }

    /// <summary>
    /// 回復　レベルアップのときにも呼ばれる
    /// </summary>
    /// <param name="healAmount"></param>
    private void Heal(int healAmount)
    {
        HP += healAmount;    //現在のHPに回復量を加算
        HP = Mathf.Clamp(HP, 0, MaxHP);  //最大HPを超えないように丸め込む

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

        while (timer < _invincibleTime)
        {
            _sr.enabled = !_sr.enabled;
            yield return new WaitForSeconds(_blinkIntervalTime);
            timer += _blinkIntervalTime;
        }

        _sr.enabled = true;

        _isInvincible = false;
    }



    private void UpdateHPUI()
    {
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = (float)HP / MaxHP;
        }

        if (_hpText != null)
        {
            _hpText.text = HP.ToString();
        }
    }
}
