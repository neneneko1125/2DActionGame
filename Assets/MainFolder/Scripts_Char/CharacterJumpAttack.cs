using UnityEngine;
using System.Collections;

/// <summary>
/// ジャンプ攻撃
/// </summary>
/// 

public class CharacterJumpAttack : CharacterBaseAction
{
    [Header("ジャンプ許可関連")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField, Range(0, 1)] private float _checkRadius = 0.1f;

    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private GameObject _characterSprite;

    private bool _isGrounded = false;

    private Rigidbody2D _rb;

    [Header("攻撃用オブジェクトのコライダー")]
    [SerializeField] private Collider2D _attackCollider;

    protected override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _attackCollider.enabled = false;
        base.Start(); // Base の Start を呼ぶ
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);
    }

    /// <summary>
    /// ジャンプ攻撃 前進しながらジャンプも可能
    /// 親クラスのメソッドをオーバーライド
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ExecuteAction()
    {
      
        //進行方向を決める
        Vector2 dashDir = _characterSprite.transform.localScale.x < 0 ? Vector2.right : Vector2.left;

        _rb.linearVelocityY = _jumpForce;
        yield return new WaitForSeconds(0.1f);  //攻撃判定は着地した一瞬

        float timer = 0f;
        while (!_isGrounded)
        {
            _rb.linearVelocity = new Vector2(dashDir.x * _dashSpeed, _rb.linearVelocity.y);
            timer += Time.deltaTime;
            yield return null;
        }

        // ダッシュジャンプ終了後は速度をリセット
        _rb.linearVelocityX = 0;
        _rb.linearVelocityY = 0;

        _attackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);  //攻撃判定は着地した一瞬
        _attackCollider.enabled = false;

    }

}
