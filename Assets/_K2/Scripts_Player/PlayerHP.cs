using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    private PlayerInstanceData _instance;

    private CharDataUIManager _ui;

    private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1.0f;
    [SerializeField] private float _blinkIntervalTime = 0.1f;


    [SerializeField] private SpriteRenderer _sr;
    private PlayerATK _playerATK;


    private void Awake()
    {
        _playerATK = GetComponent<PlayerATK>();
    }


    public void Initialize(PlayerInstanceData data)
    {
        _ui = FindAnyObjectByType<CharDataUIManager>();

        _instance = data;
        _instance.OnLvUp += UpdateLvEXPUI;
        _instance.OnExpChanged += UpdateLvEXPUI;

        UpdateHPUI();
        UpdateLvEXPUI();
    }


    void Update()
    {

    }

    public IEnumerator ReduceHP(int damage)
    {
        if (_isInvincible || (_playerATK != null && _playerATK.IsGuard))
        {
            yield break;
        }

        _instance.currentHP -= damage;
        SEManager.Instance.SEDamage();

        if (_instance.currentHP <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(BlinkInvincible());
        UpdateHPUI();
    }

    /// <summary>
    /// 回復　レベルアップのときにも呼ばれる
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(int healAmount)
    {
        int maxHP = _instance.baseData.MaxHP + (_instance.currentLv - 1) * _instance.baseData.PlusHP;

        _instance.currentHP += healAmount;
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
        _ui.UpdateHPUIOfPlayer(_instance.currentHP, MaxHP());
    }

    private void UpdateLvEXPUI()
    {
        Debug.Log("Lv = " + _instance.currentLv + "  EXP = " + _instance.currentEXP);
        _ui.UpdateLvEXPUIOfPlayer(_instance.currentLv, _instance.currentEXP, _instance.NeedExp());
    }

    private int MaxHP()
    {
        return _instance.baseData.MaxHP + (_instance.currentLv - 1) * _instance.baseData.PlusHP;
    }

}
