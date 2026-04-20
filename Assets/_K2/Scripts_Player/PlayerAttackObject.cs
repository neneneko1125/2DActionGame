using UnityEngine;

public class PlayerAttackObject : MonoBehaviour
{
    private PlayerInstanceData _instanceData;

    [Header("素の攻撃力")]
    [SerializeField] private int _attackDefault = 1;

    [Header("レベルが攻撃力に与える影響の大きさ")]
    [SerializeField] private float _attackScalingFactor = 1.5f;


    private int _attack;
    private float _lvMultiplier;



    /// <summary>
    /// PlayerInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(PlayerInstanceData data)
    {
        // 念のため、二重登録を防ぐために一度引いてから足す
        if (_instanceData != null)
        {
            _instanceData.OnChangeAttack -= UpdateAttack;
        }

        _instanceData = data;
        _instanceData.OnChangeAttack += UpdateAttack;
        UpdateAttack();
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

        _lvMultiplier = 1 + (_instanceData.currentLv - 1) * _attackScalingFactor;
        _attack = Mathf.RoundToInt(_attackDefault * _lvMultiplier + _instanceData.currentLv);
        _attack = _instanceData.GetBuffAttack(_attack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
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
