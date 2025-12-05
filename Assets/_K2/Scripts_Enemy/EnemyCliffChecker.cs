using UnityEngine;

public class EnemyCliffChecker : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private Vector3 _enemyDefaultScale;
    //  private SpriteRenderer _sr;
    void Start()
    {
        _enemyDefaultScale = _enemy.transform.localScale;
        //_sr = _enemy.GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (_enemy.transform.localScale.x == _enemyDefaultScale.x)
            {
                _enemy.transform.localScale = new Vector3(-_enemyDefaultScale.x, _enemyDefaultScale.y, _enemyDefaultScale.z);
            }
            else
            {
                _enemy.transform.localScale = _enemyDefaultScale;
            }

        }
    }
}
