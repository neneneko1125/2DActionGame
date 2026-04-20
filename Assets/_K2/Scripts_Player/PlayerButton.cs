using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

//Inspectorで選ぶ
public enum ButtonType { S, Attack, Dash, Down, Up, Guard, Warp, Jump }

/// <summary>
/// それぞれのボタンにアタッチされている
/// </summary>
public class PlayerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ButtonType _type;

    private GameObject _player;

    public static bool IsPressed { get; private set; }


    private void Start()
    {
        //取得処理開始
        StartCoroutine(FindPlayerCoroutine());
    }

    /// <summary>
    /// Startで普通に取得しようとするとタイミングがはやいから遅らせる
    /// </summary>
    /// <returns></returns>
    private IEnumerator FindPlayerCoroutine()
    {
        // _playerがnullの間だけループ
        while (_player == null)
        {
            _player = GameObject.FindWithTag("Player");

            if (_player != null)
            {
                yield break; // ループ終了
            }

            // 1フレーム待機して再試行
            yield return null;
        }
    }

    /// <summary>
    /// ボタンを押したときの処理
    /// OnPointerDownでコルーチンは使えない
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_type == ButtonType.Attack)
        {
            StartCoroutine(AttackRoutine());
        }
        if (_type == ButtonType.Dash)
        {
            StartCoroutine(DashRoutine());
        }
        if (_type == ButtonType.Down)
        {
            StartCoroutine(DownRoutine());
        }
        if (_type == ButtonType.Up)
        {
            StartCoroutine(UpRoutine());
        }
        if (_type == ButtonType.S)
        {
            BoolButtonClick.IsS = true;
        }
        if (_type == ButtonType.Guard)
        {
            BoolButtonClick.IsGuard = true;
        }
        if (_type == ButtonType.Warp)
        {
            GameEventManager.RaiseWarp();
        }
        if (_type == ButtonType.Jump)
        {
            GameEventManager.RaiseJump();
            BoolButtonClick.IsW = true;
            BoolButtonClick.IsW = false;
        }
    }


    /// <summary>
    /// ボタンを離したときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_type == ButtonType.S)
        {
            BoolButtonClick.IsS = false;
        }
        if (_type == ButtonType.Guard)
        {
            BoolButtonClick.IsGuard = false;
        }
    }


    /// <summary>
    /// 通常攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackRoutine()
    {
        BoolButtonClick.IsAttack = true;
        yield return new WaitForSeconds(0.1f);
        BoolButtonClick.IsAttack = false;
    }

    /// <summary>
    /// ダッシュ攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashRoutine()
    {
        BoolButtonClick.IsDash = true;
        yield return new WaitForSeconds(0.1f);
        BoolButtonClick.IsDash = false;
    }

    /// <summary>
    /// 下攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownRoutine()
    {
        BoolButtonClick.IsDown = true;
        yield return new WaitForSeconds(0.1f);
        BoolButtonClick.IsDown = false;
    }

    /// <summary>
    /// 上攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpRoutine()
    {
        BoolButtonClick.IsUp = true;
        yield return new WaitForSeconds(0.1f);
        BoolButtonClick.IsUp = false;
    }

}