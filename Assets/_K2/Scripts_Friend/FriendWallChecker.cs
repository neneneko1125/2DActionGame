using UnityEngine;
using System;

public class FriendWallChecker : MonoBehaviour
{
    public event Action OnWallChecker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Groundƒ^ƒO‚ªŒŸ’m‚µ‚½‚ç
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnWallChecker?.Invoke();
        }
    }

}
