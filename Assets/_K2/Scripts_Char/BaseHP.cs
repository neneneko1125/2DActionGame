using UnityEngine;
using System.Collections;

/// <summary>
/// 子クラスはPlayerHPとFriendHP
/// EnemyHPとはInstanceDataがなく、LvやEXPの概念もないから継承しない
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseHP<T> : MonoBehaviour
    where T : BaseInstanceData
{
    protected T _instanceData;

    protected bool _isInvincible = false;

    [Header("無敵時間")]
    [SerializeField] protected float _invincibleTime = 1.0f;

    [Header("点滅時間")]
    [SerializeField] protected float _blinkIntervalTime = 0.1f;

    [SerializeField] protected SpriteRenderer _sr;

    protected abstract void Save();

    protected abstract void CheckDead();

    //インデックスも絡んでくるからここでは抽象メソッドに
    protected abstract void UpdateHPUI();
    protected abstract void UpdateLvEXPUI();


    public virtual IEnumerator ReduceHP(int damage)
    {
        if (_isInvincible)
        {
            yield break;
        }
        damage = _instanceData.GetBuffDamegeCut(damage);    //バフを受けに行く
        _instanceData.currentHP -= damage;

        Save();

        SEManager.Instance.SEDamage();
        DamageAndHealTextSpawn.Instance.SpawnDamageTextPlayerAndFriend(transform.position, damage);

        CheckDead();
        StartCoroutine(BlinkInvincible());
        UpdateHPUI();
    }

    public virtual void Heal(int healAmount)
    {
        if (_instanceData.currentHP <= 0)
        {
            return;
        }

        _instanceData.currentHP += healAmount;
        _instanceData.currentHP = Mathf.Clamp(_instanceData.currentHP, 0, _instanceData.MaxHP);

        Save();

        SEManager.Instance.SEHeal();
        DamageAndHealTextSpawn.Instance.SpawnHealText(transform.position, healAmount);

        UpdateHPUI();
    }

    protected IEnumerator BlinkInvincible()
    {
        _isInvincible = true;
        float timer = 0f;

        while (timer < _invincibleTime)
        {
            //現在不透明ならば
            if(_sr.color.a == 1)
            {
                _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0f);
            }
            //既に透明ならば
            else
            {
                _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1f);
            }
            

            yield return new WaitForSeconds(_blinkIntervalTime);
            timer += _blinkIntervalTime;
        }

        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1f);
        _isInvincible = false;
    }

    protected void OnDisable()
    {
        if (_instanceData != null)
        {
            _instanceData.OnChangeLvEXP -= UpdateHPLvEXPUI;
        }
    }

    protected void UpdateHPLvEXPUI()
    {
        UpdateHPUI();
        UpdateLvEXPUI();
    }
}
