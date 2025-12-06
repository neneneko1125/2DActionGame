using UnityEngine;
using System.Collections;
public class Enemy2ATK : EnemyBaseATK
{
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private GameObject _enemy;

    private Rigidbody2D _rb;

    protected override void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        base.Start(); // Base の Start を呼ぶ
    }

    protected override IEnumerator AttackRoutine()
    {
        //攻撃前のサイン
        _atkSign.SetActive(true);
        yield return new WaitForSeconds(_atkTimebefore);
        _atkSign.SetActive(false);

        float timer = 0f;

        // ダッシュ方向（右 or 左）
        Vector2 dashDir = _enemy.transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        _atkCollider.enabled = true;
        StartCoroutine(ATKAnimation());
        while (timer < _atkTime)
        {
            _rb.linearVelocity = new Vector2(dashDir.x * _dashSpeed, _rb.linearVelocity.y);

            timer += Time.deltaTime;
            yield return null;
        }
        _atkCollider.enabled = false;

        // ダッシュ終了後は速度をリセット
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
    }
}
