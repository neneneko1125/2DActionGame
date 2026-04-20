using UnityEngine;
using System.Collections;

public class EnemyAttackObject : MonoBehaviour
{
    [SerializeField] private int _attack = 1;


    [Header("Installationå^ÇÃçUåÇÇ»ÇÁÉ`ÉFÉbÉN")]
    [SerializeField] private bool _isInstallation = false;


    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_collider.enabled && _isInstallation)
        {
            StartCoroutine(ChangeHitjudgment());
        }
    }

    private IEnumerator ChangeHitjudgment()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
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
