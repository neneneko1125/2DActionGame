using UnityEngine;

public class FriendATKObject : MonoBehaviour
{
    private FriendInstanceData _instance;

    [SerializeField] private int _atkDefault = 1;
    [SerializeField] private float _scalingFactor = 1.5f;
    private int _atk;
    private float _lvMultiplier;

    [SerializeField, Header("近距離攻撃しないキャラはチェック")] private bool _noATKChar = false;

    /// <summary>
    /// FriendInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data)
    {
        _instance = data;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_noATKChar)
        {
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                SEManager.Instance.SEDamage();
                _lvMultiplier = 1 + (_instance.currentLv - 1) * _scalingFactor;
                _atk = Mathf.RoundToInt(_atkDefault * _lvMultiplier + _instance.currentLv);
                Debug.Log(_instance.baseData.FriendName + "の攻撃力：" + _atk);
                StartCoroutine(enemyHP.ReduceHP(_atk));
            }
        }
    }
}
