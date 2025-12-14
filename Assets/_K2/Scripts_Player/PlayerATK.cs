using UnityEngine;
using System.Collections;

/// <summary>
/// ガードもここで管理
/// </summary>
public class PlayerATK : MonoBehaviour
{
    [SerializeField, Header("ダッシュ攻撃の速度")] private float _dashSpeed = 5.0f;
    [SerializeField, Header("ダッシュ減速最小値")] private float _dashMinSpeed = 1.0f;

    public bool IsDashing { get; private set; }

    [SerializeField, Header("下攻撃の速度")] private float _downSpeed = 5.0f;

    public bool IsDowning { get; private set; }

    [SerializeField, Header("アニメーション時間(通常攻撃)")] private float _animTime = 0.5f;
    [SerializeField, Header("アニメーション時間(ダッシュ攻撃)")] private float _animTime_Dash = 0.5f;
    [SerializeField, Header("ダッシュ攻撃後のインターバル")] private float _dashAfterTime = 0.5f;
    [SerializeField, Header("アニメーション時間(下攻撃)")] private float _animTime_Down = 1.0f;

    [Header("攻撃オブジェクト")]
    [SerializeField] private Collider2D _atkCollider;
    [SerializeField] private Collider2D _atkCollider_Dash;
    [SerializeField] private Collider2D _atkCollider_Down;

    private Animator _anim;
    private Rigidbody2D _rb;

    public bool IsGuard { get; private set; }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
       
        //攻撃オブジェクトの当たり判定をOFF
        _atkCollider.enabled = false;
        _atkCollider_Dash.enabled = false;
        _atkCollider_Down.enabled = false;
    }


    void Update()
    {
        ATK();
    }

    /// <summary>
    /// どのボタンを押したかによって攻撃が変化する
    /// </summary>
    private void ATK()
    {
        //攻撃オブジェクトたちの当たり判定がONじゃない かつ ガード中じゃないなら
        if (!_atkCollider.enabled && !_atkCollider_Dash.enabled && !_atkCollider_Down.enabled && !IsGuard)
        {
            //左クリックすれば
            if (Input.GetMouseButtonDown(0))
            {
                //DとSを押す かつ 反転していなければ
                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && transform.localScale.x > 0)
                {
                    //右にダッシュ攻撃
                    StartCoroutine(DashATK(Vector2.right));
                }
                //AとSを押す かつ 反転していれば
                else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && transform.localScale.x < 0)
                {
                    //左にダッシュ攻撃
                    StartCoroutine(DashATK(Vector2.left));
                }
                //Sを押していれば
                else if (Input.GetKey(KeyCode.S))
                {
                    //下攻撃
                    StartCoroutine(DownATK());
                }
                //左クリックだけなら
                else
                {
                    //通常攻撃
                    StartCoroutine(NomalATK());
                }
            }

            //右クリックを押したら
            if (Input.GetMouseButton(1))
            {
                //ガード
                IsGuard = true;
                _anim.SetBool("Guard", true);
            }
        }

        //右クリックを離したら(ガード解除はいつでも可能)
        if (Input.GetMouseButtonUp(1))
        {
            IsGuard = false;
            _anim.SetBool("Guard", false);
        }

    }

    /// <summary>
    /// 通常攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator NomalATK()
    {
        SEManager.Instance.SEATK();
        _anim.SetBool("ATK", true);
        _atkCollider.enabled = true;    //攻撃オブジェクトの当たり判定ON
        yield return new WaitForSeconds(_animTime);
        _atkCollider.enabled = false;   //攻撃オブジェクトの当たり判定OFF
        _anim.SetBool("ATK", false);
    }


    /// <summary>
    /// ダッシュ攻撃
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private IEnumerator DashATK(Vector2 dir)
    {
        SEManager.Instance.SEDashATK();

        IsDashing = true;   //ダッシュON
        _anim.SetBool("DashATK", true);
        _atkCollider_Dash.enabled = true;   //攻撃オブジェクトの当たり判定ON

        //currentSpeedはどんどん減少する
        float currentSpeed = _dashSpeed;
        float timer = 0f;
        
        //タイマーで減速
        while (timer < _animTime_Dash)
        {
            _rb.linearVelocity = dir * currentSpeed;
            currentSpeed = Mathf.Lerp(_dashSpeed, _dashMinSpeed, timer / _animTime_Dash);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_dashAfterTime);
        _atkCollider_Dash.enabled = false;  //攻撃オブジェクトの当たり判定OFF
        _anim.SetBool("DashATK", false);
        IsDashing = false;
    }

    /// <summary>
    /// 下攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownATK()
    {
        SEManager.Instance.SEDownATK();

        IsDowning = true;
        _anim.SetBool("DownATK", true);
        _atkCollider_Down.enabled = true;
        _rb.AddForce(Vector2.down * _downSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_animTime_Down);
        _atkCollider_Down.enabled = false;
        _anim.SetBool("DownATK", false);
        IsDowning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && IsDowning)
        {
            _atkCollider_Down.enabled = false;
            _anim.SetBool("DownATK", false);
        }
    }
}
