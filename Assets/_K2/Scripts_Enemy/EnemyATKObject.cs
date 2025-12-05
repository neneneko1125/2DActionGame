using UnityEngine;

public class EnemyATKObject : MonoBehaviour
{
    [SerializeField] private int _atk = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ATKê¨å˜");
            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();

            if (playerHP != null)
            {
                StartCoroutine(playerHP.ReduceHP(_atk));
            }
        }
    }
}
