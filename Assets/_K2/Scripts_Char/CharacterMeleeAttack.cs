using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 近距離攻撃キャラ
/// </summary>
public class CharacterMeleeAttack : CharacterBaseAction
{
    [Header("攻撃用オブジェクトのコライダー")]
    [SerializeField] private Collider2D _attackCollider;

    protected override void Start()
    {
        base.Start();   
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
        // 攻撃判定ON
        _attackCollider.enabled = true;

        // アニメーション
        yield return StartCoroutine(PlayActionAnim("Action"));

        // 攻撃判定OFF
        _attackCollider.enabled = false;
        
        //これでターゲットをリセット
        NotifyActed();
        
    }
}