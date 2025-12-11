using UnityEngine;
using System.Collections;
public class FriendBaseATK : MonoBehaviour
{
    [SerializeField, Header("攻撃間隔時間")] protected float _atkIntervalTime = 1.0f;

    [SerializeField, Header("攻撃時間")] protected float _atkTime = 1.0f;

    [SerializeField, Header("攻撃アニメーション時間")] protected float _animTime = 0.5f;

    [SerializeField, Header("攻撃前のサインの時間")] protected float _atkTimebefore = 1.0f;

    [SerializeField, Header("敵との距離がこれ以下で攻撃処理をする")] protected float attackRange = 3.0f;

    [SerializeField, Header("攻撃判定オブジェクト")] protected Collider2D _atkCollider;

    [SerializeField] protected Animator _anim;

    [SerializeField] protected GameObject _atkSign;

    //攻撃待機から攻撃を終えるまでON　ループをうまく動かすためにこれが必要
    protected bool _atkInterval = false;

    //攻撃している最中にON
    public bool IsATK { get; private set; }

    public Transform Enemy;

    protected virtual void Start()
    {
        _atkCollider.enabled = false;
        StartCoroutine(ATKLoop()); // 攻撃ループを開始
    }

    protected virtual IEnumerator ATKLoop()
    {
        while (true)
        {
            if(Enemy == null)
            {
                yield return null;
                continue;
            }

            //敵と自身の距離を計算
            float distance = Vector2.Distance(transform.position, Enemy.position);

            //もし敵との距離が一定より離れていれば
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

                //
                yield return new WaitForSeconds(_atkIntervalTime);
                _atkInterval = false;
            }
        }
    }

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
}
