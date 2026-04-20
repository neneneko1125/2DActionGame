using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]    //絶対必要なコンポーネントたち
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

    public bool IsGrounded { get; private set; }    //地面と接触しているときtrueになる

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
        //足元に地面のレイヤーがあるか検知する
        IsGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);

        //ジャンプボタンが押されたら
        if (_input.JumpInput)
        {
            Jump();     //ジャンプ処理
        }

        ChangeWalkAnimation();  //歩行アニメーション
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動と左右反転
    /// ダッシュ攻撃中とガード中のときに移動しないようにする
    /// </summary>
    private void Move()
    {
        //ダッシュ攻撃または下攻撃またはガードしてるとき
        if(_playerAttack.IsDownAttacking　|| _input.IsGuarding)
        {
            _rb.linearVelocityX = 0;
            return;
        }

        // スティックの入力を取得
        float dir;
        
        //PCで遊ぶ場合
        if (!InputChangeButton.IsPressedSystem)
        {
            dir = _input.MoveDirection;
        }
        //スマホの場合
        else
        {
            dir = _playerStick.nowDirection;
        }

        //ダッシュ攻撃していなければ(ダッシュの速度と競合しないように)
        if (!_playerAttack.IsDashAttacking)
        {
            _rb.linearVelocityX = dir * _moveSpeed;

            //移動中は
            if (dir != 0)
            {
                //向いてる方向に左右反転させる
                float scaleX = dir > 0 ? _defaultScale.x : -_defaultScale.x;
                transform.localScale = new Vector3(scaleX, _defaultScale.y, _defaultScale.z);
            }
        }

    }

    /// <summary>
    /// WalkアニメーションをON、OFFする
    /// </summary>
    private void ChangeWalkAnimation()
    {
        if (_anim != null)
        {
            //Walkアニメーション 移動方向が0じゃないときは歩行アニメーションON
            _anim.SetBool("Walk", _input.MoveDirection != 0);
        }
    }

    public void Jump()
    {
        //地面に接触しているかつ攻撃中じゃなければ
        if (IsGrounded && !_playerAttack.IsAttacking)
        {
            _rb.linearVelocityY = _jumpForce;   //ジャンプ
        }
    }

    private void OnEnable()
    {
        // イベントを購読
        GameEventManager.OnJumpCommand += Jump;
    }

    private void OnDisable()
    {
        // イベントを解除
        GameEventManager.OnJumpCommand -= Jump;
    }
}

/*
 * using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動速度")] private float _moveSpeed = 8.0f;
    private float _direction = 0f;  //移動方向

    [SerializeField, Header("ジャンプ力")] private float _jumpForce = 15.0f;
    [SerializeField, Header("足元の空のオブジェクト")] private Transform _groundChecker;
    [SerializeField, Header("地面のレイヤー")] private LayerMask _groundLayer;
    [SerializeField, Range(0, 1), Header("_groundCheckerの半径")] private float _checkRadius = 0.1f;
    private bool _isGrounded;   //地面に接していればtrue

    private Rigidbody2D _rb;
    private Animator _anim;

    private Vector3 _defaultScale;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        //最初のスケールを保存
        _defaultScale = transform.localScale;   
    }

    void Update()
    {
        Walk();
        Jump();
    }

    /// <summary>
    /// 歩行メソッド
    /// </summary>
    private void Walk()
    {
        //Dなら1、Aなら-1、何もしなければ0
        _direction = Input.GetAxisRaw("Horizontal");

        _rb.linearVelocityX = _direction * _moveSpeed;

        //進む方向が右ならば
        if (_direction > 0)
        {
            transform.localScale = _defaultScale;
            _anim.SetBool("Walk", true);
        }
        //進む方向が左ならば
        else if (_direction < 0)
        {
            //xのスケールの符号をマイナスにして左右反転
            transform.localScale = new Vector3(-_defaultScale.x, _defaultScale.y, _defaultScale.z);
            _anim.SetBool("Walk", true);
        }
        //止まっていれば
        else
        {
            _anim.SetBool("Walk", false);
        }
    }

    /// <summary>
    /// ジャンプメソッド
    /// </summary>
    private void Jump()
    {
        //OverlapCircle(円の位置,円の半径,検知するレイヤー);
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

}

 */