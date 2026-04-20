using UnityEngine;

public class DamageAndHealTextSpawn : MonoBehaviour
{
    [Header("敵がダメージを受けたときに表示")]
    [SerializeField] private GameObject _damageTextEnemy;
    [Header("プレイヤーか味方がダメージを受けた時に表示")]
    [SerializeField] private GameObject _damageText;
    [Header("会心の一撃を受けた時に表示")]
    [SerializeField] private GameObject _criticalDamageText;

    [Header("回復したときに表示")]
    [SerializeField] private GameObject _healText;

    [Header("Canvas")]
    [SerializeField] private Canvas _mainCanvas;

    public static DamageAndHealTextSpawn Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// カメラをスクリーン座標に変換し、ダメージテキストを生成し、
    /// 最後にDamageTextAnimのSetupメソッドを呼び出す
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="worldPosition"></param>
    /// <param name="atk"></param>
    private void SpawnText(GameObject prefab, Vector3 worldPosition, int atk)
    {
        //カメラの座標をワールド座標からスクリーン座標に変換する
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        //実際にテキストを生成する 親は指定のCanvas
        GameObject text = Instantiate(prefab, _mainCanvas.transform);

        //DamageTextAnimをもってくる
        DamageAndHealTextAnimation anim = text.GetComponent<DamageAndHealTextAnimation>();

        if (anim != null)
        {
            //Setupを呼び出してString変換→アニメーション再生
            anim.Setup(atk, screenPos, _mainCanvas.transform);
        }
    }

    /// <summary>
    /// 敵用　ダメージを受けた時に呼び出す
    /// これを呼び出すと直ちにテキスト生成→ダメージをString変換→アニメーションと処理されていく
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="damage"></param>
    public void SpawnDamageTextEnemy(Vector3 worldPosition, int damage)
    {
        SpawnText(_damageTextEnemy, worldPosition, damage);
    }

   
    /// <summary>
    /// プレイヤーと味方キャラ用　
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="damage"></param>
    public void SpawnDamageTextPlayerAndFriend(Vector3 worldPosition, int damage)
    {
        SpawnText(_damageText, worldPosition, damage);
    }

    /// <summary>
    /// 回復時に呼び出す
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="damage"></param>
    public void SpawnHealText(Vector3 worldPosition, int damage)
    {
        SpawnText(_healText, worldPosition, damage);
    }

    /// <summary>
    /// 会心の一撃用
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="damage"></param>
    public void SpawnCriticalDamageText(Vector3 worldPosition, int damage)
    {
        SpawnText(_criticalDamageText, worldPosition, damage);
    }

}
