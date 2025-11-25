using UnityEngine;

public class Player_S : MonoBehaviour
{
    [Header("追従するオブジェクト")]
    [SerializeField] private GameObject _target;

    [Header("追従速度(小さい値ほど速い)")]
    [SerializeField] private float _followSpeed = 0.1f;

    [SerializeField] private Vector3 _position = Vector3.zero;

    [Header("移動制限範囲")]
    [SerializeField] private Vector2 _minLimit = new Vector2(-5f, -3f);
    [SerializeField] private Vector2 _maxLimit = new Vector2(5f, 3f);

    void FixedUpdate()
    {
        MouseFollow();
    }


    /// <summary>
    /// 対象のオブジェクトをマウスカーソルに追従させる
    /// </summary>
    private void MouseFollow()
    {
        //マウス位置をワールド座標に変換
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //ここを設定しないと、オブジェクトのz座標がカメラと同じになって映らなくなる
        mousePosition.z = 0f;

        //移動範囲を制限する
        mousePosition.x = Mathf.Clamp(mousePosition.x, _minLimit.x, _maxLimit.x);
        mousePosition.y = Mathf.Clamp(mousePosition.y, _minLimit.y, _maxLimit.y);


        //Lerp(a,b,t):始点aと終点bをt(両端の距離を1としたときの割合、範囲は0～1)で補完する。
        //SmoothDamp(a,b,c,d)a:現在の座標 b:目標の座標 c:現在の速度 d:目標に到達するまでの時間
        _target.transform.position = Vector3.SmoothDamp(_target.transform.position, mousePosition,ref _position, _followSpeed);
    }

    /// <summary>
    /// Sceneビューに範囲を可視化
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3((_minLimit.x + _maxLimit.x) / 2, (_minLimit.y + _maxLimit.y) / 2, 0);
        Vector3 size = new Vector3(_maxLimit.x - _minLimit.x, _maxLimit.y - _minLimit.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
}

