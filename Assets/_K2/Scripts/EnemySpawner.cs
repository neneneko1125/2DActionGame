using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //カメラには当たり判定(Triggerにチェックする必要がある)はもちろん、RigidBody2Dも必要であることに注意！！
    //このスクリプトは空のオブジェクトにアタッチ。空のオブジェクトの座標に敵が生成される。空のオブジェクトにも当たり判定(Trigger)を付与しよう！

    [SerializeField] private GameObject _enemyPrefab;

    //敵がNULLのときに生成するために変数を用意する
    private GameObject _enemy;  

    [SerializeField] private int _spawnLimit = 3;
    private int _spawnCount = 0;

    [SerializeField] private float _spawnIntervalTime = 60f;
    private bool _isInterval = false;

    [SerializeField] private bool _rareEnemy = false;
    [SerializeField] private int _rareEnemyProbability = 30;


    private void OnTriggerEnter2D(Collider2D other)
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

        
        if (!_isInterval && _spawnCount < _spawnLimit && other.CompareTag("MainCamera"))
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
