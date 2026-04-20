using UnityEngine;

/// <summary>
/// Enemyは壁まで歩くとそのまま左右反転して切り返す
/// Friend側はジャンプするので、FriendWallCheckerとは全く違う処理
/// </summary>
public class EnemyWallChecker : MonoBehaviour
{
    [SerializeField] private GameObject _enemySprite;
    private Vector3 _enemyDefaultScale;

    void Start()
    {
        _enemyDefaultScale = _enemySprite.transform.localScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Groundタグが検知したら
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
