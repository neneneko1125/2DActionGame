using UnityEngine;

public class ATKObject : MonoBehaviour
{
    [SerializeField] private int _atk = 1;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = collision.gameObject.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                StartCoroutine(enemyHP.ReduceHP(_atk));
            }
        }
    }
}
