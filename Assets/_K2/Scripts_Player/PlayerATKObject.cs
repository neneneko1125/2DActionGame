using UnityEngine;

public class ATKObject : MonoBehaviour
{
    [SerializeField] private int _atkDefault = 1;
    [SerializeField] private float _scalingFactor = 1.5f;
    private int _atk;
    private float _lvMultiplier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                _lvMultiplier = 1 + (PlayerLvEXP.Instance.PlayerLv - 1) * _scalingFactor;
                _atk = Mathf.RoundToInt(_atkDefault * _lvMultiplier + PlayerLvEXP.Instance.PlayerLv);
                StartCoroutine(enemyHP.ReduceHP(_atk));
            }
        }
    }
}
