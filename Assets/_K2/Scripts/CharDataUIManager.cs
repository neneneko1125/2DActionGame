using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharDataUIManager : MonoBehaviour
{
    [Header("ベースとなるUI")]
    [SerializeField] private Text _nameText; 
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Image _hpBar;


    [SerializeField] private Text[] _nameTexts;  //名前は普通のText(仮)
    [SerializeField] private TextMeshProUGUI[] _hpTexts;
    [SerializeField] private Image[] _hpBars;
    [SerializeField] private Image[] _faceIcons;

    void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        List<FriendData> friends = OrganizationManager.Instance.SelectedFriends;

        //iはUIの枠数まで回す
        for (int i = 0; i < _nameTexts.Length; i++)
        {
            if (i >= friends.Count)
            {
                if (_nameTexts[i] == null)
                    Debug.LogError($"nameTexts[{i}] が Inspector で設定されていません！");

                if (_hpTexts[i] == null)
                    Debug.LogError($"hpTexts[{i}] が Inspector で設定されていません！");

                if (_faceIcons[i] == null)
                    Debug.LogError($"faceIcons[{i}] が Inspector で設定されていません！");


                if (_nameTexts[i] != null)
                    _nameTexts[i].text = "";

                if (_hpTexts[i] != null)
                    _hpTexts[i].text = "";

                if (_faceIcons[i] != null)
                    _faceIcons[i].sprite = null;

                continue;
            }

            FriendData data = friends[i];

            //Null チェック（念のため）
            if (data == null) continue;

            // 名前
            _nameTexts[i].text = data.FriendName;

            // HP
            _hpTexts[i].text = data.MaxHP.ToString();

            // アイコン（あれば）
            if (_faceIcons.Length > i && data.Icon != null)
                _faceIcons[i].sprite = data.Icon;
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

}
