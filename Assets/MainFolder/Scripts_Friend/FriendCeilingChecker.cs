using UnityEngine;
using System;

public class FriendCeilingChecker : MonoBehaviour
{
    //JumpAI側で登録処理をする
    public event Action<bool> OnHitCeiling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Groundタグを検知したら
        if (collision.gameObject.CompareTag("Ground"))
        {
            //trueを返す
            OnHitCeiling?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Groundタグを検知したら
        if (collision.gameObject.CompareTag("Ground"))
        {
            //falseを返す
            OnHitCeiling?.Invoke(false);
        }
    }

}
