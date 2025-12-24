using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharDataUIManager : MonoBehaviour
{
    [Header("ベースとなるUI")]
    [SerializeField] private TextMeshProUGUI _nameText; 
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _lvText;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _expBar;


    [SerializeField] private TextMeshProUGUI[] _nameTexts;  
    [SerializeField] private TextMeshProUGUI[] _hpTexts;
    [SerializeField] private TextMeshProUGUI[] _lvTexts;
    [SerializeField] private Image[] _hpBars;
    [SerializeField] private Image[] _expBars;

    void Start()
    {
        SetFriendNameUI();
        SetPlayerNameUI();
    }

    public void SetPlayerNameUI()
    {
        var playerInstance = CharInstanceManager.Instance.Player;

        if (playerInstance == null) return;

        _nameText.text = playerInstance.baseData.PlayerName;
    }


    private void SetFriendNameUI()
    {
        List<FriendData> friends = OrganizationManager.Instance.SelectedFriends;

        //iはUIの枠数まで回す
        for (int i = 0; i < _nameTexts.Length; i++)
        {
            //キャラ数がUIより少ないとき
            if (i >= friends.Count)
            {
                _nameTexts[i].text = "";
                continue;
            }

            FriendData data = friends[i];

            //Null チェック（念のため）
            if (data == null) continue;

            // 名前
            _nameTexts[i].text = data.FriendName;

        }
    }

    /// <summary>
    ///　FriendHPで呼び出す
    /// </summary>
    /// <param name="index"></param>
    /// <param name="now"></param>
    /// <param name="max"></param>
    public void UpdateHPUIOfFriend(int index, int nowHP, int maxHP)
    {
        if (_hpTexts[index] != null)
            _hpTexts[index].text = nowHP.ToString();

        if (_hpBars[index] != null)
            _hpBars[index].fillAmount = (float)nowHP / maxHP;
    }


    /// <summary>
    /// FriendHPで呼び出す
    /// </summary>
    /// <param name="index"></param>
    /// <param name="lv"></param>
    /// <param name="currentExp"></param>
    /// <param name="needExp"></param>
    public void UpdateLvEXPUIOfFriend(int index,int lv, int currentExp, int needExp)
    {
        if (_lvTexts != null)
            _lvTexts[index].text = lv.ToString();

        if (_expBars != null)
            _expBars[index].fillAmount = (float)currentExp / needExp;
    }


    /// <summary>
    /// PlayerHPでよびだす   
    /// </summary>
    /// <param name="nowHP"></param>
    /// <param name="maxHP"></param>
    public void UpdateHPUIOfPlayer(int nowHP, int maxHP)
    {
        if (_hpText != null)
            _hpText.text = nowHP.ToString();

        if (_hpBar != null)
            _hpBar.fillAmount = (float)nowHP / maxHP;

    }

    /// <summary>
    /// PlayerHPで呼び出す
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="currentExp"></param>
    /// <param name="needExp"></param>
    public void UpdateLvEXPUIOfPlayer(int lv, int currentExp, int needExp)
    {
        if (_lvText != null)
            _lvText.text = lv.ToString();

        if (_expBar != null)
            _expBar.fillAmount = (float)currentExp / needExp;
    }

}
