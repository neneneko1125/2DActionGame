using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    public int MaxHP = 100;
    public int HP;

    [SerializeField] private int _exp;

    private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1.0f;
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private Image _hpBarImage;

    [SerializeField] private SpriteRenderer _sr;

    public System.Action OnDead;



    private void Awake()
    {
       
    }

    void Start()
    {
        HP = MaxHP;
    }


    void Update()
    {

    }

    public IEnumerator ReduceHP(int damage)
    {
        if (_isInvincible)
        {
            yield break;
        }

        HP -= damage;

        //SEManager.Instance.SEDamage();

        if (HP <= 0)
        {
            OnDead?.Invoke();
            PlayerLvEXP.Instance.AddExp(_exp);
            Destroy(gameObject);
        }

        StartCoroutine(BlinkInvincible());
        UpdateHPBar();
    }

    /// <summary>
    /// –³“G‚É‚È‚Á‚½‚Æ‚«‚Ì“_–Å
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


    private void UpdateHPBar()
    {
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = (float)HP / MaxHP;
        }
    }
}
