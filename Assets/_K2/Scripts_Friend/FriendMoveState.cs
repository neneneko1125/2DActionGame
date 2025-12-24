using UnityEngine;
using System.Collections;

public class FriendMoveState : MonoBehaviour
{
    [Header("Playerを探す")]
    [SerializeField] private float _stopDistance = 1.0f;

    [Header("これ以上離れたらプレイヤーにワープ")]
    [SerializeField] private float _warpRange = 10.0f;

    [Header("Enemyを探す")]
    [SerializeField] private float _enemySearchRadius = 6f;
    [SerializeField] private LayerMask _enemyLayer;

    private Transform _player;
    private Transform _enemy;

    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _followSpeed;

    [SerializeField] private GameObject _friendSprite;
    private Vector3 _defaultScale;


    private Rigidbody2D _rb;
    private FriendBaseATK _friendATK;
    private FriendBaseATK _friendBaseATK;

    [SerializeField] private GameObject _wallChecker;

    private enum State { Follow, Chase }
    private State _current = State.Follow;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _friendATK = GetComponent<FriendBaseATK>();
        _rb = GetComponent<Rigidbody2D>();
        _friendBaseATK = GetComponent<FriendBaseATK>();


        _defaultScale = _friendSprite.transform.localScale;

        //スピードを乱数調整する
        _chaseSpeed = Random.Range(_chaseSpeed * 0.75f, _chaseSpeed * 1.25f);
        _followSpeed = Random.Range(_followSpeed * 0.9f, _followSpeed * 1.1f);

        StartCoroutine(UpdateEnemyRoutine());
    }

    private void Update()
    {
        ChangeState();
        Warp();
    }

    private void ChangeState()
    {
       
        if (_current == State.Follow && _enemy != null)
        {
            _current = State.Chase;
        }
        else if (_current == State.Chase && _enemy == null)
        {
            //一瞬アクティブを切り替えて、TriggerEnterで反応するように
            _wallChecker.SetActive(false);
            _wallChecker.SetActive(true);
            _current = State.Follow;
        }

        switch (_current)
        {
            case State.Follow:
                Follow();
                break;
            case State.Chase:
                Chase();
                break;
        }

    }

    void Chase()
    {
        float xDistanceToPlayer = Mathf.Abs(_player.position.x - transform.position.x);
        float xDirectionToEnemy = Mathf.Sign(_enemy.position.x - transform.position.x);

        if (!_friendATK.IsATK)
            _friendSprite.transform.localScale = new Vector3(-xDirectionToEnemy * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        if (_friendATK.IsATK || xDistanceToPlayer < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = xDirectionToEnemy * _chaseSpeed;
        }
    }


    void Follow()
    {
        float xDistanceToPlayer = Mathf.Abs(_player.position.x - transform.position.x);
        float xDirectionToPlayer = Mathf.Sign(_player.position.x - transform.position.x);


        if (!_friendATK.IsATK)
            _friendSprite.transform.localScale = new Vector3(-xDirectionToPlayer * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        if (_friendATK.IsATK || xDistanceToPlayer < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = xDirectionToPlayer * _followSpeed;
        }

    }

    /// <summary>
    /// 一番近い敵を探す
    /// </summary>
    /// <returns></returns>
    private Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _enemySearchRadius, _enemyLayer);

        if (hits.Length == 0) return null;

        float minDistance = float.MaxValue;

        Transform nearest = null;

        foreach (var h in hits)
        {
            float distance = Mathf.Abs(h.transform.position.x - transform.position.x);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = h.transform;
            }
        }

        return nearest;
    }


    IEnumerator UpdateEnemyRoutine()
    {
        while (true)
        {
            // すでにターゲットがいるが、生存している間は何もしない
            if (_enemy != null)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            // 新しいターゲットを探す
            Transform newEnemy = FindNearestEnemy();
            if (newEnemy != null)
            {
                _enemy = newEnemy;

                if(_friendBaseATK != null)
                    _friendBaseATK.enemy = newEnemy;

                // 敵の死亡イベントを購読
                var hp = _enemy.GetComponent<EnemyHP>();
                hp.OnDead += () => _enemy = null;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void Warp()
    {
        float xDistance = Mathf.Abs(_player.position.x - transform.position.x);

        //味方を呼び寄せる　強制的に呼ぶときはQを押す
        if(xDistance > _warpRange || Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = new Vector3(_player.position.x - 1, _player.transform.position.y + 1, _player.transform.position.z);

            //ターゲットをリセットする
            _enemy = null; 
        }
    }
}
