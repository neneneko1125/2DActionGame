using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// 現在のHP,最大HPはFriendInstanceDataで管理
/// </summary>
public class FriendHP : MonoBehaviour
{
    private FriendInstanceData _instance;

    private int _uiIndex;
    private CharDataUIManager _ui;

    private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1.0f;
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private Image _hpBarImage;

    [SerializeField] private SpriteRenderer _sr;
   

    public void Initialize(FriendInstanceData data, int uiIndex)
    {
        _instance = data;
        _uiIndex = uiIndex;
        _ui = FindAnyObjectByType<CharDataUIManager>();

        _instance.OnLvUp += UpdateLvEXPUI;
        _instance.OnExpChanged += UpdateLvEXPUI;

        UpdateHPUI();
        UpdateLvEXPUI();
    }

    public IEnumerator ReduceHP(int damage)
    {
        if (_isInvincible)
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
    /// <param name="healAmount"></param>t
    private void Heal(int healAmount)
    {
        _instance.currentHP += healAmount;    //現在のHPに回復量を加算
        _instance.currentHP = Mathf.Clamp(_instance.currentHP, 0, _instance.baseData.MaxHP);  //最大HPを超えないように丸め込む

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
        _ui.UpdateHPUIOfFriend(_uiIndex, _instance.currentHP, MaxHP());
    }

    private void UpdateLvEXPUI()
    {
        Debug.Log("Lv = " + _instance.currentLv + "  EXP = " + _instance.currentEXP);
        _ui.UpdateLvEXPUIOfFriend(_uiIndex, _instance.currentLv, _instance.currentEXP, _instance.NeedExp());
    }

    private int MaxHP()
    {
        return _instance.baseData.MaxHP + (_instance.currentLv - 1) * _instance.baseData.PlusHP;
    }
}
