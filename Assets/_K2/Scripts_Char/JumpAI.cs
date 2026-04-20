using UnityEngine;
using System.Collections;

public class JumpAI : MonoBehaviour
{
    [Header("ジャンプ力")]
    [SerializeField] protected float _jumpForce = 20.0f;

    [Header("ジャンプ許可関連")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField, Range(0, 1)] private float _checkRadius = 0.1f;

    [Header("壁,天井検知関連")]
    [SerializeField] private Collider2D _wallCheckerCollider;
    [SerializeField] private FriendWallChecker _wallChecker;
    [SerializeField] private float _wallcheckerIntervalTime = 0.5f;
    [SerializeField] private FriendCeilingChecker _ceilingChecker;


    private bool _isGrounded = false;
    private bool _isJumpProhibited = false;

    protected Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WallCheckerChange());
    }

    private void Update()
    {
        //地面の検知
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);
    }

    private void Jump()
    {
        //地面に接触している かつ ジャンプが禁止されていないなら
        if (_isGrounded && !_isJumpProhibited)
        {
            //ジャンプ
            _rb.linearVelocityY = _jumpForce;
        }
        else
        {
            Debug.Log("JUMPできない！");
        }
    }

    /// <summary>
    /// FriendCilingChecker側のイベントで呼び出される
    /// </summary>
    /// <param name="isCeiling"></param>
    private void SetJumpLimitation(bool isCeiling)
    {
        //天井があるときジャンプ禁止 これがないとずっとピョンピョンしちゃう
        _isJumpProhibited = isCeiling;
    }

    /// <summary>
    /// WallCheckerの当たり判定をONOFF
    /// 壁で詰まってしまわないようにするため
    /// </summary>
    /// <returns></returns>
    private IEnumerator WallCheckerChange()
    {
        while (true)
        {
            _wallCheckerCollider.enabled = !_wallCheckerCollider.enabled;
            yield return new WaitForSeconds(_wallcheckerIntervalTime);
        }

    }


    private void OnEnable()
    {
        _wallChecker.OnWallChecker += Jump;
        _ceilingChecker.OnHitCeiling += SetJumpLimitation;
    }

    private void OnDisable()
    {
        _wallChecker.OnWallChecker -= Jump;
        _ceilingChecker.OnHitCeiling -= SetJumpLimitation;
    }


}
