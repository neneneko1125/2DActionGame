using UnityEngine;

/// <summary>
/// アタッカー(近距離遠距離両方)のMoveState
/// </summary>
public class FriendAttackMoveState : FriendBaseMoveState
{
    private FriendWarp _warp;

    protected override void Start()
    {
        base.Start();
        _action = GetComponent<CharacterBaseAction>();
        _warp = GetComponent<FriendWarp>();

        if (_warp != null)
        {
            _warp.OnWarped += ResetTarget;
        }

        if (_action != null)
        {
            _action.Acted += ResetTarget;
        }

        //InvokeRepeating(呼び出すメソッド, スタート時間, 呼び出す間隔);
        InvokeRepeating(nameof(SearchNearestEnemy), 0, 0.2f);
    }

    /// <summary>
    /// ターゲットリセット
    /// ワープしたときなどに呼び出す
    /// </summary>
    private void ResetTarget() => _target = null;

    /// <summary>
    /// 対象の敵がnullのときは指定時間ごとに探し続ける
    /// なお、探している間は_enemyがnullになるので、親クラスでターゲットをPlayerにしてくれる
    /// 一番近い敵を探してくれる
    /// </summary>
    void SearchNearestEnemy()
    {
        //ターゲットの敵が既にいる場合はreturnしてメソッドをキャンセル
        if (_target != null)
        {
            return;
        }

        // 円の中の敵をすべて取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _searchRadius, _targetLayer);

        float minDistance = float.MaxValue;
        Transform nearest = null;

        foreach (var h in hits)
        {
            // 自分と敵の距離を計算
            float dist = Vector2.Distance(transform.position, h.transform.position);

            //最小値を探し出す
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = h.transform;
            }
        }
        _target = nearest;

        //攻撃を管理するクラスにどの敵をターゲットにしてるか教えてあげる
        if (_action != null)
        {
            _action.Target = _target;
        }
    }

}