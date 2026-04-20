using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 行動処理は敵味方共通　(プレイヤーは別に専用のものがある)
/// </summary>
public abstract class CharacterBaseAction : MonoBehaviour
{
    [Header("実行間隔")]
    [SerializeField] protected float _intervalTime = 1.0f;

    [Header("対象との距離がこれ以下で実行")]
    [SerializeField] protected float _activationDistance = 3.0f;

    [Header("予兆(！)の時間")]
    [SerializeField] protected float _signTime = 1.0f;

    [Header("アニメーション時間(実行時間)")]
    [SerializeField] protected float _animTime = 0.5f;

    [Header("Animator")]
    [SerializeField] protected Animator _anim;

    [Header("予兆(!)")]
    [SerializeField] protected GameObject _actionSign;

    [Header("炎")]
    [SerializeField] protected bool _magicTypeIsFire;

    [Header("氷")]
    [SerializeField] protected bool _magicTypeIsIce;

    [Header("雷")]
    [SerializeField] protected bool _magicTypeIsThunder;

    [Header("雷2")]
    [SerializeField] protected bool _magicTypeIsThunder2;


    //MoveStateにも行動中か知らせるためにpublicにしておく 
    //MoveState側は知るだけで変更したりしないから参照のみ許可しておく
    public bool IsActing { get; private set; }

    //行動キャラのMoveState系のクラスが今のターゲットを教えてくれる
    [HideInInspector] public Transform Target { get; set; }

    //行動終了直後に発動
    public event Action Acted;

    protected virtual void Start()
    {
        if (_actionSign != null)
        {
            _actionSign.SetActive(false);
        }

        StartCoroutine(ActionLoop());
    }

    /// <summary>
    /// IsActingがtrueかfalseかを確認し続ける
    /// IsActingがfalseになると攻撃処理へ
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ActionLoop()
    {
        while (true)
        {
            if (Target == null)
            {
                yield return null;
                continue;
            }

            float dist = Vector2.Distance(transform.position, Target.position);

            //ターゲットがNULLじゃない かつ ターゲットとの距離が一定以下だったら
            if (Target != null && dist <= _activationDistance)
            {
                //既に行動していなければ
                if (!IsActing)
                {
                    IsActing = true;
                    //ここで行動サインを出すメソッドへ
                    //サインしてその後の行アクションが終了したらここの処理を再開
                    yield return StartCoroutine(BaseRoutine());
                    IsActing = false;
                    
                    yield return new WaitForSeconds(_intervalTime);
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// ここでの具体的な処理は行動サインのみ
    /// あとは子クラスにまかせる
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator BaseRoutine()
    {
        if (_actionSign != null)
        {
            _actionSign.SetActive(true);
            yield return new WaitForSeconds(_signTime);
            _actionSign.SetActive(false);
        }
       
        //実際の行動(具体的な内容は子クラスで決める)
        yield return StartCoroutine(ExecuteAction());
    }

    /// <summary>
    /// アニメーションの共通処理
    /// </summary>
    /// <param name="paramName"></param>
    /// <returns></returns>
    protected IEnumerator PlayActionAnim(string paramName)
    {
        _anim.SetBool(paramName, true);
        yield return new WaitForSeconds(_animTime);
        _anim.SetBool(paramName, false);
    }

    /// <summary>
    /// Actedを発動させる
    /// MoveState側でターゲットをリセットする
    /// </summary>
    protected void NotifyActed()
    {
        Acted?.Invoke();
    }


    // 具体的に何をするか（攻撃、弾発射、回復など）
    protected abstract IEnumerator ExecuteAction();
}