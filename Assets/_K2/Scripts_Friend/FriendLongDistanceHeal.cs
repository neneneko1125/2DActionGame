using UnityEngine;
using System.Collections;

/// <summary>
/// 遠距離回復 Friendの遠距離キャラはInstanceデータが必要
/// </summary>
public class FriendLongDistanceHeal : CharacterBaseAction, ICharacterInitializer
{
    [SerializeField, Header("魔法のたま")] protected GameObject _bullet;
    [SerializeField, Header("弾速")] protected float _bulletSpeed = 10f;
    [SerializeField, Header("発射位置の調整Y")] protected float _adjustPosY = 0.5f;

    [SerializeField] protected GameObject _characterSprite;

    private FriendInstanceData _instanceData;


    /// <summary>
    /// FriendInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data) => _instanceData = data;


    protected override IEnumerator ExecuteAction()
    {
        yield return StartCoroutine(PlayActionAnim("Action"));

        Vector2 bulletDir = _characterSprite.transform.localScale.x < 0 ? Vector2.right : Vector2.left;    //弾を打つ方向
        Vector2 shootPos = (Vector2)transform.position + bulletDir;  //発射位置

        //ちょっと上から発射する
        shootPos = new Vector2(shootPos.x, shootPos.y + _adjustPosY);

        GameObject bullet = Instantiate(_bullet, shootPos, transform.rotation); //弾を生成

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();   //弾のRigidbody2Dを取得

        if (rb != null)
            rb.AddForce(bulletDir * _bulletSpeed, ForceMode2D.Impulse);

        OnBulletShot(bullet);
    }


    //FriendはEnemyと違ってinstanceデータからレベルを取得し回復力に反映させる必要がある
    protected void OnBulletShot(GameObject bullet)
    {
        FriendHealObject friendHealObject = bullet.GetComponent<FriendHealObject>();
        if (friendHealObject != null)
        {
            friendHealObject.Initialize(_instanceData);
        }
    }
}
