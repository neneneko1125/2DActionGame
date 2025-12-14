using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBaseATK : MonoBehaviour
{
    [SerializeField, Header("攻撃間隔時間")] protected float _atkIntervalTime = 1.0f; 
    
    [SerializeField, Header("攻撃時間")] protected float _atkTime = 1.0f;

    [SerializeField, Header("攻撃アニメーション時間")] protected float _animTime = 0.5f;

    [SerializeField, Header("攻撃前のサインの時間")] protected float _atkTimebefore = 1.0f;

    [SerializeField, Header("プレイヤーとの距離がこれ以下で攻撃処理をする")] protected float attackRange = 3.0f;

    [SerializeField, Header("攻撃判定オブジェクト")] protected Collider2D _atkCollider;

    [SerializeField] protected Animator _anim;

    [SerializeField] protected GameObject _atkSign;

    //攻撃待機から攻撃を終えるまでON　ループをうまく動かすためにこれが必要
    protected bool _atkInterval = false;

    //攻撃している最中にON
    public bool IsATK { get; private set; }

    protected Transform _player;
    protected List<Transform> _targets = new List<Transform>();

    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        //プレイヤーをリストに追加
        _targets.Add(_player);

        //Friendタグのオブジェクトをみんな集める
        foreach (var f in GameObject.FindGameObjectsWithTag("Friend"))
        {
            //_targetリストに加える
            _targets.Add(f.transform);
        }

        _atkCollider.enabled = false;
        StartCoroutine(ATKLoop()); // 攻撃ループを開始
    }

    protected virtual IEnumerator ATKLoop()
    {
        while (true)
        {
            Transform target = GetNearestTarget();

            if (target == null) yield return null;

            float distance = Vector2.Distance(transform.position, target.position);


            //もしターゲットとの距離が一定より離れていれば
            if (distance > attackRange)
            {
                //攻撃せずにループ継続
                yield return null;
                continue;
            }

            //攻撃インターバルがOFFならば
            if (!_atkInterval)
            {
                //ここから実際に攻撃する処理

                _atkInterval = true;
                IsATK = true;

                //このメソッドが一周するまで待機
                yield return StartCoroutine(AttackRoutine());

                //攻撃間隔分待機
                yield return new WaitForSeconds(_atkIntervalTime);

                _atkInterval = false;
            }
        }
    }

    /// <summary>
    /// virtual:このメソッドは子クラスでoverrideすることで上書きが可能
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator AttackRoutine()
    {
        //攻撃前のサイン
        _atkSign.SetActive(true);
        yield return new WaitForSeconds(_atkTimebefore);
        _atkSign.SetActive(false);

        //攻撃判定ON    
        _atkCollider.enabled = true;

        //アニメーション再生
        StartCoroutine(ATKAnimation());

        //攻撃時間
        yield return new WaitForSeconds(_atkTime);

        //攻撃判定OFF
        _atkCollider.enabled = false;
    }

    protected virtual IEnumerator ATKAnimation()
    {
        _anim.SetBool("EnemyATK", true);
        yield return new WaitForSeconds(_animTime);
        _anim.SetBool("EnemyATK", false);

        //アニメーションが終わってはじめて攻撃処理を終了とする
        IsATK = false;
    }

    /// <summary>
    /// 一番近いターゲットを返す
    /// targetsにはStartメソッドで既に収納している
    /// </summary>
    /// <returns></returns>
    private Transform GetNearestTarget()
    {
        Transform nearest = null;
        float minDist = Mathf.Infinity;
        Vector2 pos = transform.position;

        foreach (var t in _targets)
        {
            if (t == null) continue; // 死んだ場合

            float dist = Vector2.Distance(pos, t.position);

            //今までの最小距離より短い距離なら
            if (dist < minDist)
            {
                //最小を更新
                minDist = dist;
                nearest = t;
            }
        }

        return nearest;
    }

}
