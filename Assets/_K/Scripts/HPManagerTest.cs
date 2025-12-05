using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPManagerTest : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    [SerializeField] public int maxhp = 100;
    public int hp;

    private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1.0f;
    [SerializeField] private float _blinkIntervalTime = 0.1f;

    [SerializeField] private Image _hpBarImage;

    private SpriteRenderer _sr;

    private void Awake()
    {
       _sr = _body.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        hp = maxhp;
    }


    void Update()
    {
        
    }

    public void ReduceHP(int damage)
    {
        if(_isInvincible)
        {
            return;
        }
        hp -= damage;

        if(hp <= 0)
        {
            Debug.Log("HPが0になりました");
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
        _isInvincible = true;    //無敵フラグON
 
        float timer = 0f;   //カウントアップタイマー

        //タイマーが決められた無敵時間より小さい値ならば
        while (timer < _invincibleTime)
        {
            _sr.enabled = !_sr.enabled;   //現在の透明度と入れ替え
            yield return new WaitForSeconds(_blinkIntervalTime);
            timer += _blinkIntervalTime;     //タイマー加算
        }

        _sr.enabled = true;      //最後は必ず透明にならないように

        _isInvincible = false;   //無敵フラグOFF
    }

    private void UpdateHPBar()
    {
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = (float)hp / maxhp;
        }
    }
}
