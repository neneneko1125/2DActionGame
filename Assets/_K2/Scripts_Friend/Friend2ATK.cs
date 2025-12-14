using UnityEngine;
using System.Collections;

/// <summary>
/// ダッシュ攻撃
/// </summary>
public class Friend2ATK : FriendBaseATK
{
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private GameObject _friendSprite;

    private Rigidbody2D _rb;

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

        //親クラスにあるメソッドを呼び出している
        StartCoroutine(ATKAnimation());

        // ダッシュ方向を決める
        Vector2 dashDir = _friendSprite.transform.localScale.x < 0 ? Vector2.right : Vector2.left;

        _atkCollider.enabled = true;

        float timer = 0f;
        //指定時間中なら
        while (timer < _atkTime)
        {
            //速度を変更する
            _rb.linearVelocity = new Vector2(dashDir.x * _dashSpeed, _rb.linearVelocity.y);
            timer += Time.deltaTime;
            yield return null;
        }

        _atkCollider.enabled = false;

        // ダッシュ終了後は速度をリセット
        _rb.linearVelocityX = 0;
    }
}
