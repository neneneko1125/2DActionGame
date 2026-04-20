using UnityEngine;

public class Boss1AttackBullet : MonoBehaviour
{
    [SerializeField] private int _attack = 20;

    [Header("ザコ敵のPrefab")]
    [SerializeField] private GameObject _enemyPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Friend"))
        {
            PlayerHP playerHP = collision.GetComponent<PlayerHP>();
            FriendHP friendHP = collision.GetComponent<FriendHP>();

            if (playerHP != null)
            {
                StartCoroutine(playerHP.ReduceHP(_attack));
            }
            else if (friendHP != null)
            {
                StartCoroutine(friendHP.ReduceHP(_attack));
            }

            Destroy(gameObject);
        }

        //地面に当たった場合は
        if (collision.gameObject.CompareTag("Ground"))
        {
            //ザコ敵がその場で生成される
            Instantiate(_enemyPrefab, new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
            Destroy(gameObject);
        }
    }
}
