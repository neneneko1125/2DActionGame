using UnityEngine;
using System.Collections;
/// <summary>
/// ダッシュ攻撃
/// </summary>
public class CharacterDashAttack : CharacterBaseAction
{
    [Header("ダッシュ速度")]
    [SerializeField] private float _dashSpeed = 5f;

    [Header("Sprite")]
    [SerializeField] private GameObject _characterSprite;
    [Header("攻撃用オブジェクトのコライダー")]
    [SerializeField] private Collider2D _attackCollider;

    private Rigidbody2D _rb;

    
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();

        if (_attackCollider != null)
        {
            _attackCollider.enabled = false;
        }
    }

    /// <summary>
    /// 実際の攻撃処理
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ExecuteAction()
    {
        StartCoroutine(PlayActionAnim("Action"));

        Vector2 dashDir;

        //現在向いている方向によってダッシュ方向を決める
        //左右反転していなければ
        if(_characterSprite.transform.localScale.x > 0)
        {
            dashDir = Vector2.left;
        }
        //左右反転していれば
        else
        {
            dashDir = Vector2.right;
        }

        // 攻撃判定ON
        _attackCollider.enabled = true;

        float timer = 0f;
        //指定時間中なら
        while (timer < _animTime)
        {
            //速度を変更する
            _rb.linearVelocityX = dashDir.x * _dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }
        // 攻撃判定OFF
        _attackCollider.enabled = false;

        //最後は速度を0にして停止
        _rb.linearVelocityX = 0;
    }
}
