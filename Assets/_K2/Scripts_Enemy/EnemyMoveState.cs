using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MoveStateはEnemyとFriendをわける
/// </summary>
public class EnemyMoveState : MonoBehaviour
{
    [Header("ターゲットを認識できる距離")]
    [SerializeField] private float _searchRadius = 5.0f;

    [Header("ターゲットと保つ距離")]
    [SerializeField] private float _stopDistance = 1.0f;

    [Header("ターゲットのレイヤー")]
    [SerializeField] private LayerMask _targetLayer;

    [Header("追いかけるスピード")]
    [SerializeField] private float _chaseSpeed;
    [Header("通常のスピード")]
    [SerializeField] private float _patrolSpeed;

    [Header("Sprite")]
    [SerializeField] private GameObject _enemySprite;
    private Vector3 _defaultScale;

    private Transform _target;

    private Rigidbody2D _rb;
    private CharacterBaseAction _action;

    [SerializeField] private Collider2D _wallChecker;

    private enum State
    {
        Patrol,     //プレイヤーも味方キャラも検知範囲にいないときの状態
        ChaseTarget       //プレイヤーまたは味方キャラを追いかける状態
    }
    private State _currentState = State.Patrol;

    private void Start()
    {
        _action = GetComponent<CharacterBaseAction>();
        _rb = GetComponent<Rigidbody2D>();

        if (_action != null)
        {
            //イベントAttackedが発動したらResetTargetを呼び出すようにセット
            _action.Acted += ResetTarget; 
        }

        _defaultScale = _enemySprite.transform.localScale;

        //敵のスピードを乱数調整する
        _chaseSpeed = Random.Range(_chaseSpeed * 0.75f, _chaseSpeed * 1.25f);
        _patrolSpeed = Random.Range(_patrolSpeed * 0.9f, _patrolSpeed * 1.1f);

        //ターゲットと保つ距離を乱数調整する
        _stopDistance = Random.Range(_stopDistance * 0.9f, _stopDistance * 1.1f);

        //InvokeRepeating(呼び出すメソッド, スタート時間, 呼び出す間隔);
        InvokeRepeating(nameof(SearchNearestCharacter), 0, 0.2f);
    }

    /// <summary>
    /// ターゲットをnullにする
    /// 攻撃するたびターゲットを選び直す
    /// </summary>
    private void ResetTarget() => _target = null;

    private void Update()
    {
        ChangeState();

        //状態に合わせて行動する
        switch (_currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.ChaseTarget:
                ChaseTarget();
                break;
        }
    }


    /// <summary>
    /// 指定範囲内で一番近いターゲットを探す
    /// </summary>
    private void SearchNearestCharacter()
    {
        //ターゲットの敵が既にいる場合はreturnしてメソッドをキャンセル
        if (_target != null)
        {
            return;
        }

        // 円の中の敵をすべて取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _searchRadius, _targetLayer);

        float minDistance = float.MaxValue;
        Transform nearest = null;

        foreach (var h in hits)
        {
            //自分とターゲットの距離を計算
            float dist = Vector2.Distance(transform.position, h.transform.position);

            //最小値を探し出す
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = h.transform;
            }
        }
        _target = nearest;

        //攻撃側にも今のターゲットを教えてあげる
        if (_action != null)
        {
            _action.Target = _target;
        }
    }


    /// <summary>
    /// Stateの変化の管理
    /// ターゲットがいるかいないかで変化する
    /// </summary>
    private void ChangeState()
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
            //一瞬当たり判定のONOFFを切り替えてTriggerEnterが反応するようにする
            if(_wallChecker != null)
            {
                _wallChecker.enabled = false;
                _wallChecker.enabled = true;
            }
            //巡回状態
            _currentState = State.Patrol;
        }
    }

    /// <summary>
    /// 追跡するときの処理
    /// </summary>
    void ChaseTarget()
    {
        if (_target == null)
        {
            return;
        }
        //ターゲットとの距離
        float dist = Mathf.Abs(_target.position.x - transform.position.x);

        //方向
        float dir = Mathf.Sign(_target.position.x - transform.position.x);

        //攻撃中でなければ
        if (!_action.IsActing)
        {
            //左右反転
            _enemySprite.transform.localScale = new Vector3(-dir * _defaultScale.x, _defaultScale.y, _defaultScale.z);
        }
       
        //攻撃中または停止距離より短ければ
        if (_action.IsActing || dist < _stopDistance)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = dir * _chaseSpeed;
        }
    }


    /// <summary>
    /// 通常状態の処理
    /// 巡回中の左右反転はEnemyWallCheckerクラスに任せている
    /// </summary>
    void Patrol()
    {
        float dir;  //方向

        //反転してないときは
        if (_enemySprite.transform.localScale.x > 0)
        {
            //左に移動(マイナス)
            dir = -1.0f;
        }
        //反転してるときは
        else
        {
            //右に移動
            dir = 1.0f;
        }

        _rb.linearVelocityX = dir * _patrolSpeed;
    }
}
