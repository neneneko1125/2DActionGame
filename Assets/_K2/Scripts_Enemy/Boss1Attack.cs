using UnityEngine;
using System.Collections;

public class Boss1Attack : MonoBehaviour
{
    [SerializeField] private GameObject _bossBullet;

    [Header("弾に加える力の強さ")]
    [SerializeField] private float _bulletSpeedX = 2;
    [SerializeField] private float _bulletSpeedY = 2;
    [Header("弾の初期位置の微調整")]
    [SerializeField] private float _adjustPosX = 2;
    [SerializeField] private float _adjustPosY = 2;

    [Header("攻撃間隔")]
    [SerializeField] private float _intervalTime = 3;
    private bool _isInterval = false;


    private void Update()
    {
        if (!_isInterval)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 direction;  //方向
        Vector2 shootPos;   //どこから打つか

        int rnd = Random.Range(1, 10);　//1～9の範囲でランダムな整数値が返る

        //rndが5以下ならば
        if (rnd <= 5)
        {
            direction = Vector2.right; //方向を右に決定
            shootPos = new Vector2(transform.position.x + _adjustPosX, transform.position.y + _adjustPosY);   //打つ場所を決定
        }
        //rndが6以上ならば
        else
        {
            direction = Vector2.left;   //方向を左に決定
            shootPos = new Vector2(transform.position.x - _adjustPosX, transform.position.y + _adjustPosY);   //打つ場所を決定
        }

        //弾を生成
        GameObject bullet = Instantiate(_bossBullet, shootPos, transform.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //rndで弾の飛ぶスピードを乱数調整
        rb.AddForce(direction * _bulletSpeedX * (rnd % 3), ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * _bulletSpeedY * (rnd % 3), ForceMode2D.Impulse);

        StartCoroutine(Interval(_intervalTime)); //インターバル
    }

    //インターバルの関数
    IEnumerator Interval(float intervalTime)
    {
        _isInterval = true;
        yield return new WaitForSeconds(intervalTime);
        _isInterval = false;
    }
}
