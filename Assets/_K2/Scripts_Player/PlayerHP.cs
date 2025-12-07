using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public int maxhp = 100;
    public int hp;

    private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1.0f;
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private Image _hpBarImage;
    [SerializeField] private TextMeshProUGUI hpText; 

    [SerializeField] private SpriteRenderer _sr;
    private PlayerATK _playerATK;

    private void Awake()
    {
       _playerATK = GetComponent<PlayerATK>();
    }

    void Start()
    {
        hp = maxhp;

        //ç≈èâÇ…TextÇ∆BarÇèâä˙âª
        UpdateHPUI();
    }


    void Update()
    {

    }

    public IEnumerator ReduceHP(int damage)
    {
        if (_isInvincible || (_playerATK != null &&_playerATK.isGuard))
        {
            yield break;
        }

        hp -= damage;
        SEManager.Instance.SEDamage();

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(BlinkInvincible());
        UpdateHPUI();
    }

    /// <summary>
    /// ñ≥ìGÇ…Ç»Ç¡ÇΩÇ∆Ç´ÇÃì_ñ≈
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
            _hpBarImage.fillAmount = (float)hp / maxhp;
        }

        if (hpText != null)
        {
            hpText.text = hp.ToString();
        }
    }
}
