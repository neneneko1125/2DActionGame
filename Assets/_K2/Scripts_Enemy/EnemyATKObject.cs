using UnityEngine;

public class EnemyATKObject : MonoBehaviour
{
    [SerializeField] private int _atk = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Friend"))
        {
            PlayerHP playerHP = collision.GetComponent<PlayerHP>();
            FriendHP friendHP = collision.GetComponent<FriendHP>();

            if (playerHP != null)
            {
                StartCoroutine(playerHP.ReduceHP(_atk));
            }
            else if (friendHP != null)
            {
                StartCoroutine(friendHP.ReduceHP(_atk));
            }


        }
    }
}
