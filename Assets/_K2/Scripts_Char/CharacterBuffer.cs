using UnityEngine;
using System.Collections;

public class CharacterBuffer : CharacterBaseAction
{
    [Header("バフの範囲オブジェクト")]
    [SerializeField] private GameObject _bufferObject;

    [Header("バフの時間")]
    [SerializeField] private float _bufferTime;

    protected override void Start()
    {
        base.Start();
        if (_bufferObject != null)
        {
           _bufferObject.SetActive(false);
        }
    }

    /// <summary>
    /// 実際の攻撃処理
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ExecuteAction()
    {
        //バフ判定ON
        _bufferObject.SetActive(true);

        SEManager.Instance.SEBuff();

        //アニメーション
        StartCoroutine(PlayActionAnim("Action"));

        yield return new WaitForSeconds(_bufferTime);

        //バフ判定OFF
        _bufferObject.SetActive(false);
    }

}
