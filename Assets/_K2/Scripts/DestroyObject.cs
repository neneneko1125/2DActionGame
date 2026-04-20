using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 2f;
    [SerializeField] private bool _isGroundDestroy;

    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("Ground") || collision.CompareTag("Ground2")) && _isGroundDestroy)
        {
            Destroy(gameObject);
        }
    }

}
