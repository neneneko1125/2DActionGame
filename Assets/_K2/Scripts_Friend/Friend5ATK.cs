using UnityEngine;
using System.Collections;

/// <summary>
/// 遠距離攻撃
/// </summary>
public class Friend5ATK : FriendBaseATK
{
    [SerializeField, Header("魔法のたま")] private GameObject _bullet;
    [SerializeField, Header("弾速")] private float _bulletSpeed = 10f;
    [SerializeField, Header("発射位置の調整Y")] private float _adjustPosY = 0.5f;


    [SerializeField] private GameObject _friendSprite;
    private Rigidbody2D _rb;

    private FriendInstanceData _instance;

    /// <summary>
    /// FriendInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data)
    {
        _instance = data;
    }


    protected override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        base.Start(); // Base の Start を呼ぶ
    }

    /// <summary>
    /// ダッシュ攻撃
    /// 親クラスのメソッドをオーバーライド
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator AttackRoutine()
    {
        //攻撃前のサイン
        _atkSign.SetActive(true);
        yield return new WaitForSeconds(_atkTimebefore);
        _atkSign.SetActive(false);

        StartCoroutine(ATKAnimation());

        Vector2 bulletDir = _friendSprite.transform.localScale.x < 0 ? Vector2.right : Vector2.left;    //弾を打つ方向
        Vector2 shootPos = (Vector2)transform.position + bulletDir;  //発射位置

        //ちょっと上から発射するための＋１
        shootPos = new Vector2(shootPos.x, shootPos.y + _adjustPosY);

        Debug.Log("攻撃魔法を発射します");
        GameObject bullet = Instantiate(_bullet, shootPos, transform.rotation); //弾を生成

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();   //弾のRigidbody2Dを取得

        if (rb != null)
            rb.AddForce(bulletDir * _bulletSpeed, ForceMode2D.Impulse);

        //発射者のレベルなどを攻撃力に反映させるためにInstanceDataを取得
        FriendATKObject healObject = bullet.GetComponent<FriendATKObject>();
        if (healObject != null)
            healObject.Initialize(_instance);
    }
}
