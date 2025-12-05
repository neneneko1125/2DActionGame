using UnityEngine;
using System.Collections;
/*

public class EnemyMove : MonoBehaviour
{
    [Header("移動関連")]
    [SerializeField] private float _escapeDistance = 6f;
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _noiseStrength = 0.5f; // 揺らぎの強さ
    [SerializeField] private float _noiseSpeed = 1.5f;    // 揺らぎの変化スピード

    [Header("移動範囲制限")]
    [SerializeField] private Vector2 _minLimit = new Vector2(-5f, -3f);
    [SerializeField] private Vector2 _maxLimit = new Vector2(5f, 3f);

    [Header("Ray＆回避関連")]
    [SerializeField] private float _rayDistance = 5.0f;
    [SerializeField] private float _rayWidth = 1.0f;
    [SerializeField] private LayerMask _rayLayer;
    [SerializeField] private float _avoidSpeed = 20.0f;
    [SerializeField] private float _avoidTime = 0.5f;
    private bool _isAvoiding = false;

    [SerializeField] private Transform _player;
    private float _noiseTime;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private EnemyState_ _enemyState;

    private float _distanceToPlayer;

    // ダッシュ制御
    [SerializeField] private float _dashPower = 10f;
    [SerializeField] private float _dashCooldown = 2f;
    private float _dashTimer = 0f;

    // 画面端判定
    [SerializeField] private float _stageMinX = -8f;
    [SerializeField] private float _stageMaxX = 8f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _enemyState = GetComponent<EnemyState_>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float clampedX = Mathf.Clamp(transform.position.x, _minLimit.x, _maxLimit.x);
        float clampedY = Mathf.Clamp(transform.position.y, _minLimit.y, _maxLimit.y);
        transform.position = new Vector2(clampedX, clampedY);

        //プレイヤーとの距離を計算
        _distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (_isAvoiding == false)
        {
            Move();
            StartCoroutine(AvoidMagic());
        }
    }

    private IEnumerator AvoidMagic()
    {
        Vector2 direction = Vector2.left;

        if (_player.position.x < transform.position.x)
        {
            direction = Vector2.left;
            _sr.flipX = false;
        }
        else if (_player.position.x > transform.position.x)
        {
            direction = Vector2.right;
            _sr.flipX = true;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _rayDistance, _rayLayer);
        Debug.DrawRay(transform.position, direction * _rayDistance, Color.yellow);

        RaycastHit2D hit_up = Physics2D.Raycast(transform.position + Vector3.up * _rayWidth, direction, _rayDistance, _rayLayer);
        Debug.DrawRay(transform.position + Vector3.up * _rayWidth, direction * _rayDistance, Color.yellow);

        RaycastHit2D hit_down = Physics2D.Raycast(transform.position + Vector3.down * _rayWidth, direction, _rayDistance, _rayLayer);
        Debug.DrawRay(transform.position + Vector3.down * _rayWidth, direction * _rayDistance, Color.yellow);


        if (hit.collider != null || hit_up.collider != null || hit_down.collider != null)
        {
            Debug.Log("敵の回避");
            _isAvoiding = true;
            Vector2 avoidDir;
            float rnd;

            if (hit.collider != null)
            {
                rnd = Random.Range(0f, 1f);

                if (rnd <= 0.5f)
                {
                    avoidDir = Vector2.down;
                }
                else
                {
                    avoidDir = Vector2.up;
                }
            }
            else if (hit_up.collider != null)
            {
                //上で敵の魔法を検知したら下によける
                avoidDir = Vector2.down;
            }
            else
            {
                //下で敵の魔法を検知したら上によける
                avoidDir = Vector2.up;
            }


            _rb.linearVelocity = Vector2.zero;
            _rb.linearVelocity = avoidDir * _avoidSpeed;
            yield return new WaitForSeconds(_avoidTime);

            _isAvoiding = false;
        }
    }

    private void ATKMove()
    {
        //y方向にのみプレイヤーを追いかける
        float yDir = Mathf.Sign(_player.position.y - transform.position.y);
        Vector2 dir = new Vector2(0, yDir);

        // 揺らぎを時間とともに変化させる
        _noiseTime += Time.fixedDeltaTime * _noiseSpeed;

        /* PerlinNoise(パーリンノイズ)の返り値は0～1
         * *2f-1fによって、0～1を-1～1に変換
         * これによって、正の方向だけでなく負の方向にも揺らぐ
        
        float noise = Mathf.PerlinNoise(_noiseTime, 0f) * 2f - 1f; // -1～1

        //揺らぎベクトル
        Vector2 sway = new Vector2(0, noise * _noiseStrength);

        // 最後に基本方向ベクトルdirと揺らぎベクトルswayを足している
        Vector2 finalDir = (dir + sway).normalized;

        _rb.linearVelocity = finalDir * _moveSpeed;
    }


    private void EscapeMove()
    { 
        //ダッシュクールタイム更新
        _dashTimer -= Time.fixedDeltaTime;

        //画面端チェック
        if ((transform.position.x < _stageMinX || transform.position.x > _stageMaxX) && _dashTimer <= 0f)
        {
            Debug.Log("ダッシュ");
            //DashEscape();
            return; // 通常のEscape処理は行わない
        }

        //プレイヤーと逆方向へ
        float xDir = Mathf.Sign(transform.position.x - _player.position.x);  
        Vector2 dir = new Vector2(xDir, 0);

        _noiseTime += Time.fixedDeltaTime * _noiseSpeed;
        float noise = Mathf.PerlinNoise(_noiseTime, 0f) * 2f - 1f;
        Vector2 sway = new Vector2(0, noise * _noiseStrength);

        Vector2 finalDir = (dir + sway).normalized;
        _rb.linearVelocity = finalDir * _moveSpeed;
    }


    void Move()
    {
        // 現在のモードに合わせて移動
        switch (_enemyState.moveCurrentState)
        {
            case EnemyState_.MoveState.ATKMove:
                ATKMove();
                break;

            case EnemyState_.MoveState.EscapeMove:
                EscapeMove();
                break;
        }
    }
}

*/
