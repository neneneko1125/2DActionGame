using System.Collections;
using UnityEngine;

public class Boss1Move : MonoBehaviour
{
    public float _moveSpeed = 5f;  //敵の移動速度
    [SerializeField] private float _jumpForce = 5f;  //ジャンプ力
    [SerializeField] private float _jumpIntervalTime = 1f;  //ジャンプ間隔
    private bool _jumpInterval = false;  //ジャンプ間隔に応じてtrue,falseを切り替える

    [SerializeField] private Transform _enemyTransform;

    [SerializeField] private Collider2D _attackCollider;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _attackCollider.enabled = false;
    }


    void FixedUpdate()
    {
        EnemyMove();

        //ジャンプ
        if (!_jumpInterval)
        {
            StartCoroutine(EnemyJump());
        }
    }

    /// <summary>
    /// 敵の動きを管理
    /// </summary>
    void EnemyMove()
    {
        //反転してないときは
        if (_enemyTransform.localScale.x > 0)
        {
            _rb.linearVelocityX = -_moveSpeed;
        }
        //反転してるときは
        else
        {
            _rb.linearVelocityX = _moveSpeed;
        }
    }


    /// <summary>
    /// ジャンプメソッド
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyJump()
    {
        _jumpInterval = true; 　//ジャンプ中ON
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);   //上方向に力を加えてジャンプさせる
        yield return new WaitForSeconds(_jumpIntervalTime);
        _jumpInterval = false;  //ジャンプ中OFF
    }

    IEnumerator JumpAttack()
    {
        _attackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _attackCollider.enabled = false;
    }

    /// <summary>
    /// 地面に着地した瞬間攻撃判定が一瞬出る
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(JumpAttack());
        }
    }

}
