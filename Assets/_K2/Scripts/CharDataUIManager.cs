using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharDataUIManager : MonoBehaviour
{
    [Header("ベースとなるUI")]
    [SerializeField] private Text[] nameTexts;  //名前は普通のText(仮)
    [SerializeField] private TextMeshProUGUI[] hpTexts;
    [SerializeField] private Image[] hpBars;
    [SerializeField] private Image[] faceIcons;

    void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        List<FriendData> friends = OrganizationManager.Instance.SelectedFriends;

        //iはUIの枠数まで回す
        for (int i = 0; i < nameTexts.Length; i++)
        {
            if (i >= friends.Count)
            {
                if (nameTexts[i] == null)
                    Debug.LogError($"nameTexts[{i}] が Inspector で設定されていません！");

                if (hpTexts[i] == null)
                    Debug.LogError($"hpTexts[{i}] が Inspector で設定されていません！");

                if (faceIcons[i] == null)
                    Debug.LogError($"faceIcons[{i}] が Inspector で設定されていません！");


                if (nameTexts[i] != null)
                    nameTexts[i].text = "";

                if (hpTexts[i] != null)
                    hpTexts[i].text = "";

                if (faceIcons[i] != null)
                    faceIcons[i].sprite = null;

                continue;
            }

            FriendData data = friends[i];

            //Null チェック（念のため）
            if (data == null) continue;

            // 名前
            nameTexts[i].text = data.FriendName;

            // HP
            hpTexts[i].text = data.MaxHP.ToString();

            // アイコン（あれば）
            if (faceIcons.Length > i && data.Icon != null)
                faceIcons[i].sprite = data.Icon;
        }
    }

    /// <summary>
    ///　FriendHPで呼び出す
    /// </summary>
    /// <param name="index"></param>
    /// <param name="now"></param>
    /// <param name="max"></param>
    public void UpdateHPUI(int index, int now, int max)
    {
        if (hpTexts[index] != null)
            hpTexts[index].text = now.ToString();

        if (hpBars[index] != null)
            hpBars[index].fillAmount = (float)now / max;
    }

}
