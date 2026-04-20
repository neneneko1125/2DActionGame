using UnityEngine;

public class FriendBufferMoveState : FriendBaseMoveState
{
    private bool _hasTargetSet = false; // ターゲット設定済みフラグ


    protected override void Start()
    {
        base.Start();
        //ターゲットをプレイヤーに固定
        _target = _player;

        //攻撃を管理するクラスにどの敵をターゲットにしてるか教えてあげる
        if (_action != null)
        {
            _action.Target = _target;
        }
    }
    protected override void Update()
    {
        // プレイヤーが見つかった瞬間に一度だけ、または常時Actionに同期させる
        if (_player != null && !_hasTargetSet)
        {
            _target = _player;
            if (_action != null)
            {
                _action.Target = _target;
                _hasTargetSet = true; // 確実にセットしたことを記録
            }
        }

        base.Update();
    }

    protected override void Move(Transform target, float stopDist, float speed)
    {   
        //距離
        float dist = Vector2.Distance(target.position, transform.position);
        //方向
        float dir = Mathf.Sign(target.position.x - transform.position.x);

        //ターゲットの方向によって左右反転する
        _friendSprite.transform.localScale = new Vector3(-dir * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        //停止距離より短い距離なら
        if (dist < stopDist)
        {
            _rb.linearVelocityX = 0;    //停止
        }
        else
        {
            _rb.linearVelocityX = dir * speed;
        }

    }
}
