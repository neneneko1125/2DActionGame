using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遠距離回復キャラクターのMoveState
/// </summary>
public class FriendHealMoveState : FriendBaseMoveState
{
    protected override void Start()
    {
        base.Start();
        _action = GetComponent<CharacterBaseAction>();
        //InvokeRepeating(呼び出すメソッド, スタート時間, 呼び出す間隔);
        InvokeRepeating(nameof(SearchPinchCharacter), 0, 0.2f);
    }

    /// <summary>
    /// HPの割合が一番小さいキャラクターを探す
    /// 距離で探すわけではない
    /// HPの割合のデータを参照するのにInstanceDataが必要
    /// </summary>
    private void SearchPinchCharacter()
    {
        //比較用の変数(1.0f = 100%)
        float minHPRatio = 1.0f;
        Transform nearestPinchTransform = null;

        //このリストに生存しているPlayerとFriendを全員入れる
        var allUnits = new List<BaseInstanceData>();

        //プレイヤーを追加
        var pData = CharacterInstanceManager.Instance.PlayerInstanceData;
        if (pData != null)
        {
            allUnits.Add(pData);
        }

        //仲間リストを追加
        var fList = CharacterInstanceManager.Instance.FriendsInstanceDataList;
        if (fList != null)
        {
            allUnits.AddRange(fList);
        }

        //ループを回して一番HP割合が低いキャラクターを探す
        foreach (var unit in allUnits)
        {
            //死亡している、またはTransformがない場合はスキップ
            if (unit.currentHP <= 0 || unit.CharacterTransform == null)
            {
                continue;
            }

            if (unit.HPRatio < minHPRatio)
            {
                //最小値更新
                minHPRatio = unit.HPRatio;
                nearestPinchTransform = unit.CharacterTransform;
            }
        }

        //HP割合最小キャラ決定
        _target = nearestPinchTransform;

        //ターゲットがnull,またはターゲットが自分自身だったら
        if (_target == null || _target == transform)
        {
            _target = _player;     //プレイヤーをターゲットにする
        }

        //攻撃を管理するクラスにどの敵をターゲットにしてるか教えてあげる
        if (_action != null)
        {
            _action.Target = _target;
        }
            
    }
}