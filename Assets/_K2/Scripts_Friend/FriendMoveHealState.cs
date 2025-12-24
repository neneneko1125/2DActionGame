using UnityEngine;
using System.Collections;

public class FriendMoveHealState : MonoBehaviour
{
    [Header("Playerを探す")]
    [SerializeField] private float _stopDistance = 1.0f;

    [Header("これ以上離れたらプレイヤーにワープ")]
    [SerializeField] private float _warpRange = 10.0f;

    private Transform _player;

    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _followSpeed;

    [SerializeField] private GameObject _friendSprite;
    private Vector3 _defaultScale;


    private Rigidbody2D _rb;
    private FriendBaseHeal _friendHeal;

    [SerializeField] private GameObject _wallChecker;

    private enum State { Follow, Chase }
    private State _current = State.Follow;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _friendHeal = GetComponent<FriendBaseHeal>();
        _rb = GetComponent<Rigidbody2D>();


        _defaultScale = _friendSprite.transform.localScale;

        //スピードを乱数調整する
        _chaseSpeed = Random.Range(_chaseSpeed * 0.75f, _chaseSpeed * 1.25f);
        _followSpeed = Random.Range(_followSpeed * 0.9f, _followSpeed * 1.1f);

    }

    private void Update()
    {
        ChangeState();
        Warp();
    }

    private void ChangeState()
    {

        if (_current == State.Follow && _player != null)
        {
            _current = State.Chase;
        }
        else if (_current == State.Chase && _player == null)
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
        float xDirectionToPlayer = Mathf.Sign(_player.position.x - transform.position.x);

        if (!_friendHeal.IsHeal)
            _friendSprite.transform.localScale = new Vector3(-xDirectionToPlayer * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        if (_friendHeal.IsHeal || xDistanceToPlayer < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = xDirectionToPlayer * _chaseSpeed;
        }
    }


    void Follow()
    {
        float xDistanceToPlayer = Mathf.Abs(_player.position.x - transform.position.x);
        float xDirectionToPlayer = Mathf.Sign(_player.position.x - transform.position.x);


        if (!_friendHeal.IsHeal)
            _friendSprite.transform.localScale = new Vector3(-xDirectionToPlayer * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        if (_friendHeal.IsHeal || xDistanceToPlayer < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = xDirectionToPlayer * _followSpeed;
        }

    }

   
    private void Warp()
    {
        float xDistance = Mathf.Abs(_player.position.x - transform.position.x);

        //味方を呼び寄せる　強制的に呼ぶときはQを押す
        if (xDistance > _warpRange || Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = new Vector3(_player.position.x - 1, _player.transform.position.y + 1, _player.transform.position.z);

        }
    }
}
