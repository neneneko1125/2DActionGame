using UnityEngine;
using System.Collections;

/// <summary>
/// 遠距離攻撃キャラ
/// </summary>
public class CharacterLongDistanceAttack : CharacterBaseAction
{
    [Header("魔法のたま")]
    [SerializeField] protected GameObject _bullet;

    [Header("弾速")]
    [SerializeField] protected float _bulletSpeed = 10f;

    [Header("発射位置(Y座標)の調整")]
    [SerializeField] protected float _adjustPosY = 0.5f;

    [Header("Sprite")]
    [SerializeField] protected Transform _characterSprite;

    
    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ExecuteAction()
    {
        yield return StartCoroutine(PlayActionAnim("Action"));

        //弾を打つ方向
        Vector2 bulletDir;     

        //左右反転していなければ
        if(_characterSprite.localScale.x > 0)
        {
            bulletDir = Vector2.left;
        }
        //左右反転していれば
        else
        {
            bulletDir = Vector2.right;
        }

        Vector2 shootPos = (Vector2)transform.position + bulletDir;  //発射位置

        //ちょっと上から発射する
        shootPos = new Vector2(shootPos.x, shootPos.y + _adjustPosY);

        GameObject bullet = Instantiate(_bullet, shootPos, transform.rotation); //弾を生成

        if(bulletDir == Vector2.right)
        {
            //反転
            bullet.transform.localScale = new Vector3(-bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();   //弾のRigidbody2Dを取得

        if (rb != null)
        {
            rb.AddForce(bulletDir * _bulletSpeed, ForceMode2D.Impulse);
        }

        if (_magicTypeIsFire) SEManager.Instance.SEFire();
        else if (_magicTypeIsIce) SEManager.Instance.SEIce();
        else if (_magicTypeIsThunder) SEManager.Instance.SEThudener();
        else if(_magicTypeIsThunder2) SEManager.Instance.SEThudener2();

        OnBulletShot(bullet);
    }

    /// <summary>
    /// Friend側ではここにInstanceデータからレベルを取得して攻撃力に反映させる処理を書く
    /// 敵側は抽象メソッドのまま何もしなくて大丈夫
    /// </summary>
    /// <param name="bullet"></param>
    protected virtual void OnBulletShot(GameObject bullet)
    {

    }
}
