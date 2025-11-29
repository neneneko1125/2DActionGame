using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [Header("Lerp関連")]
    [SerializeField] private float followSpeed = 3.0f;  //追跡速度

    [Header("SmoothDamp関連")]
    [SerializeField] private float smoothTime = 0.2f; // 動きの滑らかさ 小さい値ほど追跡速度が高い
    private Vector3 _velocity = Vector3.zero;         // SmoothDampが内部で使う速度

    [Header("オブジェクトの移動範囲を制限")]
    [SerializeField] private Vector2 minLimit = new Vector2(-5f, -3f);
    [SerializeField] private Vector2 maxLimit = new Vector2(5f, 3f);


    void Update()
    {
       // FollowMouseSimple();    //一般的な方法
       // FollowMouseLerp();    //Lerpを使う方法
       // FollowMouseSmoothDamp();  //SmoothDampを使う方法
        
    }

    void FollowMouseSimple()
    {
        //マウスの座標を取得
        Vector3 mouseScreenPos = Input.mousePosition;

        //スクリーン座標 → ワールド座標に変換 (マウスの座標は元々スクリーン座標)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        //z座標はノータッチ (これがないとカメラのz座標と被ってカメラに映らない)
        mouseWorldPos.z = transform.position.z;

        //オブジェクトをマウスの位置へ移動
        transform.position = mouseWorldPos;
    }

    void FollowMouseLerp()
    {
        //マウスの座標を取得
        Vector3 mouseScreenPos = Input.mousePosition;

        //スクリーン座標 → ワールド座標に変換 (マウスの座標は元々スクリーン座標)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        //z座標はノータッチ (これがないとカメラのz座標と被ってカメラに映らない)
        mouseWorldPos.z = transform.position.z;

        //Lerp(a,b,t):始点aと終点bをt(両端の距離を1としたときの割合、範囲は0～1)で補完する。
        transform.position = Vector3.Lerp(transform.position, mouseWorldPos, followSpeed * Time.deltaTime);
    }

    void FollowMouseSmoothDamp()
    {
        //マウスの座標を取得
        Vector3 mouseScreenPos = Input.mousePosition;

        //スクリーン座標 → ワールド座標に変換 (マウスの座標は元々スクリーン座標)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        //z座標はノータッチ (これがないとカメラのz座標と被ってカメラに映らない)
        mouseWorldPos.z = transform.position.z;

        //移動範囲を制限する(任意) Clampで最小値と最大値を設定できる
        mouseWorldPos.x = Mathf.Clamp(mouseWorldPos.x, minLimit.x, maxLimit.x);
        mouseWorldPos.y = Mathf.Clamp(mouseWorldPos.y, minLimit.y, maxLimit.y);

        //SmoothDamp(a,b,c,d)a:現在の座標 b:目標の座標 c:現在の速度 d:目標に到達するまでの時間
        transform.position = Vector3.SmoothDamp(transform.position, mouseWorldPos, ref _velocity, smoothTime);
    }

   
    /// <summary>
    /// Sceneビューに移動範囲を可視化
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3((minLimit.x + maxLimit.x) / 2, (minLimit.y + maxLimit.y) / 2, 0);
        Vector3 size = new Vector3(maxLimit.x - minLimit.x, maxLimit.y - minLimit.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
}
