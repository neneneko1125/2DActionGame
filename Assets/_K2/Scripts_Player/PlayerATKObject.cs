using UnityEngine;

public class PlayerATKObject : MonoBehaviour
{
    private PlayerInstanceData _instance;

    [SerializeField] private int _atkDefault = 1;
    [SerializeField] private float _scalingFactor = 1.5f;
    private int _atk;
    private float _lvMultiplier;

    /// <summary>
    /// PlayerInstaceData‚ğæ“¾
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(PlayerInstanceData data)
    {
        _instance = data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                _lvMultiplier = 1 + (_instance.currentLv - 1) * _scalingFactor;
                _atk = Mathf.RoundToInt(_atkDefault * _lvMultiplier + _instance.currentLv);
                Debug.Log(_instance.baseData.name + "‚ÌUŒ‚—ÍF" + _atk);
                StartCoroutine(enemyHP.ReduceHP(_atk));
            }
        }
    }
}
