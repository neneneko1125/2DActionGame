using UnityEngine;

public class FriendATKObject : MonoBehaviour
{
    [SerializeField] private int _atkDefault = 1;

    private int _atk = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = collision.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                _atk = _atkDefault;
                SEManager.Instance.SEDamage();
                StartCoroutine(enemyHP.ReduceHP(_atk));
            }
        }
    }
}
