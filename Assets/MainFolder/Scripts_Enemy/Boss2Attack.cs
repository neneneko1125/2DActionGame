using UnityEngine;
using System.Collections;

public class Boss2Attack : MonoBehaviour
{
    [Header("大きい竜巻")]
    [SerializeField] private GameObject _bossBulletPrefab;
    [SerializeField] private float _adjustPosY = 2;
    [Header("小さい竜巻")]
    [SerializeField] private GameObject _bossBulletPrefab2;
    [SerializeField] private float _adjustPosY2 = 2;

    [SerializeField] private float _intervalTime = 3;
    private bool _isInterval = false;

    private SpriteRenderer _sr;
    [SerializeField] private Animator _anim;


    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isInterval)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        StartCoroutine(AttackAnimation());

        int rnd = Random.Range(1, 10); //1～9の範囲でランダムな整数値が返る

        GameObject bullet;

        SEManager.Instance.SETornado();
        if (rnd < 5)
        {
            bullet = Instantiate(_bossBulletPrefab); //弾を生成

            if (_sr.flipX == true)
                bullet.transform.position = new Vector2(transform.position.x + 2, _adjustPosY);
            else
                bullet.transform.position = new Vector2(transform.position.x - 2, _adjustPosY);
        }
        else
        {
            bullet = Instantiate(_bossBulletPrefab2); //弾を生成

            if (_sr.flipX == true)
                bullet.transform.position = new Vector2(transform.position.x + 1, _adjustPosY2);
            else
                bullet.transform.position = new Vector2(transform.position.x - 1, _adjustPosY2);
        }



        StartCoroutine(Interval(_intervalTime)); //インターバル　コルーチンを利用
    }

    //インターバルの関数
    IEnumerator Interval(float intervalTime)
    {
        _isInterval = true;
        yield return new WaitForSeconds(intervalTime);
        _isInterval = false;
    }

    //点滅する関数
    IEnumerator AttackAnimation()
    {
        _anim.SetBool("Action", true);
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("Action", false);
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("Action", true);
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("Action", false);
    }

}
