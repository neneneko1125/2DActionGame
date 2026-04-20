using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharDataUIManager : MonoBehaviour
{
    [Header("ベースとなるUI")]
    [SerializeField] private TextMeshProUGUI _playerNameText; 
    [SerializeField] private TextMeshProUGUI _playerHpText;
    [SerializeField] private TextMeshProUGUI _playerLvText;
    [SerializeField] private Image _playerHpBar;
    [SerializeField] private Image _playerExpBar;


    [SerializeField] private TextMeshProUGUI[] _friendsNameText;  
    [SerializeField] private TextMeshProUGUI[] _friendsHpText;
    [SerializeField] private TextMeshProUGUI[] _friendLvText;
    [SerializeField] private Image[] _friendHpBar;
    [SerializeField] private Image[] _friendExpBar;

    void Start()
    {
        SetFriendNameUI();
        SetPlayerNameUI();
    }

    public void SetPlayerNameUI()
    {
        var playerInstance = CharacterInstanceManager.Instance.PlayerInstanceData;

        if (playerInstance == null) return;

        _playerNameText.text = playerInstance.Data.Name;
    }


    private void SetFriendNameUI()
    {
        List<FriendData> friends = OrganizationManager.Instance.SelectedFriends;

        //iはUIの枠数まで回す
        for (int i = 0; i < _friendsNameText.Length; i++)
        {
            //キャラ数がUIより少ないとき
            if (i >= friends.Count)
            {
                _friendsNameText[i].text = "";
                continue;
            }

            FriendData data = friends[i];

            //Null チェック（念のため）
            if (data == null) continue;

            // 名前
            _friendsNameText[i].text = data.Name;

        }
    }

    /// <summary>
    ///　FriendHPで呼び出す
    ///　FriendHPはInstanceDataをもってるよ
    /// </summary>
    /// <param name="index"></param>
    /// <param name="now"></param>
    /// <param name="max"></param>
    public void UpdateHPUIOfFriend(int index, int nowHP, int maxHP)
    {
        if (_friendsHpText[index] != null)
            _friendsHpText[index].text = nowHP.ToString();

        if (_friendHpBar[index] != null)
            _friendHpBar[index].fillAmount = (float)nowHP / maxHP;
    }


    /// <summary>
    /// FriendHPで呼び出す
    /// FriendHPはInstanceDataをもってるよ
    /// </summary>
    /// <param name="index"></param>
    /// <param name="lv"></param>
    /// <param name="currentExp"></param>
    /// <param name="needExp"></param>
    public void UpdateLvEXPUIOfFriend(int index, int lv, int currentExp, int needExp)
    {
        if (_friendLvText != null)
            _friendLvText[index].text = lv.ToString();

        if (_friendExpBar != null)
            _friendExpBar[index].fillAmount = (float)currentExp / needExp;
    }


    /// <summary>
    /// PlayerHPでよびだす   
    /// PlayerHPはInstanceDataをもってるよ
    /// </summary>
    /// <param name="nowHP"></param>
    /// <param name="maxHP"></param>
    public void UpdateHPUIOfPlayer(int nowHP, int maxHP)
    {
        if (_playerHpText != null)
            _playerHpText.text = nowHP.ToString();

        if (_playerHpBar != null)
            _playerHpBar.fillAmount = (float)nowHP / maxHP;

    }

    /// <summary>
    /// PlayerHPで呼び出す
    /// PlayerHPはInstanceDataをもってるよ
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="currentExp"></param>
    /// <param name="needExp"></param>
    public void UpdateLvEXPUIOfPlayer(int lv, int currentExp, int needExp)
    {
        if (_playerLvText != null)
            _playerLvText.text = lv.ToString();

        if (_playerExpBar != null)
            _playerExpBar.fillAmount = (float)currentExp / needExp;
    }

}
