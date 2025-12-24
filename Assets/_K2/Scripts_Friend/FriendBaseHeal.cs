using UnityEngine;
using System.Collections;

public class FriendBaseHeal : MonoBehaviour
{
    [SerializeField, Header("回復間隔時間")] protected float _healIntervalTime = 1.0f;

    [SerializeField, Header("回復時間")] protected float _healTime = 1.0f;

    [SerializeField, Header("回復アニメーション時間")] protected float _animTime = 0.5f;

    [SerializeField, Header("回復前のサインの時間")] protected float _healTimebefore = 1.0f;

    [SerializeField, Header("プレイヤーとの距離がこれ以下で回復処理をする")] protected float healRange = 3.0f;

    [SerializeField, Header("回復のたま")] private GameObject _heal;
    [SerializeField, Header("弾速")] private float _bulletSpeed = 10f;
    [SerializeField, Header("発射位置の調整Y")] private float _adjustPosY = 0.5f;  

    [SerializeField, Header("Spriteの方をアタッチ")] protected Animator _anim;

    [SerializeField] private GameObject _friendSprite;

    [SerializeField, Header("回復前の[!]マーク")] protected GameObject _healSign;

    //攻撃待機から攻撃を終えるまでON　ループをうまく動かすためにこれが必要
    protected bool _healInterval = false;

    //攻撃している最中にON
    public bool IsHeal { get; private set; }

    private Transform _player;

    private FriendInstanceData _instance;

    /// <summary>
    /// FriendInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data)
    {
        _instance = data;
    }


    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(HealLoop()); //回復ループを開始
    }


    protected virtual IEnumerator HealLoop()
    {
        while (true)
        {
            if (_player == null)
            {
                Debug.Log("Playerがnull");
                yield return null;
                continue;
            }

            //プレイヤーと自身の距離を計算
            float distance = Vector2.Distance(transform.position, _player.position);

            //もしプレイヤーとの距離が一定より離れていれば
            if (distance > healRange)
            {
                //回復せずにループ継続
                yield return null;
                continue;
            }

            //回復インターバルがOFFならば
            if (!_healInterval)
            {
                //ここから実際に回復する処理

                _healInterval = true;
                IsHeal = true;

                //このメソッドが一周するまで待機
                yield return StartCoroutine(HealRoutine());

                //全体ループの待機時間
                yield return new WaitForSeconds(_healIntervalTime);
                _healInterval = false;
            }
        }
    }

    /// <summary>
    /// 回復魔法
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator HealRoutine()
    {
        //攻撃前のサイン
        _healSign.SetActive(true);
        yield return new WaitForSeconds(_healTimebefore);
        _healSign.SetActive(false);

        StartCoroutine(HealAnimation());

        Vector2 bulletDir = _friendSprite.transform.localScale.x < 0 ? Vector2.right : Vector2.left;    //弾を打つ方向
        Vector2 shootPos = (Vector2)transform.position + bulletDir;  //発射位置

        //ちょっと上から発射するための＋１
        shootPos = new Vector2(shootPos.x, shootPos.y + _adjustPosY);

        Debug.Log("回復魔法を発射します");
        GameObject heal = Instantiate(_heal, shootPos, transform.rotation); //弾を生成

        Rigidbody2D rb = heal.GetComponent<Rigidbody2D>();   //弾のRigidbody2Dを取得

        if(rb != null)
            rb.AddForce(bulletDir * _bulletSpeed, ForceMode2D.Impulse);

        FriendHealObject healObject = heal.GetComponent<FriendHealObject>();
        if(healObject != null)
            healObject.Initialize(_instance);
    }

    protected virtual IEnumerator HealAnimation()
    {
        _anim.SetBool("FriendHeal", true);
        yield return new WaitForSeconds(_animTime);
        _anim.SetBool("FriendHeal", false);

        //アニメーションが終わってはじめて回復処理を終了とする
        IsHeal = false;
    }
}
