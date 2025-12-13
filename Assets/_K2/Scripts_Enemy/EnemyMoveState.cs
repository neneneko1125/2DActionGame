using UnityEngine;

public class EnemyMoveState : MonoBehaviour
{
    [SerializeField, Header("Playerを認識できる距離")] private float _detectionRange = 5.0f;
    [SerializeField, Header("Playerと保つ距離")] private float _stopDistance = 1.0f;

    private Transform _player;

    [SerializeField, Header("追いかけるスピード")] private float _chaseSpeed;
    [SerializeField, Header("通常のスピード")] private float _patrolSpeed;

    [SerializeField] private GameObject _enemySprite;
    private Vector3 _defaultScale;
   

    private Rigidbody2D _rb;
    private EnemyBaseATK _enemyATK;

    [SerializeField] private GameObject _wallChecker;

    private enum State { Patrol, Chase }
    private State _current = State.Patrol;

    private void Start()
    {
        //Playerタグを探索
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _enemyATK = GetComponent<EnemyBaseATK>();
        _rb = GetComponent<Rigidbody2D>();

        _defaultScale = _enemySprite.transform.localScale;

        //敵のスピードを乱数調整する
        _chaseSpeed = Random.Range(_chaseSpeed * 0.75f, _chaseSpeed * 1.25f);
        _patrolSpeed = Random.Range(_patrolSpeed * 0.9f, _patrolSpeed * 1.1f);
    }

    private void Update()
    {
        ChangeState();
    }

    /// <summary>
    /// Stateの変化の管理
    /// </summary>
    private void ChangeState()
    {
        float xDistance;

        if (_player != null)
            xDistance = Mathf.Abs(_player.position.x - transform.position.x);
        else
            xDistance = 0;

        //通常状態かつ検知可能距離より距離が近ければ
        if (_current == State.Patrol && xDistance <= _detectionRange)
        {
            _current = State.Chase;
        }
        //追跡状態かつ検知可能距離より距離が遠ければ
        else if (_current == State.Chase && xDistance > _detectionRange)
        {
            //一瞬アクティブを切り替えて、TriggerEnterで反応するように
            _wallChecker.SetActive(false);
            _wallChecker.SetActive(true);

            _current = State.Patrol;
        }

        //swich文で切り替え
        switch (_current)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }

    }

    /// <summary>
    /// 追跡するときの処理
    /// </summary>
    void Chase()
    {
        float xDistance = Mathf.Abs(_player.position.x - transform.position.x);
        float xDirection = Mathf.Sign(_player.position.x - transform.position.x);

        //攻撃中でなければ
        if (!_enemyATK.IsATK)
        {
            _enemySprite.transform.localScale = new Vector3(-xDirection * _defaultScale.x, _defaultScale.y, _defaultScale.z);
        }
       
        //攻撃中または停止距離より短ければ
        if (_enemyATK.IsATK || xDistance < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = xDirection * _chaseSpeed;
        }

       
    }

    /// <summary>
    /// 通常状態の処理
    /// </summary>
    void Patrol()
    {  
        float direction;

        //反転してないときは
        if (_enemySprite.transform.localScale.x > 0)
        {
            //左に移動(マイナス)
            direction = -1.0f;
        }
        //反転してるときは
        else
        {
            //右に移動
            direction = 1.0f;
        }

        //攻撃中ならば
        if (_enemyATK.IsATK)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = direction * _patrolSpeed;
        }
        
    }


}
