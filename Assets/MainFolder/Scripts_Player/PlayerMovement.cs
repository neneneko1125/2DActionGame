using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))] 
public class PlayerMovement : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float _moveSpeed = 8.0f;
    [Header("ジャンプ力")]
    [SerializeField] private float _jumpForce = 15.0f;

    [Header("プレイヤーの足元にある地面チェッカー")]
    [SerializeField] private Transform _groundChecker;
    [Header("地面のレイヤー")]
    [SerializeField] private LayerMask _groundLayer;
    [Header("地面を検知する半径")]
    [SerializeField, Range(0, 1)] private float _checkRadius = 0.1f;

    private Rigidbody2D _rb;
    private Animator _anim;
    private PlayerInput _input;
    private PlayerAttack _playerAttack;
    private PlayerStick _playerStick;
    private Vector3 _defaultScale;
    private bool _shouldJump = false;

    public bool IsGrounded { get; private set; }

    //スマホで遊ぶ場合はイベントでジャンプボタンが押されたことを検知する
    private void OnEnable() => GameEventManager.OnJumpCommand += PerformJump;
    private void OnDisable() => GameEventManager.OnJumpCommand -= PerformJump;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _input = GetComponent<PlayerInput>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerStick = GetComponent<PlayerStick>();
        _defaultScale = transform.localScale;
    }
    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);
        if (_input.IsJumpButtonPressed)
        {
            _shouldJump = true;
        }
        
        ChangeWalkAnimation();
    }
    private void FixedUpdate()
    {
        PerformMove();

        if (_shouldJump)
        {
            PerformJump();
            _shouldJump = false;
        }
    }
    /// <summary>
    /// 移動と左右反転
    /// ダッシュ攻撃中とガード中のときに移動しないようにする
    /// </summary>
    private void PerformMove()
    {
        //ダッシュ攻撃または下攻撃またはガードしてるとき
        if(_playerAttack.IsDownAttacking　|| _input.IsGuarding)
        {
            _rb.linearVelocityX = 0;
            return;
        }

        // 入力を取得
        float direction;
        
        //PCで遊ぶ場合
        if (!InputChangeButton.IsPressedSystem)
        {
            direction = _input.MoveDirection;
        }
        //スマホの場合
        else
        {
            direction = _playerStick.nowDirection;
        }

        //ダッシュ攻撃していなければ(ダッシュの速度と競合しないように)
        if (!_playerAttack.IsDashAttacking)
        {
            _rb.linearVelocityX = direction * _moveSpeed;

            //移動中は
            if (direction != 0)
            {
                //向いてる方向に左右反転させる
                float scale_X = direction > 0 ? _defaultScale.x : -_defaultScale.x;
                transform.localScale = new Vector3(scale_X, _defaultScale.y, _defaultScale.z);
            }
        }

    }
    private void ChangeWalkAnimation()
    {
        if (_anim != null)
        {
            _anim.SetBool("Walk", _input.MoveDirection != 0);
        }
    }
    public void PerformJump()
    {
        if (IsGrounded && !_playerAttack.IsAttacking)
        {
            _rb.linearVelocityY = _jumpForce;
        }
    }

   
}