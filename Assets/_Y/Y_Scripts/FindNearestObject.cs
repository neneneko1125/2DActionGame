using UnityEngine;
using System.Collections.Generic;

public class FindNearestObject : MonoBehaviour
{
    //探したいタグ名を記入
    [SerializeField] private string targetTag = "Target";

    //ターゲットをまとめて収納するリスト
    private List<Transform> targets = new List<Transform>();

    //一番近いターゲット
    private Transform nearestTarget;

    //前のフレームで一番近かったターゲット
    private Transform previousNearest;

    private void Start()
    {
        //指定したタグを全て取得
        GameObject[] objs = GameObject.FindGameObjectsWithTag(targetTag);

        //取得したオブジェクトをリストに追加
        foreach (var obj in objs)
        {
            targets.Add(obj.transform);
        }
    }

    private void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        //毎フレームごとに一番近いターゲットを調べる
        nearestTarget = GetNearestTarget();

        if (previousNearest != null && previousNearest != nearestTarget)
        {
            //前のターゲットの色を白に戻す
            previousNearest.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (nearestTarget != null)
        {
            //今一番近いターゲットを赤くする
            nearestTarget.GetComponent<SpriteRenderer>().color = Color.red;

            //次のフレームのために保存
            previousNearest = nearestTarget;
        }
    }

    /// <summary>
    /// 一番近いオブジェクトを調べるメソッド
    /// </summary>
    /// <returns></returns>
    private Transform GetNearestTarget()
    {
        Transform nearest = null;

        //minDistanceは、最初どんな値よりも大きい
        float minDistance = float.MaxValue;

        //ターゲット全員を調べる
        foreach (Transform t in targets)
        {
            //自分からターゲットまでの距離を計算
            float distance = Vector2.Distance(t.position, transform.position);

            // 最も短い距離を更新していく
            if (distance < minDistance)
            {
                //距離が最小値のオブジェクトに更新する
                minDistance = distance;
                nearest = t;
            }
        }

        // 距離が一番近いターゲットを返す
        return nearest;
    }
}
