using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class StageChangeObject : MonoBehaviour
{
    [SerializeField] private string _nextStageName;

    //レベルか経験値が変化したときのイベント
    //FriendHPスクリプトに知らせる
    public event Action OnChangeLvEXP;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            OnChangeLvEXP?.Invoke();
            OrganizationManager.Instance.MakeAndSendInstanceData();
            SceneManager.LoadScene(_nextStageName);
        }
    }
}
