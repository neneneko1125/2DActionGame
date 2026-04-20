using UnityEngine;
using System.Collections;
/// <summary>
/// 実際に攻撃する処理をまとめたクラス
/// 入力処理は全てPlayerInputクラスにまとめている
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public bool IsAttacking {  get; private set; }

    [Header("ダッシュ攻撃の速度")]
    [SerializeField] private float _dashSpeed = 5.0f;

    [Header("ダッシュ減速最小値")]
    [SerializeField] private float _dashMinSpeed = 1.0f;
    public bool IsDashAttacking { get; private set; }


    [Header("下攻撃の速度")]
    [SerializeField] private float _downSpeed = 5.0f;
    public bool IsDownAttacking { get; private set; }


    [Header("上攻撃の速度")]
    [SerializeField] private float _upForce = 20.0f;
    public bool IsUpAttacking { get; private set; }

    [Header("アニメーション時間(通常攻撃)")]
    [SerializeField] private float _animTime = 0.5f;

    [Header("アニメーション時間(ダッシュ攻撃)")]
    [SerializeField] private float _animTime_Dash = 0.5f;

    [Header("ダッシュ攻撃後のインターバル")]
    [SerializeField] private float _dashIntervalTime = 0.5f;

    [Header("アニメーション時間(下攻撃)")]
    [SerializeField] private float _animTime_Down = 1.0f;

    [Header("アニメーション時間(上攻撃)")]
    [SerializeField] private float _animTime_Up = 0.5f;

    [Header("攻撃オブジェクト")]
    [SerializeField] private Collider2D _attackCollider;
    [SerializeField] private Collider2D _attackCollider_Dash;
    [SerializeField] private Collider2D _attackCollider_Down;
    [SerializeField] private Collider2D _attackCollider_Up;

    private Animator _anim;
    private Rigidbody2D _rb;
    private PlayerInput _input;
    private PlayerMovement _player;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        _player = GetComponent<PlayerMovement>();
       
        //攻撃オブジェクトの当たり判定をOFF
        _attackCollider.enabled = false;
        _attackCollider_Dash.enabled = false;
        _attackCollider_Down.enabled = false;
        _attackCollider_Up.enabled = false;
    }

    void Update()
    {
        // 攻撃中でない、かつガード中でないなら
        if (!IsAttacking && !_input.IsGuarding)
        {
            HandleAttackExecution();
        }

        // ガードのアニメーション制御
        _anim.SetBool("Guard", _input.IsGuarding);
    }

    /// <summary>
    /// 
    /// </summary>
    private void HandleAttackExecution()
    {
        Vector2 dir;

        //Scaleがプラスなら右向き、マイナスなら左向き
        if (transform.localScale.x > 0)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }

        //AttackTypeの状態をみて実行する
        switch (_input.AttackType)
        {
            case AttackType.Normal:
                StartCoroutine(NormalAttack()); 
                break;
            case AttackType.Dash:
                StartCoroutine(DashAttack(dir)); 
                break;
            case AttackType.Down:
                StartCoroutine(DownAttack()); 
                break;
            case AttackType.Up:
                StartCoroutine(UpAttack());
                break;
        }

        //状態リセット
        _input.ClearAttackType();
    }

    /// <summary>
    /// 通常攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator NormalAttack()
    {
        IsAttacking = true;
        SEManager.Instance.SEAttack();
        _anim.SetBool("ATK", true);
        _attackCollider.enabled = true; 
        yield return new WaitForSeconds(_animTime);
        _attackCollider.enabled = false; 
        _anim.SetBool("ATK", false);
        IsAttacking = false;
    }


    /// <summary>
    /// ダッシュ攻撃
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private IEnumerator DashAttack(Vector2 dir)
    {
        SEManager.Instance.SEDashAttack();
        IsAttacking = true;
        IsDashAttacking = true;
        _anim.SetBool("DashATK", true);
        _attackCollider_Dash.enabled = true;

        //currentSpeedはどんどん減少する
        float currentSpeed = _dashSpeed;
        float timer = 0f;
        
        //タイマーで減速
        while (timer < _animTime_Dash)
        {
            _rb.linearVelocity = dir * currentSpeed;

            //Lerp(a, b, t);　aとbの間をtで補完する
            currentSpeed = Mathf.Lerp(_dashSpeed, _dashMinSpeed, timer / _animTime_Dash);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_dashIntervalTime);
        _attackCollider_Dash.enabled = false;
        _anim.SetBool("DashATK", false);
        IsDashAttacking = false;
        IsAttacking = false;
    }

    /// <summary>
    /// 下攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownAttack()
    {
        SEManager.Instance.SEDownAttack();
        IsAttacking = true;
        IsDownAttacking = true;
        _anim.SetBool("DownATK", true);
        _attackCollider_Down.enabled = true;
        _rb.AddForce(Vector2.down * _downSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_animTime_Down);
        _attackCollider_Down.enabled = false;
        _anim.SetBool("DownATK", false);
        IsDownAttacking = false;
        IsAttacking = false;
    }

    /// <summary>
    /// 上攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpAttack()
    {
        //地面に接触していないときは
        if (!_player.IsGrounded)
        {
            yield break;    //メソッドをぬける
        }   

        SEManager.Instance.SEAttack();
        IsAttacking = true;
        IsUpAttacking = true;
        _anim.SetBool("UpATK", true);
        _attackCollider_Up.enabled = true;
        _rb.linearVelocityY = _upForce;
        yield return new WaitForSeconds(_animTime_Up);
        _attackCollider_Up.enabled = false;
        _anim.SetBool("UpATK", false);
        IsUpAttacking = false;
        IsAttacking = false;
    }
}
