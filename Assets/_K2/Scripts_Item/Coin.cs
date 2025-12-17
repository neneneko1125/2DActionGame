using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _addCoin = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DropItemManager.Instance.dropCoinCount += _addCoin;
            Debug.Log(DropItemManager.Instance.dropCoinCount);
            Destroy(gameObject);
        }
    }
}
