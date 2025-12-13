using UnityEngine;

public class EnemyState : MonoBehaviour
{
    //列挙型
    private enum State
    {
        Patrol,
        Chase
    }

    [Header("ターゲット")]
    [SerializeField] private Transform player;

    [Header("検知する距離")]
    [SerializeField] private float detectDistance = 3f;

    [Header("巡回速度")]
    [SerializeField] private float patrolSpeed = 1.0f;

    [Header("追跡速度")]
    [SerializeField] private float chaseSpeed = 3.0f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    //State型の変数に「Patrol」を代入 最初はパトロール状態からスタートする
    private State currentState = State.Patrol;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckDistance();
    }

    private void FixedUpdate()
    {
        UpdateState();
    }

    /// <summary>
    /// プレイヤーとの距離でステートを決定
    /// </summary>
    private void CheckDistance()
    {
        //プレイヤーとの距離を計算
        float distance = Vector2.Distance(transform.position, player.position);

        //検知可能距離より近づいたら
        if (distance <= detectDistance)
        {
            //追跡状態に変更
            currentState = State.Chase;
        }
        else
        {
            //巡回状態(通常状態)に変更
            currentState = State.Patrol;
        }
    }

    /// <summary>
    /// 現在のステートに応じた行動
    /// </summary>
    private void UpdateState()
    {
        switch (currentState)
        {
            //_currentStateの状態がPatrolのときは
            case State.Patrol:
                Patrol();
                break;

            //_currentStateの状態がChaseのときは
            case State.Chase:
                Chase();
                break;
        }
    }

    /// <summary>
    /// 巡回する
    /// </summary>
    private void Patrol()
    {
        //左に進む
        rb.linearVelocityX = -patrolSpeed;
        sr.color = Color.white;
    }

    /// <summary>
    /// プレイヤーを追いかける
    /// </summary>
    private void Chase()
    {
        //Sign:プラスかマイナスか0を返す
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocityX = direction * chaseSpeed;
        sr.color = Color.red;
    }
}
