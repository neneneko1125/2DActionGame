using UnityEngine;

/// <summary>
/// MoveStateはEnemyとFriendをわける
/// </summary>
public abstract class FriendBaseMoveState : MonoBehaviour
{
    [Header("ターゲットを探す範囲")]
    [SerializeField] protected float _searchRadius = 5.0f;

    [Header("ターゲットレイヤー")]
    [SerializeField] protected LayerMask _targetLayer;

    [Header("Targetとの停止距離")]
    [SerializeField] protected float _stopDistanceToTarget = 1.0f;
    [Header("Playerとの停止距離")]
    [SerializeField] protected float _stopDistanceToPlayer = 2.0f;

    [Header("ターゲットを追いかける速度")]
    [SerializeField] protected float _chaseSpeed = 8.0f;
    [Header("プレイヤーについていく速度")]
    [SerializeField] protected float _followSpeed = 4.0f;

    [Header("Sprite")]
    [SerializeField] protected GameObject _friendSprite;

    protected Transform _player;
    protected Rigidbody2D _rb;
    protected CharacterBaseAction _action;
    protected Vector3 _defaultScale;

    private enum State 
    {
        FollowPlayer, //プレイヤーについていくモード
        ChaseTarget   //ターゲットを追いかけるモード
    }
    private State _currentState = State.FollowPlayer;

    //何かしらのアクション(攻撃やヒールなど)が発動中はtrue
    protected bool IsActing => _action != null && _action.IsActing;

    //子クラスによってターゲットが違う 他クラスで値の変更不可
    protected Transform _target;

    
    private Transform PlayerTransform
    {
        get
        {
            // PlayerManager(シングルトン)がいればそこから、いなければタグで探す
            if (PlayerManager.Instance != null)
            {
                return PlayerManager.Instance.transform;
            }

            return GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _action = GetComponent<CharacterBaseAction>();
        _defaultScale = _friendSprite.transform.localScale;
    }

    protected virtual void Update()
    {
        _player = PlayerTransform;

        ChangeState();

        //状態に合わせて行動する
        switch (_currentState)
        {
            case State.FollowPlayer:
                FollowPlayer();
                break;
            case State.ChaseTarget:
                ChaseTarget();
                break;
        }
    }

    protected void FollowPlayer()
    {
        if (_player == null)
        {
            return;
        }

        Move(_player, _stopDistanceToPlayer, _followSpeed);
    }

    protected void ChaseTarget()
    {
        if (_target == null)
        {
            return;
        }

        Move(_target, _stopDistanceToTarget, _chaseSpeed);
    }

    /// <summary>
    /// Stateの切り替え
    /// </summary>
    protected virtual void ChangeState()
    {
        //ターゲットがいる場合は
        if (_target != null)
        {   
            //ターゲット追跡状態
            _currentState = State.ChaseTarget;
        }
        //ターゲットがいないときは
        else
        {
            //プレイヤーについていく状態
            _currentState = State.FollowPlayer;
        }
    }

    /// <summary>
    /// 移動する　ターゲットが何者かによってスピードも変化する
    /// </summary>
    /// <param name="target"></param>
    /// <param name="stopDist"></param>
    /// <param name="speed"></param>
    protected virtual void Move(Transform target, float stopDist, float speed)
    {
        //距離
        float dist = Vector2.Distance(target.position, transform.position);
        //方向
        float dir = Mathf.Sign(target.position.x - transform.position.x);

        //アクション中でなければ
        if (!IsActing)
        {
            //ターゲットの方向によって左右反転する
            _friendSprite.transform.localScale = new Vector3(-dir * _defaultScale.x, _defaultScale.y, _defaultScale.z);
        }
            
        //アクション中または停止距離より短い距離なら
        if (IsActing || dist < stopDist)
        {
            _rb.linearVelocityX = 0;    //停止
        }
        else
        {
            _rb.linearVelocityX = dir * speed;
        }
    }
}