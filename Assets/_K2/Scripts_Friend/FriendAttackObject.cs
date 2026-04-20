using UnityEngine;
using System.Collections;

/// <summary>
/// 近距離も遠距離もこれを使う
/// 近距離のためのインタフェース
/// 遠距離はFriendLongDistanceAttackから渡される
/// </summary>
public class FriendAttackObject : MonoBehaviour, ICharacterInitializer
{
    private FriendInstanceData _instanceData;

    [Header("素の攻撃力")]
    [SerializeField] private int _attackDefault = 1;

    [Header("レベルが攻撃力に与える影響の大きさ")]
    [SerializeField] private float _attackScalingFactor = 1.5f;

    [Header("Installation型の攻撃ならチェック")]
    [SerializeField] private bool _isInstallation = false;

    private int _attack;
    private float _lvMultiplier;

    private Collider2D _collider;

    /// <summary>
    /// PlayerInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data)
    {
        //二重登録を防ぐために一度引いてから足す
        if (_instanceData != null)
        {
            _instanceData.OnChangeAttack -= UpdateAttack;
        }

        _instanceData = data;
        _instanceData.OnChangeAttack += UpdateAttack;
        UpdateAttack();
    }

    void Start()
    {
        _collider = GetComponent<Collider2D>();

        // 最初に1回だけ呼ぶ（Updateには何も書かない）
        if (_isInstallation)
        {
            StartCoroutine(BlinkColliderRoutine());
        }
    }

    private IEnumerator BlinkColliderRoutine()
    {
        while (true) // ずっと繰り返す
        {
            // 1. 最初は True の状態（ここで待機時間を設定）
            _collider.enabled = true;
            yield return new WaitForSeconds(0.02f); // 例えば2秒間は当たり判定あり

            // 2. False にする
            _collider.enabled = false;
            yield return new WaitForSeconds(0.25f); // 0.5秒間だけ当たり判定消滅
        }
    }

    /// <summary>
    /// レベルアップやゲーム開始時に攻撃力を調整する
    /// </summary>
    public void UpdateAttack()
    {
        if (_instanceData == null)
        {
            return;
        }

        _lvMultiplier = 1 + (_instanceData.currentLv - 1) * _attackScalingFactor;       //レベルの影響
        _attack = Mathf.RoundToInt(_attackDefault * _lvMultiplier + _instanceData.currentLv);       //レベルの影響
        _attack = _instanceData.GetBuffAttack(_attack);     //バフ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                SEManager.Instance.SEDamage();

                int finalAttack = _attack;
                bool isCritical = false;
                //会心の一撃
                float rnd = Random.Range(0, 100);
                if (rnd <= _instanceData.criticalProbability)
                {
                    finalAttack *= 2;
                    isCritical = true;
                }

                StartCoroutine(enemyHP.ReduceHP(finalAttack, isCritical));
            }
        }
    }
    private void OnDisable()
    {
        // オブジェクトが非表示・破棄される時にイベント登録を解除する
        if (_instanceData != null)
        {
            _instanceData.OnChangeAttack -= UpdateAttack;
        }
    }
}
