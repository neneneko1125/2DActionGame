using UnityEngine;
using TMPro; // TMPを扱うために必要
using UnityEngine.UI;

public class NameChangeManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField; // インスペクターで登録

    [SerializeField] private PlayerData _playerData;

    private void Start()
    {
        _inputField.text = _playerData.Name;
    }

    /// <summary>
    /// 入力が完了した時に呼び出す（Enterキーやフォーカス外れ）
    /// </summary>
    public void OnEndEdit()
    {
        _playerData.Name = _inputField.text;
        Debug.Log("入力された名前: " + _playerData.Name);
    }

}