using UnityEngine;

public class Boss2AttackBullet : MonoBehaviour
{
    [SerializeField] private int _attack = 10;
    [SerializeField] private float _speed = 2f; // 移動速度

    private int _directionX; // 左右の方向（-1 or 1）


    void Start()
    {
        int rnd;
        rnd = Random.Range(0, 2);   //0か1

        if (rnd == 0)
        {
            _directionX = 1;  // 右へ
        }
        else
        {
            _directionX = -1; // 左へ
        }
    }

    void Update()
    {
        // X方向だけ移動（Yは固定）
        transform.Translate(Vector2.right * _directionX * _speed * Time.deltaTime);
    }

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

        }
    }

}

