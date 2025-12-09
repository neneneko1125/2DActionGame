using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLvEXP : MonoBehaviour
{
    public int PlayerLv = 1;    //プレイヤーのLv
    [SerializeField] private TextMeshProUGUI levelText; //レベルを表示

    private int _playerEXP = 0;   //プレイヤーの経験値　レベルアップの度にリセット
    [SerializeField] private Image expBarImage; //経験値のバー

    [SerializeField] private int _plusHP = 5;

    [Header("プレイヤーLvのm乗*n")]
    [SerializeField] private float exp_n = 5f;
    [SerializeField] private float exp_m = 1.5f;

    private PlayerHP _playerHP;

    public static PlayerLvEXP Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Instance = This");
            Instance = this;
        }
        else
        {
            Debug.Log("PlayerDestroy");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _playerHP = GetComponent<PlayerHP>();
        UpdateLevelUI();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 経験値を追加してレベルアップをチェック（Enmy_HPクラスで敵のEXPを管理している）
    /// </summary>
    /// <param name="gainedExp"></param>
    public void AddExp(int gainedExp)
    {
        _playerEXP += gainedExp;    //経験値加算
       UpdateLevelUI();
        CheckLevelUp();     //レベルアップチェック
    }

    /// <summary>
    /// レベルアップ処理
    /// </summary>
    private void CheckLevelUp()
    {
        //プレイヤーの経験値が現在のレベルと対応する必要経験値以上ならば
        while (_playerEXP >= GetRequiredExp(PlayerLv))
        {
            _playerEXP -= GetRequiredExp(PlayerLv);    //必要経験値分引く
            PlayerLv++;    //レベルアップ
          //  SaveLevels();   //レベルを保存
          //  SEManager.Instance.LvUPSE();    //SEを鳴らす
            _playerHP.LvUpHP(_plusHP, 1000);  //最大HPを更新して1000回復するメソッドへ
            UpdateLevelUI(); //経験値バーの更新メソッドへ
          
        }

    }



    /// <summary>
    /// 次のレベルに必要な経験値を返す
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int GetRequiredExp(int level)
    {
        return Mathf.RoundToInt(exp_n * Mathf.Pow(level, exp_m));
    }

    /// <summary>
    /// レベルのUI更新
    /// </summary>
    private void UpdateLevelUI()
    {
        if (levelText != null) levelText.text = PlayerLv.ToString();
        if (expBarImage != null) expBarImage.fillAmount = (float)_playerEXP / GetRequiredExp(PlayerLv);
    }

   
}
