using UnityEngine;

public class EnemyMoveState : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5.0f;
    [SerializeField] private float _stopDistance = 1.0f;

    private Transform _player;

    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _patrolSpeed;

    [SerializeField] private GameObject _enemySprite;
    private Vector3 _defaultScale;
   

    private Rigidbody2D _rb;
    private EnemyBaseATK _enemyATK;

    [SerializeField] private GameObject _wallChecker;

    private enum State { Patrol, Chase }
    private State _current = State.Patrol;

    private void Start()
    {
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

    private void ChangeState()
    {
        float xDistance = Mathf.Abs(_player.position.x - transform.position.x);

        if (_current == State.Patrol && xDistance <= _detectionRange)
        {
            _current = State.Chase;
        }
        else if (_current == State.Chase && xDistance > _detectionRange)
        {
            //一瞬アクティブを切り替えて、TriggerEnterで反応するように
            _wallChecker.SetActive(false);
            _wallChecker.SetActive(true);
            _current = State.Patrol;
        }

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

    void Chase()
    {
        float xDistance = Mathf.Abs(_player.position.x - transform.position.x);
        float xDirection = Mathf.Sign(_player.position.x - transform.position.x);

        if(!_enemyATK.IsATK)
        _enemySprite.transform.localScale = new Vector3(-xDirection * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        if (_enemyATK.IsATK || xDistance < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = xDirection * _chaseSpeed;
        }

       
    }


    void Patrol()
    {  
        float direction;

        //反転してないときは
        if (_enemySprite.transform.localScale.x > 0)
        {
            direction = -1.0f;
        }
        //反転してるときは
        else
        {
            direction = 1.0f;
        }

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
