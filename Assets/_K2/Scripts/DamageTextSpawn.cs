using UnityEngine;

public class DamageTextSpawn : MonoBehaviour
{
    [Header("Prefab化されたダメージテキストEnemy")]
    [SerializeField] private GameObject damageTextPrefabEnemy;
    [Header("Prefab化されたダメージテキストPlayer")]
    [SerializeField] private GameObject damageTextPrefabPlayer;
    [Header("Canvas")]
    [SerializeField] private Canvas mainCanvas;

    public static DamageTextSpawn Instance { get; private set; }
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
    private void SpawnDamageText(GameObject prefab, Vector3 worldPosition, int atk)
    {
        //カメラの座標をワールド座標からスクリーン座標に変換する
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        //実際にテキストを生成する 親は指定のCanvas
        GameObject text = Instantiate(prefab, mainCanvas.transform);

        //DamageTextAnimをもってくる
        DamageTextAnim anim = text.GetComponent<DamageTextAnim>();

        if (anim != null)
        {
            //Setupを呼び出してString変換→アニメーション再生
            anim.Setup(atk, screenPos, mainCanvas.transform);
        }
        else
        {
            Debug.LogWarning("DamageAnimがアタッチされていません！", prefab);
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
        SpawnDamageText(damageTextPrefabEnemy, worldPosition, damage);
    }

    /// <summary>
    /// プレイヤー用　ダメージを受けた時に呼び出す
    /// これを呼び出すと直ちにテキスト生成→ダメージをString変換→アニメーションと処理されていく
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="damage"></param>
    public void SpawnDamageTextPlayer(Vector3 worldPosition, int damage)
    {
        SpawnDamageText(damageTextPrefabPlayer, worldPosition, damage);
    }
}
