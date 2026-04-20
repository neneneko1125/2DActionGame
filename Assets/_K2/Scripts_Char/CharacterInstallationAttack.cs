using UnityEngine;
using System.Collections;

/// <summary>
/// 地面を検知してそこに魔法を設置する系の攻撃
/// 雷をふらすなど
/// </summary>
public class CharacterInstallationAttack : CharacterBaseAction
{
    [Header("魔法の弾")]
    [SerializeField] private GameObject _bullet;

    [Header("レイキャストの最大の長さ")]
    [SerializeField] private float _downMaxdistance = 5;

    [Header("高さを調整する")]
    [SerializeField] private float _adjustPosY = -0.5f;

    [Header("レイを高い所から出して、地面にうまらなくする")]
    [SerializeField] private float _ray_y = 10f;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private SpriteRenderer _characterSprite;

  
    protected override void Start()
    {
        base.Start();
    }

    protected override IEnumerator ExecuteAction()
    {
        if(Target == null)
        {
            yield break;
        }

        //光線を出す座標
        Vector2 origin = Target.position;

        //高いところから光線を出すために
        origin.y += _ray_y;

        yield return StartCoroutine(PlayActionAnim("Action"));

        //Scene画面で光線を可視化
        Debug.DrawRay(origin, Vector2.down * _downMaxdistance, Color.blue, 1f);


        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, _downMaxdistance, _groundLayer);

        if (hit.collider != null)
        {
            //生成場所
            Vector2 spawnPos = hit.point;

            //魔法の弾の高さ
            float bulletHeight = _bullet.GetComponent<SpriteRenderer>().bounds.size.y;

            //生成場所のY座標を魔法の高さ÷2+調整分とする
            spawnPos.y += bulletHeight / 2f + _adjustPosY; 

            //弾の生成
            GameObject bullet = Instantiate(_bullet, spawnPos, Quaternion.identity);

            //タイプによってSEを変える
            if (_magicTypeIsFire) SEManager.Instance.SEFire();
            else if (_magicTypeIsIce) SEManager.Instance.SEIce();
            else if (_magicTypeIsThunder) SEManager.Instance.SEThudener();
            else if (_magicTypeIsThunder2) SEManager.Instance.SEThudener2();

            ////InstanceDataを受け取りにいく
            OnBulletShot(bullet);
        }
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
