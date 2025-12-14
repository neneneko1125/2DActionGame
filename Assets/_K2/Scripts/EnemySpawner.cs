using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //カメラには当たり判定(Triggerにチェックする必要がある)はもちろん、RigidBody2Dも必要であることに注意！！
    //このスクリプトは空のオブジェクトにアタッチ。空のオブジェクトの座標に敵が生成される。空のオブジェクトにも当たり判定(Trigger)を付与しよう！

    [SerializeField] private GameObject _enemyPrefab;

    //敵がNULLのときに生成するために変数を用意する
    private GameObject _enemy;  

    [SerializeField, Header("生成する回数の制限")] private int _spawnLimit = 3;
    private int _spawnCount = 0;

    [SerializeField, Header("再生成するまでの時間")] private float _spawnIntervalTime = 60f;
    private bool _isInterval = false;

    [SerializeField] private bool _rareEnemy = false;
    [SerializeField, Header("レアエネミーの確率(百分率)")] private int _rareEnemyProbability = 30;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //レアモンスターなら
        if (_rareEnemy)
        {
            // ※ 0～100の範囲でランダムな整数値が返る
            int rnd = Random.Range(0, 101);

            //ランダム整数が出現率を下回ったら
            if (!(rnd < _rareEnemyProbability))
            {
                //スポナー削除
                Destroy(gameObject);
            }
        }

        //インターバルじゃないかつ生成制限に引っかかってないかつMainCameraを検知したら
        if (!_isInterval && _spawnCount < _spawnLimit && collision.CompareTag("MainCamera"))
        {
            //敵がNULLならば(敵が重複することはない)
            if (_enemy == null)
            {
                _spawnCount++;   //カウントを＋１
                _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);   //敵を生成
                StartCoroutine(Interval());     //インターバル
            }
        }
    }

    private IEnumerator Interval()
    {
        _isInterval = true;
        yield return new WaitForSeconds(_spawnIntervalTime);
        _isInterval = false;
    }

}
