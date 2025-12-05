using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;  //敵の移動速度
    [SerializeField] private float _jumpForce = 5f;  //ジャンプ力
    [SerializeField] private float _jumpIntervalTime = 1f;  //ジャンプ間隔
    private bool _jumpInterval = false;  //ジャンプ間隔に応じてtrue,falseを切り替える
   
    private float _direction = 1.0f;

    [SerializeField] private GameObject _enemySprite;

    private EnemyATK _enemyATK;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyATK = GetComponent<EnemyATK>();
    }


    void FixedUpdate()
    {
        EnemyMove();

        //ジャンプ
        if (_jumpInterval == false)
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
        if (_enemySprite.transform.localScale.x > 0)
        {
            _direction = -1.0f;
        }
        //反転してるときは
        else
        {
            _direction = 1.0f;
        }

        if (_enemyATK.isATK)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = _direction * _moveSpeed;
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

   
}
