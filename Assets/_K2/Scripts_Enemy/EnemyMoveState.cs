using UnityEngine;

public class EnemyMoveState : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5.0f;
    [SerializeField] private float _stopDistance = 1.0f;
    private Transform _player;

    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _patrolSpeed;

    [SerializeField] private GameObject _enemySprite;
    private float _direction = 1.0f;

    private Rigidbody2D _rb;
    private EnemyATK _enemyATK;

    private enum State { Patrol, Chase }
    private State _current = State.Patrol;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyATK = GetComponent<EnemyATK>();
        _rb = GetComponent<Rigidbody2D>();
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

        _enemySprite.transform.localScale = new Vector3(-xDirection, 1, 1);

        if (_enemyATK.isATK || xDistance < _stopDistance)
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
        //”½“]‚µ‚Ä‚È‚¢‚Æ‚«‚Í
        if (_enemySprite.transform.localScale.x > 0)
        {
            _direction = -1.0f;
        }
        //”½“]‚µ‚Ä‚é‚Æ‚«‚Í
        else
        {
            _direction = 1.0f;
        }

        _rb.linearVelocityX = _direction * _patrolSpeed;

       

    }


}
