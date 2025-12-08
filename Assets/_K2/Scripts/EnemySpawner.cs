using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //カメラには当たり判定(Triggerにチェックする必要がある)はもちろん、RigidBody2Dも必要であることに注意！！
    //このスクリプトは空のオブジェクトにアタッチ。空のオブジェクトの座標に敵が生成される。空のオブジェクトにも当たり判定(Trigger)を付与しよう！

    [SerializeField] private GameObject _enemyPrefab;  // インスタンス化する敵プレハブ
    private GameObject _enemy;   //敵がNULLのときに生成するために変数を用意する

    [SerializeField] private int _spawnLimit = 3;    //生成の回数制限
    private int _spawnCount = 0; //スポーン回数

    [SerializeField] private float _spawnIntervalTime = 60f; //インターバル時間
    [SerializeField] private bool _isInterval = false;   //一度生成するとtrueになる

    [SerializeField] private bool _rareEnemy = false;
    [SerializeField] private int _rareEnemyProbability = 30;

    void OnTriggerEnter2D(Collider2D other)
    {
        //レアモンスターなら
        if (_rareEnemy == true)
        {
            int rnd = Random.Range(0, 101); // ※ 0～100の範囲でランダムな整数値が返る

            //ランダム整数が出現率を下回ったら
            if (!(rnd < _rareEnemyProbability))
            {
                Destroy(gameObject);    //スポナー削除
            }
        }

        //インターバル中じゃないかつカウントがまだ上限に達していないかつタグがMainCameraのオブジェクトに触れたら
        if (_isInterval == false && _spawnCount < _spawnLimit && other.CompareTag("MainCamera"))
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

    IEnumerator Interval()
    {
        _isInterval = true;
        yield return new WaitForSeconds(_spawnIntervalTime);
        _isInterval = false;
    }

}
