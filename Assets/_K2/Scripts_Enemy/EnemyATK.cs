using UnityEngine;
using System.Collections;
public class EnemyATK : MonoBehaviour
{
    //攻撃間隔
    [SerializeField] private float _atkIntervalTime = 1.0f; 
    
    //攻撃時間
    [SerializeField] private float _atkTime = 1.0f;

    //プレイヤーとの距離がこれ以下だと攻撃処理をする
    [SerializeField] private float attackRange = 3.0f;

    //攻撃前のサインの時間
    [SerializeField] private float _atkTimebefore = 1.0f;

    //攻撃判定オブジェクト
    [SerializeField] private Collider2D _atkCollider;

    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject _atkSign;

    //攻撃待機から攻撃を終えるまでON
    private bool _atkInterval = false;

    //攻撃している最中にON
    public bool isATK = false;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _atkCollider.enabled = false;
        StartCoroutine(ATKLoop()); // 攻撃ループを開始
    }

    private IEnumerator ATKLoop()
    {
        //無限ループ
        while (true)
        {
            float dist = Vector2.Distance(transform.position, _player.position);
            if (dist > attackRange)
            {
                yield return null;
                continue; //攻撃ループを実行せずスキップ
            }

            if (!_atkInterval)
            {
                _atkInterval = true;

                _atkSign.SetActive(true);
                yield return new WaitForSeconds(_atkTimebefore); // "攻撃前の時間"
                _atkSign.SetActive(false);

                isATK = true;

                // 攻撃開始（コライダーON）
                _atkCollider.enabled = true;
                _anim.SetBool("EnemyATK", true);

                yield return new WaitForSeconds(_atkTime); // "当たり判定の出る時間"

                _anim.SetBool("EnemyATK", false);
                // 攻撃終了（コライダーOFF）
                _atkCollider.enabled = false;

                isATK = false;

                // 次の攻撃まで待つ
                yield return new WaitForSeconds(_atkIntervalTime);

                _atkInterval = false;
            }
        }
    }
}
