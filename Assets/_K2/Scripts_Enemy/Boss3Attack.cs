using UnityEngine;
using System.Collections;

public class Boss3Attack : MonoBehaviour
{
    [SerializeField] private GameObject _bossBulletPrefab;
    [SerializeField] private float _bulletSpeed = 8f;
    [SerializeField] private float _adjustPosY = 0.5f;

    [SerializeField] private GameObject _bossBulletPrefab2;
    [SerializeField] private float _bulletSpeed2 = 8f;
    [SerializeField] private float _bulletSpeedUp2 = 8f;

    [SerializeField] private GameObject _bossBulletPrefab3;
    [SerializeField] private float _adjustPosY3 = 2;

    [SerializeField] private float _intervalTime = 3;
    private bool _isInterval = false;

    [SerializeField] private Transform _enemyTramsform;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        StartCoroutine(Interval(1)); //インターバル　コルーチンを利用
    }

    private void Update()
    {
        if (!_isInterval)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Vector2 dir;

        if (_enemyTramsform.localScale.x > 0)
        {
            dir = Vector2.left;
        }
        else
        {
            dir = Vector2.right;
        }

        StartCoroutine(Interval(_intervalTime)); //インターバル　コルーチンを利用

        int rnd = Random.Range(1, 13); // ※ 1～12の範囲でランダムな整数値が返る

        GameObject bullet, bullet2;

        //ストレート型
        if (rnd < 6)
        {
            _anim.SetBool("Action", true);
            bullet = Instantiate(_bossBulletPrefab); //弾を生成

            if (_enemyTramsform.localScale.x > 0)
            {
                bullet.transform.position = new Vector2(transform.position.x - 1, transform.position.y + _adjustPosY);
            }
            else
            {
                bullet.transform.position = new Vector2(transform.position.x + 1, transform.position.y + _adjustPosY);
            }
                
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();   //弾のRigidbody2Dを取得
            rb.AddForce(dir * _bulletSpeed, ForceMode2D.Impulse);   //AddForceのImpulseで瞬間的に力を加える

            SEManager.Instance.SEBeam();

            yield return new WaitForSeconds(0.5f);

            _anim.SetBool("Action", false);
        }
        //投擲型
        else if (rnd < 9)
        {
            _anim.SetBool("Action2", true);
            bullet = Instantiate(_bossBulletPrefab2); //弾を生成
           
            bullet.transform.position = new Vector2(transform.position.x, transform.position.y + 2);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();   //弾のRigidbody2Dを取得
            rb.AddForce(dir * _bulletSpeed2, ForceMode2D.Impulse);   //AddForceのImpulseで瞬間的に力を加える
            rb.AddForce(Vector2.up * _bulletSpeedUp2, ForceMode2D.Impulse);   //AddForceのImpulseで瞬間的に力を加える

            SEManager.Instance.SEBeam();

            yield return new WaitForSeconds(0.5f);

            _anim.SetBool("Action2", false);
        }
        //設置型
        else
        {
            _anim.SetBool("Action", true);
            bullet = Instantiate(_bossBulletPrefab3); //弾を生成
            bullet2 = Instantiate(_bossBulletPrefab3);

            if (_enemyTramsform.localScale.x > 0)
            {
                bullet.transform.position = new Vector2(transform.position.x - 3, _adjustPosY3);
                bullet2.transform.position = new Vector2(transform.position.x - 6, _adjustPosY3);
            }
            else
            {
                bullet.transform.position = new Vector2(transform.position.x + 3, _adjustPosY3);
                bullet2.transform.position = new Vector2(transform.position.x + 6, _adjustPosY3);
            }

            SEManager.Instance.SEBeam2();

            yield return new WaitForSeconds(0.5f);
            _anim.SetBool("Action", false);
        }




    }

    //インターバルの関数
    IEnumerator Interval(float intervalTime)
    {
        _isInterval = true;
        yield return new WaitForSeconds(intervalTime);
        _isInterval = false;
    }

}
