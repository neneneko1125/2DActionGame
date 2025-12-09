using UnityEngine;

public class EnemyCliffChecker : MonoBehaviour
{
    [SerializeField] private GameObject _enemySprite;
    private Vector3 _enemyDefaultScale;

    void Start()
    {
        _enemyDefaultScale = _enemySprite.transform.localScale;
    }


    void Update()
    {

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //Groundタグが検知できなくなったら
        if (collision.gameObject.CompareTag("Ground"))
        {
            //反転していなければ
            if (_enemySprite.transform.localScale.x == _enemyDefaultScale.x)
            {
                //反転
                _enemySprite.transform.localScale = new Vector3(-_enemyDefaultScale.x, _enemyDefaultScale.y, _enemyDefaultScale.z);
            }
            //既に反転していたら
            else
            {
                //デフォルトに戻す
                _enemySprite.transform.localScale = _enemyDefaultScale;
            }

        }
    }
}
