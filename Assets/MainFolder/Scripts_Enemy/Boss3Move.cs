using UnityEngine;
using System.Collections;

public class Boss3Move : MonoBehaviour
{
    private Transform _player;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpIntervalTime = 1f;
    private bool _isJumpInterval = false;
    private bool _isGrounded = false;

    [SerializeField] private Transform _enemyTransform;
    private Vector3 _defaultScale;
    private Rigidbody2D _rb;

    private float _moveDirection = 1f;

    private Transform PlayerTransform
    {
        get
        {
            if (PlayerManager.Instance != null) return PlayerManager.Instance.transform;
            return GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultScale = _enemyTransform.localScale;

        // 最初に向きをランダムに決めておく
        SetRandomDirection();
    }

    private void Update()
    {
        // プレイヤーを見るのではなく、進行方向を向くように変更
        UpdateFacingDirection();
        Move();

        if (!_isJumpInterval && _isGrounded)
        {
            StartCoroutine(Jump());
        }
    }

    void Move()
    {
        // ジャンプ中（空中）のみ、決まった方向に移動
        if (!_isGrounded)
        {
            _rb.linearVelocityX = _moveDirection * _moveSpeed;
        }
    }

    IEnumerator Jump()
    {
        _isJumpInterval = true;

        // ジャンプする瞬間に次の方向をランダムに決める
        SetRandomDirection();

        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_jumpIntervalTime);
        _isJumpInterval = false;
    }

    // 新設：ランダムに方向を決めるメソッド
    void SetRandomDirection()
    {
        // 0か1をランダムに取得し、-1(左)か1(右)に変換
        _moveDirection = (Random.Range(0, 2) == 0) ? -1f : 1f;
    }

    // 修正：移動方向に基づいて見た目（Scale）を変える
    void UpdateFacingDirection()
    {
        _player = PlayerTransform;

        //プレイヤーが左にいるときは
        if (_enemyTransform.position.x > _player.position.x)
        {
            // 左向き
            _enemyTransform.localScale = _defaultScale;
        }
        else
        {
            // 右向き
            _enemyTransform.localScale = new Vector3(-_defaultScale.x, _defaultScale.y, _defaultScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            _isGrounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }

    }
}